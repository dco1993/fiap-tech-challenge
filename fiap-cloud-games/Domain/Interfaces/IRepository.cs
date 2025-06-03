using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T>
    {
        IList<T> GetAll();
        T GetById(int id);
        T Add(T entidade);
        void Update(T entidade);
        void Delete(int id);

    }
}
