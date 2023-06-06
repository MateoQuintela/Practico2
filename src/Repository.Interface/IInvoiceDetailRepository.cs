using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repository.Interface
{
    public interface IInvoiceDetailRepository
    {
        void Create(IEnumerable<InvoiceDetail> model, int invoiceId);
        IEnumerable<InvoiceDetail> GetAllByInvoiceId(int invoiceId);
        void RemoveByInvoiceId(int invoiceId);
    }
}
