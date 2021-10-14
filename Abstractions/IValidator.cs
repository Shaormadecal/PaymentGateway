namespace Abstractions
{
    public interface IValidator<TInput>
    {
        bool Validate(TInput input);
    }
}