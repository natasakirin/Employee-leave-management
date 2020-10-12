using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_leave_management.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {

        /* Synchronous functions */
        //ICollection<T> FindAll();
        //T FindById(int id);
        //bool isExists(int id);
        //bool Create(T entity);
        //bool Update(T entity);
        //bool Delete(T entity);
        //bool Save();



        /* Asynchronous functions */
        Task<ICollection<T>> FindAll();
        Task<T> FindById(int id);
        Task<bool> isExists(int id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();
    }
}
