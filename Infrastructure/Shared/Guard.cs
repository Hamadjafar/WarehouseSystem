namespace Infrastructure.Shared
{
    public static class Guard
    {
        public static void AssertArgumentNotNull(object value, string argumentName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void AssertArgumentNotLessThanOrEqualToZero(int? value, string argumentName)
        {
            if (value == null)
                throw new ArgumentNullException(argumentName);

            if (value <= 0)
                throw new ArgumentException("Value cannot be less than or equal to zero.", argumentName);
        }
    }
}
