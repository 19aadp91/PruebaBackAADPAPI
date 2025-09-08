using AADP.Domain.Model;

namespace AADP.Application.Port.In
{
    public interface IProductoServices
    {
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<Producto?> GetByIdAsync(int id);
        Task<int> InsertAsync(Producto entity);
        Task<bool> UpdateAsync(Producto entity);
        Task<bool> DeleteAsync(int id);
    }
}
