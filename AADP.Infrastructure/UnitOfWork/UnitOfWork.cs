using AADP.Application.Port.Out;
using AADP.Infrastructure.Mapper.Factory;
using AADP.Infrastructure.Registry;
using AADP.Infrastructure.Repository;
using System.Collections.Concurrent;
using System.Data;

namespace AADP.Infrastructure.UnitOfWork
{
    public class UnitOfWork(IDbConnection db, IFactoryMapper mapper) : IUnitOfWork
    {
        private readonly IDbConnection _db = db;
        private readonly IFactoryMapper _mapper = mapper;
        private readonly ConcurrentDictionary<Type, object> _repositories = new();

        public IRepository<TDominio> Repository<TDominio>() where TDominio : class, new()
        {
            var dominioType = typeof(TDominio);

            if (!_repositories.ContainsKey(dominioType))
            {
                var infraType = InfraRegistry.GetInfraestructuraType<TDominio>();

                var repoType = typeof(Repository<,>).MakeGenericType(dominioType, infraType);
                var repoInstance = Activator.CreateInstance(repoType, _db, _mapper);

                _repositories[dominioType] = repoInstance!;
            }

            return (IRepository<TDominio>)_repositories[dominioType];
        }
    }
}
