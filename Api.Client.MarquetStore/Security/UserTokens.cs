namespace Api.Client.MarquetStore.Security
{
    public class UserTokens
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public TimeSpan Validty { get; set; }
        public string RefreshToken { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public Guid GuidId { get; set; }
        public DateTime ExpiredTime { get; set; }
        public int Rol { get; set; }
    }
}
