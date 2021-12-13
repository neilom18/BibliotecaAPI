using BibliotecaAPI.DTOs.ResultDTO;
using BibliotecaAPI.Models;
using BibliotecaAPI.Models.Login;
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

        public IEnumerable<User> Get(string cpf,int age, string name,int page, int size)
        {
            var users = _users.Values.Where(u => u.Username == name && u.Age == age && u.CPF == cpf);
            return users.Skip( page == 1 ? 0 : (page - 1) * size).Take(size);
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
            user.Id = Guid.NewGuid();
            if (_users.TryAdd(user.Id, user))
                return user;

            throw new Exception("Não foi possivel cadastrar");
        }

        public bool Remove(Guid id)
        {
            return _users.Remove(id);
        }

        public void ChangePassword(Guid id, string newPassword)
        {
            var user = Get(id);
            if(_users.TryGetValue(user.Id, out user))
                user.Password = newPassword;
        }
        public User Login(string username, string password)
        {
            var user = _users.Values.Where(u => u.Username == username && u.Password == password).SingleOrDefault();
            return user;
        }
    }
}
