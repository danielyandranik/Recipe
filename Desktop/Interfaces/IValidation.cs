namespace Desktop.Interfaces
{
    /// <summary>
    /// Interface for validation
    /// </summary>
    public interface IValidation
    {
        /// <summary>
        /// Validates
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <returns>boolean value indicating the validity of parameter</returns>
        bool Validate(object parameter);
    }
}
