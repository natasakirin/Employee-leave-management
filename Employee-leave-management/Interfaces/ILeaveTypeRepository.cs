using Employee_leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_leave_management.Interfaces
{
    public interface ILeaveTypeRepository : IRepositoryBase<LeaveType>
    {
        /* Synchronous functions */
        //ICollection<LeaveType> GetEmployeesByLeaveType(int id);


        /* Asynchronous functions */
        Task<ICollection<LeaveType>> GetEmployeesByLeaveType(int id);
    }
}
