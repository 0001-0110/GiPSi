namespace Services
{
    internal static class StringService
    {
        public static string NormalizeNumber(string number, int length)
        {
            while (number.Length < length)
            {
                number = "0" + number;
            }
            return number;
        }
    }
}
