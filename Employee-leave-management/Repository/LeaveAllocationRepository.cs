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
        /* Synchronous functions */
        //public bool CheckAllocation(int leavetypeid, string employeeid)
        //{
        //    var period = DateTime.Now.Year;
        //    return FindAll()
        //        .Where(x => x.EmployeeId == employeeid && x.LeaveTypeId == leavetypeid && x.Period == period)
        //        .Any();
        //}

        /* Asynchronous functions */

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
        /* Synchronous functions */
        //public bool Create(LeaveAllocation entity)
        //{
        //    _db.LeaveAllocations.Add(entity);
        //    return Save();
        //}

        /* Asynchronous functions */
        public async Task<bool> Create(LeaveAllocation entity)
        {
            await _db.LeaveAllocations.AddAsync(entity);
            return await Save();
        }

        #endregion


        #region DELETE

        /* Synchronous functions */
        //public bool Delete(LeaveAllocation entity)
        //{
        //    _db.LeaveAllocations.Remove(entity);
        //    return Save();
        //}

        /* Asynchronous functions */
        public async Task<bool> Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return await Save();
        }
        #endregion


        #region FIND ALL

        /* Synchronous functions */
        //public ICollection<LeaveAllocation> FindAll()
        //{
        //    return _db.LeaveAllocations
        //        .Include(x => x.LeaveType)
        //        .ToList();
        //}

        /* Asynchronous functions */
        public async Task<ICollection<LeaveAllocation>> FindAll()
        {
            return await _db.LeaveAllocations
                .Include(x => x.LeaveType)
                .ToListAsync();
        }
        #endregion


        #region FIND BY ID

        /* Synchronous functions */
        //public LeaveAllocation FindById(int id)
        //{
        //    var leaveAllocation = _db.LeaveAllocations
        //         .Include(x => x.LeaveType)
        //         .Include(x => x.Employee)
        //         .FirstOrDefault(x => x.Id == id);

        //    return leaveAllocation;
        //}

        /* Asynchronous functions */
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

        /* Synchronous functions */
        //public ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string employeeid)
        //{
        //    var period = DateTime.Now.Year;
        //    return FindAll()
        //        .Where(x => x.EmployeeId == employeeid && x.Period == period)
        //        .ToList();
        //}

        /* Asynchronous functions */
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

        /* Synchronous functions */

        //public LeaveAllocation GetLeaveAllocationsByEmployeeAndType(string employeeid, int leaveTypeId)
        //{
        //    var period = DateTime.Now.Year;
        //    return FindAll()
        //        .FirstOrDefault(x => x.EmployeeId == employeeid && x.Period == period && x.LeaveTypeId == leaveTypeId);
        //}

        /* Asynchronous functions */
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

        /* Synchronous functions */

        //public bool isExists(int id)
        //{
        //    var exists = _db.LeaveAllocations.Any(x => x.Id == id);
        //    return exists;
        //}

        /* Asynchronous functions */
        public async Task<bool> isExists(int id)
        {
            var exists = await _db.LeaveAllocations.AnyAsync(x => x.Id == id);
            return exists;
        }

        #endregion


        #region Save

        /* Synchronous functions */
        //public bool Save()
        //{
        //    return _db.SaveChanges() > 0;
        //}

        /* Asynchronous functions */
        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }
        #endregion


        #region UPDATE
        /* Synchronous functions */
        //public bool Update(LeaveAllocation entity)
        //{
        //    _db.LeaveAllocations.Update(entity);
        //    return Save();
        //}


        /* Asynchronous functions */
        public async Task<bool> Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return await Save();
        }
        #endregion
    }
}
