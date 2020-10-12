using Employee_leave_management.Data;
using Employee_leave_management.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_leave_management.Repository
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        #region CREATE
        /* Synchronous functions */
        //public bool Create(LeaveRequest entity)
        //{
        //    _db.LeaveRequests.Add(entity);
        //    return Save();
        //}

        /* Asynchronous functions */
        public async Task<bool> Create(LeaveRequest entity)
        {
            await _db.LeaveRequests.AddAsync(entity);
            return await Save();
        }

        #endregion


        #region DELETE

        /* Synchronous functions */
        //public bool Delete(LeaveRequest entity)
        //{
        //    _db.LeaveRequests.Remove(entity);
        //    return Save();
        //}

        /* Asynchronous functions */
        public async Task<bool> Delete(LeaveRequest entity)
        {
            _db.LeaveRequests.Remove(entity);
            return await Save();
        }
        #endregion


        #region FIND ALL

        /* Synchronous functions */
        //public ICollection<LeaveRequest> FindAll()
        //{
        //    return _db.LeaveRequests
        //        .Include(x => x.RequestingEmployee)
        //        .Include(x => x.ApprovedBy)
        //        .Include(x => x.LeaveType)
        //        .ToList();
        //}

        /* Asynchronous functions */
        public async Task<ICollection<LeaveRequest>> FindAll()
        {
            return await _db.LeaveRequests
                .Include(x => x.RequestingEmployee)
                .Include(x => x.ApprovedBy)
                .Include(x => x.LeaveType)
                .ToListAsync();
        }

        #endregion


        #region FIND BY ID

        /* Synchronous functions */
        //public LeaveRequest FindById(int id)
        //{
        //    return _db.LeaveRequests
        //        .Include(x => x.RequestingEmployee)
        //        .Include(x => x.ApprovedBy)
        //        .Include(x => x.LeaveType)
        //        .FirstOrDefault(x => x.Id == id);
        //}

        /* Asynchronous functions */
        public async Task<LeaveRequest> FindById(int id)
        {
            return await _db.LeaveRequests
                .Include(x => x.RequestingEmployee)
                .Include(x => x.ApprovedBy)
                .Include(x => x.LeaveType)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion


        #region GetLeaveRequestsByEmployee

        /* Synchronous functions */
        //public ICollection<LeaveRequest> GetLeaveRequestsByEmployee(string employeeid)
        //{
        //    var leaveRequests = FindAll()
        //        .Where(x => x.RequestingEmployeeId == employeeid)
        //        .ToList();
        //    return leaveRequests;
        //}

        /* Synchronous functions */
        public async Task<ICollection<LeaveRequest>> GetLeaveRequestsByEmployee(string employeeid)
        {
            var leaveRequests = await FindAll();
            return leaveRequests
                .Where(x => x.RequestingEmployeeId == employeeid)
                .ToList();

        }
        #endregion


        #region isExists

        /* Synchronous functions */
        //public bool isExists(int id)
        //{
        //    var exists = _db.LeaveRequests.Any(x => x.Id == id);
        //    return exists;
        //}

        /* Asynchronous functions */
        public async Task<bool> isExists(int id)
        {
            var exists = await _db.LeaveRequests.AnyAsync(x => x.Id == id);
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
        //public bool Update(LeaveRequest entity)
        //{
        //    _db.LeaveRequests.Update(entity);
        //    return Save();
        //}

        /* Asynchronous functions */
        public async Task<bool> Update(LeaveRequest entity)
        {
            _db.LeaveRequests.Update(entity);
            return await Save();
        }
        #endregion
    }
}
