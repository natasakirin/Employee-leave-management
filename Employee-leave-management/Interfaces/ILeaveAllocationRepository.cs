using Employee_leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_leave_management.Interfaces
{
    public interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
        /* Synchronous functions */
        //bool CheckAllocation(int leavetypeid, string employeeid);
        //ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string employeeid);
        //LeaveAllocation GetLeaveAllocationsByEmployeeAndType(string employeeid, int leavetypeid);


        /* Asynchronous functions */
        Task<bool> CheckAllocation(int leavetypeid, string employeeid);
        Task<ICollection<LeaveAllocation>> GetLeaveAllocationsByEmployee(string employeeid);
        Task <LeaveAllocation> GetLeaveAllocationsByEmployeeAndType(string employeeid, int leavetypeid);


    }
}
