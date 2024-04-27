namespace ExchangeRate.Validation
{
    public class ArgumentGuard<T>
    {
        public ArgumentGuard(T value, string argumentName)
        {
            ArgumentName = argumentName;
            Value = value;
        }

        public string ArgumentName { get; }

        public T Value { get; }
    }
}
