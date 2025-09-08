using AADP.Domain.Model;

namespace AADP.Application.Port.In
{
    public interface IUserServices
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task<int> InsertAsync(Usuario entity);
        Task<bool> UpdateAsync(Usuario entity);
        Task<bool> DeleteAsync(int id);
    }
}
