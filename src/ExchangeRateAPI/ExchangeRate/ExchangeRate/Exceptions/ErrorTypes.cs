namespace ExchangeRate.Exceptions
{
    public enum ErrorTypes
    {
        /// <summary>
        /// An unspecified error has occured.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Error thrown when authentication is required during current processing, 
        /// but is missing.
        /// </summary>
        Authentication,

        /// <summary>
        /// Error thrown when a certain authorization policy is required during current processing, 
        /// but is missing.
        /// </summary>
        Authorization,

        /// <summary>
        /// Error thrown when a non-existing resource is tried to be accessed during current processing.
        /// </summary>
        ResourceNotFound,

        /// <summary>
        /// Error thrown when there's a validation issue with the input data for the current processing.
        /// </summary>
        RequestValidation,

        /// <summary>
        /// Error thrown when continuing the current processing would lead to a conflict 
        /// with the current state of the processed resource(s).
        /// </summary>
        Conflict,

        /// <summary>
        /// Error thrown when current processing met an unexpected error
        /// and cannot continue.
        /// </summary>
        InternalError
    }
}
