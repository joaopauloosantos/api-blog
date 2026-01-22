using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleBlog.Dto.Dto;

namespace SimpleBlog.API.Configuration
{
    public class FluentValidationFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public FluentValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments)
            {
                if (argument.Value == null) continue;

                // Tenta achar um validador registrado para esse tipo de argumento
                var argumentType = argument.Value.GetType();
                var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);
                var validator = _serviceProvider.GetService(validatorType);

                if (validator != null)
                {
                    // Se achou, executa o ValidateAsync via Reflection (necessário pois o tipo é genérico em tempo de execução)
                    var validateMethod = validatorType.GetMethod("ValidateAsync", [argumentType, typeof(CancellationToken)]);

                    if (validateMethod != null)
                    {
                        var task = (Task)validateMethod.Invoke(validator, [argument.Value, CancellationToken.None])!;
                        await task.ConfigureAwait(false);

                        // Pega o resultado da Task (ValidationResult)
                        var resultProperty = task.GetType().GetProperty("Result");
                        var validationResult = resultProperty!.GetValue(task) as FluentValidation.Results.ValidationResult;

                        // Se tiver erro, interrompe tudo e devolve o 400 Bad Request padronizado
                        if (validationResult != null && !validationResult.IsValid)
                        {
                            var errorResponse = new ErrorResponseDto
                            {
                                Status = StatusCodes.Status400BadRequest,
                                Errors = [.. validationResult.Errors.Select(x => x.ErrorMessage).Distinct()]
                            };

                            context.Result = new BadRequestObjectResult(errorResponse);
                            return;
                        }
                    }
                }
            }

            // Se passou sem erros, segue para o Controller
            await next();
        }
    }
}
