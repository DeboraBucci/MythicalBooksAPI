namespace MythicalBooksAPI.Helpers
{
    public static class TokenHelper
    {
        private static readonly TimeSpan TokenLifeTime = TimeSpan.FromDays(1);

        public static string GenerateToken(Guid userId )
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = userId.ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());

            return token;
        }

        public static bool IsTokenExpired (string token)
        {
            try
            {
                byte[] data = Convert.FromBase64String(token);
                DateTime tokenDate = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
                return DateTime.UtcNow - tokenDate > TokenLifeTime;
            }

            catch
            {
                return true;
            }
        }
    }
}
