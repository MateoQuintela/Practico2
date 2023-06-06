using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Repository.Interface.Action;

namespace Repository.Interface
{
    public interface IInvoiceRepository : IReadRepository<Invoice, int>, ICreateRepository<Invoice>, IUpdateRepository<Invoice>, IRemoveRepository<int>
    {

    }

}
