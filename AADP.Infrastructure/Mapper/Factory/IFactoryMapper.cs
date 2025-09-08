using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADP.Infrastructure.Mapper.Factory
{
    public interface IFactoryMapper
    {
        IEntityMapper<TSrc, TDest> Mapper<TSrc, TDest>() where TSrc : class, new() where TDest : class, new();
    }
}
