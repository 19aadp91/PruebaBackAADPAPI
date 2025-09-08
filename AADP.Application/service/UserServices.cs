using AADP.Application.Port.In;
using AADP.Application.Port.Out;
using AADP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADP.Application.service
{
    public class UserServices(IUnitOfWork unitOfWork) : IUserServices
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;


        public async Task<bool> DeleteAsync(int id) =>   await _unitOfWork.Repository<Usuario>().DeleteAsync(id);

        public async Task<IEnumerable<Usuario>> GetAllAsync() => await _unitOfWork.Repository<Usuario>().GetAllAsync();

        public async Task<Usuario?> GetByIdAsync(int id) => await _unitOfWork.Repository<Usuario>().GetByIdAsync(id);

        public async Task<int> InsertAsync(Usuario entity) => await _unitOfWork.Repository<Usuario>().InsertAsync(entity);

        public async Task<bool> UpdateAsync(Usuario entity) => await _unitOfWork.Repository<Usuario>().UpdateAsync(entity);
    }
}
