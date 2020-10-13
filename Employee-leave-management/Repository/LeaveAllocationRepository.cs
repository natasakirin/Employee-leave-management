using Employee_leave_management.Data;
using Employee_leave_management.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_leave_management.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        #region CHECK ALLOCATION

        public async Task<bool> CheckAllocation(int leavetypeid, string employeeid)
        {
            var period = DateTime.Now.Year;
            var allocations = await FindAll();
            return allocations
                .Where(x => x.EmployeeId == employeeid 
                    && x.LeaveTypeId == leavetypeid 
                    && x.Period == period)
                .Any();
        }

        #endregion


        #region CREATE

        public async Task<bool> Create(LeaveAllocation entity)
        {
            await _db.LeaveAllocations.AddAsync(entity);
            return await Save();
        }

        #endregion


        #region DELETE

        public async Task<bool> Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return await Save();
        }
        #endregion


        #region FIND ALL

        public async Task<ICollection<LeaveAllocation>> FindAll()
        {
            return await _db.LeaveAllocations
                .Include(x => x.LeaveType)
                .ToListAsync();
        }
        #endregion


        #region FIND BY ID

        public async Task<LeaveAllocation> FindById(int id)
        {
            var leaveAllocation = await _db.LeaveAllocations
                 .Include(x => x.LeaveType)
                 .Include(x => x.Employee)
                 .FirstOrDefaultAsync(x => x.Id == id);

            return leaveAllocation;
        }
        #endregion


        #region GetLeaveAllocationsByEmployee

        public async Task<ICollection<LeaveAllocation>> GetLeaveAllocationsByEmployee(string employeeid)
        {
            var period = DateTime.Now.Year;
            var allocations = await FindAll();
            return allocations
                .Where(x => x.EmployeeId == employeeid && x.Period == period)
                .ToList();
        }

        #endregion


        #region GetLeaveAllocationsByEmployeeAndType

        public async Task<LeaveAllocation> GetLeaveAllocationsByEmployeeAndType(string employeeid, int leaveTypeId)
        {
            var period = DateTime.Now.Year;
            var allocations = await FindAll();
            return allocations
                .FirstOrDefault(x => x.EmployeeId == employeeid 
                                && x.Period == period 
                                && x.LeaveTypeId == leaveTypeId);
        }

        #endregion


        #region isExists

        public async Task<bool> isExists(int id)
        {
            var exists = await _db.LeaveAllocations.AnyAsync(x => x.Id == id);
            return exists;
        }

        #endregion


        #region Save

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }
        #endregion


        #region UPDATE

        public async Task<bool> Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return await Save();
        }
        #endregion
    }
}
