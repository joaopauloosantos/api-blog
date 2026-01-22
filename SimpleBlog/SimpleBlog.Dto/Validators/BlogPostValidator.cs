using FluentValidation;
using SimpleBlog.Dto.Dto;

namespace SimpleBlog.Dto.Validators
{
    public class BlogPostValidator : AbstractValidator<BlogPostDto>
    {
        public BlogPostValidator()
        {
            RuleFor(x => x.Manchete)
                .NotEmpty().WithMessage("A manchete é obrigatória.")
                .Length(5, 100).WithMessage("A manchete deve ter entre 5 e 100 caracteres.");

            RuleFor(x => x.CorpoTexto)
                .NotEmpty().WithMessage("O corpo do texto não pode estar vazio.")
                .Length(5, 500).WithMessage("A manchete deve ter entre 5 e 500 caracteres.");
        }
    }
}
