using System;

namespace ExchangeRate.Validation
{
    public static class ArgumentGuardExtensions
    {
        public static T IsNotNull<T>(this ArgumentGuard<T> argumentGuard, string message = null)
            where T : class
        {
            if (argumentGuard.Value is null)
            {
                if (message is null)
                {
                    throw new ArgumentNullException(paramName: argumentGuard.ArgumentName);
                }
                else
                {
                    throw new ArgumentNullException(
                        paramName: argumentGuard.ArgumentName,
                        message: message);
                }
            }

            return argumentGuard.Value;
        }

        public static string IsNotNullOrEmpty(this ArgumentGuard<string> argumentGuard, string message = null)
        {
            string value = argumentGuard.IsNotNull();

            if (string.IsNullOrEmpty(value))
            {
                if (message is null)
                {
                    throw new ArgumentException(
                        paramName: argumentGuard.ArgumentName,
                        message: "Value cannot be an empty string.");
                }
                else
                {
                    throw new ArgumentException(
                        paramName: argumentGuard.ArgumentName,
                        message: message);
                }
            }

            return value;
        }

        public static string IsNotNullOrWhiteSpace(this ArgumentGuard<string> argumentGuard, string message = null)
        {
            string value = argumentGuard.IsNotNull();

            if (string.IsNullOrWhiteSpace(value))
            {
                if (message is null)
                {
                    throw new ArgumentException(
                        paramName: argumentGuard.ArgumentName,
                        message: "Value cannot be an empty or whitespaces-only string.");
                }
                else
                {
                    throw new ArgumentException(
                        paramName: argumentGuard.ArgumentName,
                        message: message);
                }
            }

            return value;
        }

        public static T SatisfiesCondition<T>(
            this ArgumentGuard<T> argumentGuard,
            Func<T, bool> condition,
            string message = null)
        {
            bool isConditionSatisfied = condition?.Invoke(argumentGuard.Value) 
                ?? throw new ArgumentNullException(nameof(condition));

            if (!isConditionSatisfied)
            {
                if (message is null)
                {
                    throw new ArgumentException(
                        paramName: argumentGuard.ArgumentName,
                        message: "Argument doesn't satisfies the required condition.");
                }
                else
                {
                    throw new ArgumentNullException(
                        paramName: argumentGuard.ArgumentName,
                        message: message);
                }
            }

            return argumentGuard.Value;
        }
    }
}
