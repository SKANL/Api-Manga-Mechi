namespace MangaMechiApi.Infrastructure.Data;

public class DatabaseSettings
{
    public string ConnectionString { get; set; } = "workstation id=mangasDB.mssql.somee.com;packet size=4096;user id=AnguK_SQLLogin_1;pwd=w7jcfeoicm;data source=mangasDB.mssql.somee.com;persist security info=False;initial catalog=mangasDB;TrustServerCertificate=True";

    public DatabaseSettings() {}
    public DatabaseSettings(string connectionString)
    {
        ConnectionString = connectionString;
    }
}
