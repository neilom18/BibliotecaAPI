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

        public IEnumerable<User> Get(int page, int size)
        {
           return _users.Values.Skip( page == 1 ? 0 : (page - 1) * size).Take(size);
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

        public User Create(User user)
        {
            user.Id = Guid.NewGuid();
            if (_users.TryAdd(user.Id, user))
                return user;

            throw new Exception("Não foi possivel cadastrar");
        }

        public bool Remove(Guid id)
        {
            return _users.Remove(id);
        }

        public User Update(Guid id, User user)
        {
            if (_users.TryGetValue(id, out var userToUpdate))
            {
                userToUpdate.Role = user.Role;
                userToUpdate.Username = user.Username;
                userToUpdate.Password = user.Password;

                return Get(id);
            }

            throw new Exception("Usuario não encontrado");
        }
    }
}
