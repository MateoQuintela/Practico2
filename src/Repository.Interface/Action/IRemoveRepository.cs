using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface.Action
{
    public interface IRemoveRepository<T>
    {
        void Remove(T id);
    }

}
