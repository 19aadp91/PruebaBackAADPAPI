using AADP.Application.Port.In;
using AADP.Application.Port.Out;
using AADP.Domain.Model;

namespace AADP.Application.service
{
    public class ProductoServices(IUnitOfWork unitOfWork) : IProductoServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> DeleteAsync(int id) => await _unitOfWork.Repository<Producto>().DeleteAsync(id);

        public async Task<IEnumerable<Producto>> GetAllAsync() => await _unitOfWork.Repository<Producto>().GetAllAsync();

        public async Task<Producto?> GetByIdAsync(int id) => await _unitOfWork.Repository<Producto>().GetByIdAsync(id);

        public async Task<int> InsertAsync(Producto entity) => await _unitOfWork.Repository<Producto>().InsertAsync(entity);

        public async Task<bool> UpdateAsync(Producto entity) => await _unitOfWork.Repository<Producto>().UpdateAsync(entity);
    }
}
