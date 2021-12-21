using BibliotecaAPI.Models;
using System.Collections.Generic;

namespace BibliotecaAPI.Repositories
{
    //
    //Ainda não implementado.
    //
    public class BaseRepository<TKey, TEntity> where TEntity : BaseEntity<TKey> where TKey : notnull
    {
        protected Dictionary<TKey, TEntity> _store;

        public BaseRepository()
        {
            _store ??= new Dictionary<TKey, TEntity>();
        }

        public bool Insert(TEntity entity)
        {
            return _store.TryAdd(entity.Id, entity);
        }

        public bool Update(TEntity entity)
        {
            if(_store.TryGetValue(entity.Id,out var old))
            {
                old = entity;
                return true;
            }

            return false;
        }

        public bool Delete(TEntity entity)
        {
            return _store.Remove(entity.Id);
        }

        public IEnumerable<TEntity> Get()
        {
            return _store.Values;
        }
    }
}
