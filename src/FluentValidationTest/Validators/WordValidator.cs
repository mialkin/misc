using FluentValidation;

namespace FluentValidationTest.Validators
{
    public class WordValidator : AbstractValidator<Word>
    {
        public WordValidator()
        {
            RuleFor(x => x.Name).Length(1,  50);
            RuleFor(x => x.Translation).Length(1,  500);
            RuleFor(x => x.LanguageId).NotEmpty();
            RuleFor(x => x.DateCreated).NotEmpty();
        }
    }
}