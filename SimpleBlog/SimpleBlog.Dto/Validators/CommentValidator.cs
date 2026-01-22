using FluentValidation;
using SimpleBlog.Dto.Dto;

namespace SimpleBlog.Dto.Validators
{
    public class CommentValidator : AbstractValidator<CommentDto>
    {
        public CommentValidator()
        {
            RuleFor(x => x.Texto)
                .NotEmpty().WithMessage("O comentário não pode ser vazio.")
                .MaximumLength(500).WithMessage("O comentário não pode exceder 500 caracteres.");

            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("O ID do post é obrigatório para criar um comentário.");
        }
    }
}
