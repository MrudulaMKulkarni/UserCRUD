using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using UserCRUD.Models;

namespace UserCRUD.Services
{
    /// <summary>
    /// Created Service to MongoDB call for all the GET, POST, PUT, DELETE API calls from Controller
    /// </summary>
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        public UserService(IUserDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }
        public List<User> Get()
        {
            List<User> users;
            users = _users.Find(usr => true).ToList();
            return users;
        }
        public User Get(string id) =>
            _users.Find<User>(usr => usr._id == id).FirstOrDefault();

        public User Create(User u)
        {
            _users.InsertOne(u);
            return u;
        }

        public User Put(string id, User u)
        {
            // Create an equality filter
            var filter = Builders<User>.Filter.Eq(usr => usr._id, id);

            //update field code
            //// Create an update definition using the Set operator    
            var update = Builders<User>.Update.Set(usr => usr._id, id);            
            update = Builders<User>.Update.Set(usr => usr.id, u.id);
            if (!string.IsNullOrEmpty(u.name))
                update = Builders<User>.Update.Set(usr => usr.name, u.name);
            if (!string.IsNullOrEmpty(u.username))
                update = Builders<User>.Update.Set(usr => usr.username, u.username);
            if (!string.IsNullOrEmpty(u.email))
                update = Builders<User>.Update.Set(usr => usr.email, u.email);
            if (!string.IsNullOrEmpty(u.address.street))
                update = Builders<User>.Update.Set(usr => usr.address.street, u.address.street);
            if (!string.IsNullOrEmpty(u.address.suite))
                update = Builders<User>.Update.Set(usr => usr.address.suite, u.address.suite);
            if (!string.IsNullOrEmpty(u.address.city))
                update = Builders<User>.Update.Set(usr => usr.address.city, u.address.city);
            if (!string.IsNullOrEmpty(u.address.zipcode)) 
                update = Builders<User>.Update.Set(usr => usr.address.zipcode, u.address.zipcode);
            if (!string.IsNullOrEmpty(u.address.geo.lat))
                update = Builders<User>.Update.Set(usr => usr.address.geo.lat, u.address.geo.lat);
            if (!string.IsNullOrEmpty(u.address.geo.lng))
                update = Builders<User>.Update.Set(usr => usr.address.geo.lng, u.address.geo.lng);
            if (!string.IsNullOrEmpty(u.phone))
                update = Builders<User>.Update.Set(usr => usr.phone, u.phone);
            if (!string.IsNullOrEmpty(u.website))
                update = Builders<User>.Update.Set(usr => usr.website, u.website);
            if (!string.IsNullOrEmpty(u.company.name))
                update = Builders<User>.Update.Set(usr => usr.company.name, u.company.name);
            if (!string.IsNullOrEmpty(u.company.bs))
                update = Builders<User>.Update.Set(usr => usr.company.bs, u.company.bs);
            if (!string.IsNullOrEmpty(u.company.catchPhrase))
                update = Builders<User>.Update.Set(usr => usr.company.catchPhrase, u.company.catchPhrase);

                // Update the document
                var personUpdateResult = _users.UpdateOne(filter, update);

            return u;
        }
        public bool Remove(string id)
        {
            // Create an equality filter
            var filter = Builders<User>.Filter.Eq(usr => usr._id, id);

            var DeleteResult = _users.DeleteOne(filter);
            return DeleteResult.IsAcknowledged;
        }
    }
}
