using Employee_leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_leave_management.Interfaces
{
    interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
    }
}
