namespace UserCRUD.Models
{
    /// <summary>
    /// UserDatabaseSettings class to store Users collection name, connection string and database name of MongoDB
    /// </summary>
    public class UserDatabaseSettings : IUserDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    /// <summary>
    /// Interface for storing UserDatabaseSettings 
    /// </summary>
    public interface IUserDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
