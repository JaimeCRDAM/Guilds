using Cassandra.Data.Linq;
using Cassandra.Mapping;
using Cassandra;
using GenericTools.Database;
using System.Linq.Expressions;
using Guilds.Models;
using System;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Guilds.Repositories
{
    public class GuildRepository<T> : IBaseRepository<T> where T : BaseEntity
    {

        private Cluster _cluster;
        private Cassandra.ISession _session;
        private Table<T> _table;
        private IMapper _mapper;

        public GuildRepository(CassandraBuilder cassandraBuild)
        {
            _cluster = cassandraBuild.myCluster;
            _session = _cluster.Connect();
            _session.CreateKeyspaceIfNotExists("guilds");
            _session.ChangeKeyspace("guilds");
            _mapper = new Mapper(_session);
            MappingConfiguration.Global.Define<MyMappings>();
            MyMappings.mapClasses(_session);
            _table = new Table<T>(_session);
            _table.CreateIfNotExists();
        }

        public void Add(T entity)
        {
            //add the entity to the database
            _table.Insert(entity).Execute();
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            _mapper.Delete(entity);
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _table.Where(predicate).Execute();
        }

        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _table.Select(x => x).Execute();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public T GetById(Guid id)
        {
            return _table.Where(x => x.Id == id).FirstOrDefault().Execute();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            //update the entity in the database
            _mapper.Update(entity);
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
