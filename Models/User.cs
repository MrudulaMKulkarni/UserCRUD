using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserCRUD.Models
{
    /// <summary>
    /// Class to store user details from MongoDB
    /// </summary>
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public address address { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public company company { get; set; }
    }
    /// <summary>
    /// Address class to store street, suite,city,zipcode and geo etc.
    /// </summary>
    public class address
    {
        public string street { get; set; }
        public string suite { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public geo geo { get; set; }
    }
    /// <summary>
    /// Geo Class for storing lattitude and longitude
    /// </summary>
    public class geo
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }
    /// <summary>
    /// Company class to store company details like name, catchphrase and bs
    /// </summary>
    public class company
    {
        public string name { get; set; }
        public string catchPhrase { get; set; }
        public string bs { get; set; }
    }
}
