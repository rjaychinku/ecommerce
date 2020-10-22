namespace BuyABit.Extensions
{ 
    public class AppSettings
    {
        public string JSWSecret { get; set; }
        public int  JSWTokenExpiryDuration { get; set; }
        public string RedisHostName { get; set; }
        public string RedisPassword { get; set; }
        public int RedisPort{ get; set; }
    }

    public class ConnectionStrings
    {
        public string SQLConnection { get; set; }
        public string LocalSQLConnection { get; set; }
        
    }
}
