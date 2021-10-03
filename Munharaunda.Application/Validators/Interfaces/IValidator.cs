namespace Munharaunda.Application.Validators.Interfaces
{
    public interface IValidator<T>
    {
        bool Validate(T t);
    }
}
