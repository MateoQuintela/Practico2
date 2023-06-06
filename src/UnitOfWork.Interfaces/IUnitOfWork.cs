using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        //Metodo de crear el 
        IUnitOfWorkAdapter Create();
        // Encargado de realizar la conexion a la base de datos y nos va permitir ingresar al repositorio. 

    }
}
