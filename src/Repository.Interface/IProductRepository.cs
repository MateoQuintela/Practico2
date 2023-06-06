using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repository.Interface.Action;

namespace Repository.Interface
{
    public interface IProductRepository : IReadRepository<Product, int>
    {
    }
}
