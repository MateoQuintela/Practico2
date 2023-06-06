using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Repository.Interface;
using Repository.SqlServer;
using UnitOfWork.Interfaces;

namespace UnitOfWork.SqlServer
{
    public class UnitOfWorkSqlServerRepository: IUnitOfWorkRepository
    {

        public IProductRepository ProductRepository { get; }
        public IClientRepository ClientRepository { get; }
        public IInvoiceRepository InvoiceRepository { get; }
        public IInvoiceDetailRepository InvoiceDetailRepository { get; }

        public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction)
        {
            ClientRepository = new ClientRepository(context, transaction);
            ProductRepository = new ProductRepository(context, transaction);
            InvoiceRepository = new InvoiceRepository(context, transaction);
            InvoiceDetailRepository = new InvoiceDetailRepository(context, transaction);
        }

    }
}
