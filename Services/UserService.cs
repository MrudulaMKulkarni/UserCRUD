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
        private readonly Repository _repository;
        public UserService(IUserDatabaseSettings settings)
        {
            _repository = new Repository(settings);
        }
        public List<User> Get()
        {
            List<User> users;
            users = _repository.Get();
            return users;
        }
        public User Get(string id)
        {
            User user;
            user = _repository.Get(id);
            return user;
        }

        public User Create(User u)
        {
            User user;
            user  = _repository.Create(u);
            return user;
        }

        public User Put(string id, User u)
        {
            User user = _repository.Put(id, u);
            return u;
        }
        public bool Remove(string id)
        {
            bool DeleteResult = _repository.Remove(id);
            return DeleteResult;
        }
    }
}
