using Employee_leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_leave_management.Interfaces
{
    public interface ILeaveRequestRepository : IRepositoryBase<LeaveRequest>
    {
        Task <ICollection<LeaveRequest>> GetLeaveRequestsByEmployee(string employeeid);
    }
}
