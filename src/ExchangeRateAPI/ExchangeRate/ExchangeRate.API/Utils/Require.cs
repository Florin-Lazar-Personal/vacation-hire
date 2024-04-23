namespace ExchangeRate.API.Utils
{
    public static class Require
    {
        public static T NotNull<T>(T value, string paramName)
            where T : class
        {
            if (value is null)
            {
                throw new ArgumentNullException(paramName);
            }

            return value;
        }

        public static DateTimeOffset IsMoreRecentThan(
            DateTimeOffset value,
            DateTimeOffset notOlderThan,
            string paramName)
        {
            if (value <= notOlderThan)
            {
                throw new ArgumentException(
                    message: $"Date '{value}' must be more recent than '{notOlderThan}'.",
                    paramName: paramName);
            }

            return value;
        }

        public static decimal IsPositive(
            decimal value,
            string paramName)
        {
            if (value < 0)
            {
                throw new ArgumentException(
                    message: $"Specified value '{value}' is not positive.",
                    paramName: paramName);
            }

            return value;
        }
    }
}
