using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IGenericRepository<T>
    {
        List<T> GetListado();
        T GetById(int id);
        void Insert(T data);
        void Update(T data);
        void DeleteById(int id);
    }
}
