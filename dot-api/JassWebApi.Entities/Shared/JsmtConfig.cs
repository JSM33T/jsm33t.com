namespace JassWebApi.Entities.Shared
{
    public class JsmtConfig
    {
        public SmtpConfig SMTP { get; set; } = new SmtpConfig();
        public ConnectionSettings ConnectionSettings { get; set; } = new ConnectionSettings();
        public bool CacheEnabled { get; set; }
    }

    public class SmtpConfig
    {
        public string Server { get; set; } = string.Empty;
        public int Port { get; set; } = 587;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool UseSSL { get; set; } = true;
    }

    public class ConnectionSettings
    {
        public string MsSqlConstr { get; set; } = string.Empty;
        public string PostgresConstr { get; set; } = string.Empty;
    }
}
