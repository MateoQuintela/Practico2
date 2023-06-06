using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Invoice
    {
        public int Id { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        // IEnumerable Expone un enumerador, que admite una iteración simple sobre una colección no genérica.
        //public IEnumerable<InvoiceDetail> Detail { get; set; }

        public List<InvoiceDetail> Detail { get; set; }

        public decimal Iva { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public Invoice()
        {
            Detail = new List<InvoiceDetail>();
        }
    }
}
