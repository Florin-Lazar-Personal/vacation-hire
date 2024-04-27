namespace ExchangeRate.Validation
{
    public static class Require
    {
        public static ArgumentGuard<T> ThatArgument<T>(T value, string paramName)
        {
            return new ArgumentGuard<T>(value, paramName);
        }
    }
}
