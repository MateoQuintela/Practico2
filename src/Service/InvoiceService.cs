using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Common;
using Models;
using UnitOfWork.Interfaces;

namespace Service
{

    public interface IInvoiceService
    {
        IEnumerable<Invoice> GetAll();
        Invoice Get(int id);
        void Create(Invoice model);
        void Update(Invoice model);
        void Delete(int id);
    }

    public class InvoiceService : IInvoiceService
    {
        private IUnitOfWork _unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Invoice> GetAll()
        {
            using (var context = _unitOfWork.Create())
            {
                var records = context.Repositories.InvoiceRepository.GetAll();

                foreach (var record in records)
                {
                    record.Client = context.Repositories.ClientRepository.Get(record.ClientId);
                    record.Detail = context.Repositories.InvoiceDetailRepository.GetAllByInvoiceId(record.Id);

                    foreach (var item in record.Detail)
                    {
                        item.Product = context.Repositories.ProductRepository.Get(item.ProductId);
                    }
                }

                return records;
            }
        }

        public Invoice Get(int id)
        {
            using (var context = _unitOfWork.Create())
            {
                var result = context.Repositories.InvoiceRepository.Get(id);

                result.Client = context.Repositories.ClientRepository.Get(result.ClientId);
                result.Detail = context.Repositories.InvoiceDetailRepository.GetAllByInvoiceId(result.Id);

                foreach (var item in result.Detail)
                {
                    item.Product = context.Repositories.ProductRepository.Get(item.ProductId);
                }

                return result;
            }
        }

        public void Create(Invoice model)
        {
            PrepareOrder(model);

            using (var context = _unitOfWork.Create())
            {
                // Header
                context.Repositories.InvoiceRepository.Create(model);

                // Detail
                context.Repositories.InvoiceDetailRepository.Create(model.Detail, model.Id);

                // Confirm changes
                context.SaveChanges();
            }
        }

        public void Update(Invoice model)
        {
            PrepareOrder(model);

            using (var context = _unitOfWork.Create())
            {
                // Header
                context.Repositories.InvoiceRepository.Update(model);

                // Detail
                context.Repositories.InvoiceDetailRepository.RemoveByInvoiceId(model.Id);
                context.Repositories.InvoiceDetailRepository.Create(model.Detail, model.Id);

                // Confirm changes
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = _unitOfWork.Create())
            {
                context.Repositories.InvoiceRepository.Remove(id);

                // Confirm changes
                context.SaveChanges();
            }
        }

        private void PrepareOrder(Invoice model)
        {
            foreach (var detail in model.Detail)
            {
                detail.Total = detail.Quantity * detail.Price;
                detail.SubTotal = detail.Total / (1 + Parameters.IvaRate);
                detail.Iva = detail.Total - detail.SubTotal;
            }

            model.Total = model.Detail.Sum(x => x.Total);
            model.Iva = model.Detail.Sum(x => x.Iva);
            model.SubTotal = model.Detail.Sum(x => x.SubTotal);
        }
    }
    //public class InvoiceService

    /*public List<Invoice> GetAll()
    {
        var result = new List<Invoice>();

        using (var context = new SqlConnection(Parameters.ConnectionString))
        {
            context.Open();

            var command = new SqlCommand("SELECT * FROM invoices", context);

            using (var reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    var invoice = new Invoice
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Iva = Convert.ToDecimal(reader["iva"]),
                        SubTotal = Convert.ToDecimal(reader["subtotal"]),
                        Total = Convert.ToDecimal(reader["total"]),
                        ClientId = Convert.ToInt32(reader["clientId"])
                    };

                    result.Add(invoice);


                }
            }
            ///set aditional propiedades
            foreach (var invoice in result)
            {
                //cliente
                SetClient(invoice, context);
                //Ditail
                SetDetail(invoice, context);
            }
        }
        return result;
    }



    private void SetClient(Invoice invoice , SqlConnection context)
    {
        var command = new SqlCommand("SELECT * FROM clients WHERE id = @clientId ",context);
        command.Parameters.AddWithValue("@clientId", invoice.ClientId);

        using (var reader = command.ExecuteReader())
        {

            reader.Read();

            invoice.Client = new Client
            {
                Id= Convert.ToInt32(reader["id"]),
                Name= reader["name"].ToString()
            };
        }

    }

    private void SetDetail(Invoice invoice, SqlConnection context)
    {
        var command = new SqlCommand("SELECT * FROM invoicedetail WHERE invoiceId = @invoiceId ", context);
        command.Parameters.AddWithValue("@invoiceId", invoice.Id);

        using (var reader = command.ExecuteReader())
        {

            while (reader.Read())
            {
                invoice.Detail.Add(new InvoiceDetail
                {
                    Id = Convert.ToInt32(reader["id"]),
                    ProductId = Convert.ToInt32(reader["productId"]),
                    Quantity = Convert.ToInt32(reader["quantity"]),
                    Iva = Convert.ToDecimal(reader["subtotal"]),
                    SubTotal = Convert.ToDecimal(reader["subtotal"]),
                    Total = Convert.ToDecimal(reader["total"])
                });
            }

        }
        foreach(var detail  in invoice.Detail)
        {
            SetProduct(detail, context);
        }

    }
    private void SetProduct(InvoiceDetail detail, SqlConnection context)
    {
        var command = new SqlCommand("SELECT * FROM products WHERE id = @productId ",context );
        command.Parameters.AddWithValue("@productId", detail.ProductId);

        using (var reader = command.ExecuteReader())
        {

            reader.Read();

            detail.Product = new Product
            {
                Id = Convert.ToInt32(reader["id"]),
                Price = Convert.ToDecimal(reader["price"]),
                Name = reader["name"].ToString()
            };
        }

    }

    public Invoice Get(int id)
    {
        var result = new Invoice();
        using (var context = new SqlConnection (Parameters.ConnectionString))
        {
            context.Open();


            var command = new SqlCommand("SELECT * FROM invoices WHERE id = @id", context);
            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                result.Id = Convert.ToInt32(reader["id"]);
                result.Iva = Convert.ToDecimal(reader["iva"]);
                result.SubTotal = Convert.ToDecimal(reader["subtotal"]);
                result.Total = Convert.ToDecimal(reader["total"]);
                result.ClientId = Convert.ToInt32(reader["clienteId"]);
            }
            //client 
            SetClient(result, context);

            //Detail
            SetDetail(result, context); 

        };

        return result;
    }


    public void Create(Invoice model)
    {
        PrepararOrder(model);
    }

    public void PrepararOrder(Invoice model)
    {
        foreach (var detail in model.Detail) 
        {
            detail.Total = detail.Quantity * detail.Price;
            detail.Iva = detail.Total * Parameters.IvaRate;
            detail.SubTotal = detail.Total - detail.Iva;
        }

        model.Total = model.Detail.Sum(x => x.Total);
        model.Iva = model.Detail.Sum(X=> X.Iva);
        model.SubTotal = model.Detail.Sum(x => x.SubTotal);
    }
}*/
}

