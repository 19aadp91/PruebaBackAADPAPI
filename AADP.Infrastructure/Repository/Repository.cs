using AADP.Application.Port.Out;
using AADP.Domain.Model;
using AADP.Infrastructure.Mapper;
using AADP.Infrastructure.Mapper.Factory;
using Dapper;
using System.Data;

namespace AADP.Infrastructure.Repository
{
    public class Repository<TDominio, TInfraestructura>(IDbConnection db, IFactoryMapper mapper) : IRepository<TDominio>where TDominio : class, new() where TInfraestructura : class, new()
    {
        private readonly IDbConnection _db = db;
        private readonly IEntityMapper<TInfraestructura, TDominio> _mapperToDominio = mapper.Mapper<TInfraestructura, TDominio>();
        private readonly IEntityMapper<TDominio, TInfraestructura> _mapperToEntity = mapper.Mapper<TDominio, TInfraestructura>();

        public async Task<IEnumerable<TDominio>> GetAllAsync()
        {
            var sp = $"sp_{typeof(TInfraestructura).Name}_GetAll";
            var data = await _db.QueryAsync<TInfraestructura>(sp, commandType: CommandType.StoredProcedure);
            return data.Select(_mapperToDominio.MapTo).ToList();
        }

        public async Task<TDominio?> GetByIdAsync(int id)
        {
            var sp = $"sp_{typeof(TInfraestructura).Name}_GetById";
            var result = await _db.QuerySingleOrDefaultAsync<TInfraestructura>(sp, new { Id = id }, commandType: CommandType.StoredProcedure);
            return result is null ? null : _mapperToDominio.MapTo(result);
        }

        public async Task<int> InsertAsync(TDominio entity)
        {
            var sp = $"sp_{typeof(TInfraestructura).Name}_Insert";
            var infra = _mapperToEntity.MapTo(entity);
            return await _db.ExecuteScalarAsync<int>(sp, infra, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateAsync(TDominio entity)
        {
            var sp = $"sp_{typeof(TInfraestructura).Name}_Update";
            var infra = _mapperToEntity.MapTo(entity);
            var rows = await _db.ExecuteAsync(sp, infra, commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sp = $"sp_{typeof(TInfraestructura).Name}_Delete";
            var rows = await _db.ExecuteAsync(sp, new { Id = id }, commandType: CommandType.StoredProcedure);
            return rows > 0;
        }

        public async Task<Usuario?> LoginAsync(string correo, string claveHash)
        {
            var sp = "sp_AADP_Usuario_Login";
            var parameters = new { CorreoElectronico = correo, ClaveHash = claveHash };

            return await _db.QuerySingleOrDefaultAsync<Usuario>(
                sp,
                parameters,
                commandType: CommandType.StoredProcedure);
        }
    }
}
