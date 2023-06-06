using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Parameters
    {                                          //Direcion ip o extancia de la maquina     Catalog = nombre de la base de datos   Security = le estamos diciendo que use la autentificacion de windows                         
        public const string ConnectionString = "Server=DESKTOP-HFDSG6E;Initial Catalog=KodotiSells;Integrated Security=true;";
        public const decimal IvaRate = 0.18m;
    }
}
