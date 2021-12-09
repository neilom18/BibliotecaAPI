using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;

namespace BibliotecaAPI.Repositories
{
    public class ClientsRepository
    {
        private readonly Dictionary<Guid, Client> _clients;

        public ClientsRepository()
        {
            _clients = new Dictionary<Guid, Client>();
        }

        public void Create(Client client)
        {
            if (!_clients.TryAdd(client.User.Id, client))
                throw new Exception();
        }
    }
}
