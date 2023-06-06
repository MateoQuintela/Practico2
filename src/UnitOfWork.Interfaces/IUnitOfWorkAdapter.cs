using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.Interfaces
{
    public interface IUnitOfWorkAdapter : IDisposable

    //IDisposable implemeta porque ese IUOWA va estar dento del scoutUsing 
    {
        //Tienw una propiedad que nos va a permitir acceder a los repositorios
        IUnitOfWorkRepository Repositories { get; }

        // Tiene un metodo saveChanges : Confirma los cabios , update , delet , insert
        void SaveChanges();
    }
}
