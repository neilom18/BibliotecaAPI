using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaAPI.Repositories
{
    public class EmployeerRepository
    {
        private readonly Dictionary<Guid, Employeer> _employeer;

        public EmployeerRepository()
        {
            _employeer = new Dictionary<Guid, Employeer>();
        }

        public Employeer Create(Employeer employeer)
        {
            employeer.SetCreatedDate(DateTime.Now);
            if (_employeer.TryAdd(employeer.User.Id, employeer))
                return employeer;

            throw new Exception("Não foi possivel cadastrar");
        }

        public bool Remove(Guid id)
        {
            return _employeer.Remove(id);
        }
    }
}
