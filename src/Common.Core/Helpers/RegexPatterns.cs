namespace Common.Core.Helpers
{
    public class RegexPatterns
    {
        /// <summary>
        /// Ip - адрес
        /// </summary>
        public const string IPv4 = "^(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

        /// <summary>
        /// Целое число
        /// </summary>
        public const string IsDigit = "^[0-9]+$";
    }
}
