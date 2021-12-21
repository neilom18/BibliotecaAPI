using BibliotecaAPI.DTOs.Query;
using BibliotecaAPI.DTOs.ResultDTO;
using BibliotecaAPI.ExtensionsMethod;
using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaAPI.Repositories
{
    public class UsersRepository
    {
        private readonly Dictionary<Guid, User> _users;

        public UsersRepository()
        {
            _users = new Dictionary<Guid, User>();
        }

        public IEnumerable<User> Get(UserQuery parameters)
        {
            var users = _users.Values.WhereIf(parameters.Name, x => x.Username == parameters.Name)
                .WhereIf(parameters.Document, x => x.Document == parameters.Document)
                .WhereIf(parameters.Age, x => x.Age == parameters.Age);
            return users.Paginaze(parameters.Page, parameters.Size);
        }

        public User Get(Guid id)
        {
            if(_users.TryGetValue(id, out var user))
                return user;

            throw new Exception("Usuario não encontrado");
        }

        public User GetbyUsername(string username)
        {
            return _users.Values.Where(u => u.Username == username).FirstOrDefault();
        }

        public bool GetbyUsernameBOOL(string username)
        {
            return _users.Values.Where(u => u.Username == username).Any();
        }

        public User Create(User user)
        {
            if (_users.TryAdd(user.Id, user))
                return user;

            throw new Exception("Não foi possivel cadastrar");
        }

        /*public User Update(User user, Guid id)
        {
            if(_users.TryGetValue(id, out var userToUpdate))
            {
                userToUpdate
            }
        }*/

        public bool Remove(Guid id)
        {
            return _users.Remove(id);
        }

        public void ChangePassword(Guid id, string newPassword)
        {
            var user = Get(id);
            if(_users.TryGetValue(user.Id, out user))
                user.SetPassword(newPassword);
        }
        public User Login(string username, string password)
        {
            var user = _users.Values.Where(u => u.Username == username && u.Password == password).SingleOrDefault();
            return user;
        }
    }
}
