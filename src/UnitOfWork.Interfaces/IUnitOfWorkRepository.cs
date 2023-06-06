using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interface;

namespace UnitOfWork.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        IProductRepository ProductRepository { get; }
        IClientRepository ClientRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }
        IInvoiceDetailRepository InvoiceDetailRepository { get; }
    }
}
