using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADP.Infrastructure.Mapper
{
    public interface IEntityMapper<TSrc, TDest> where TSrc : class where TDest : class
    {
        TDest MapTo(TSrc domain);
    }
}
