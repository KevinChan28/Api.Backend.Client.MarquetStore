namespace Api.Client.MarquetStore.Tools
{
    public class CreateCode
    {
        private static Random random = new Random();
        private static HashSet<string> usedCoupons = new HashSet<string>();

        public static string GenerateUniqueCode()
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string code = new string(Enumerable.Repeat(characters, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            while (usedCoupons.Contains(code))
            {
                code = new string(Enumerable.Repeat(characters, 5)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }

            usedCoupons.Add(code);

            return code;
        }
    }
}

