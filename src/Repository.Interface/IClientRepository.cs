using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interface.Action;
using Models;

namespace Repository.Interface
{
    public interface IClientRepository : IReadRepository<Client, int>
    {

    }
}
