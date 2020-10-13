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

        public async Task<bool> Create(LeaveRequest entity)
        {
            await _db.LeaveRequests.AddAsync(entity);
            return await Save();
        }

        #endregion


        #region DELETE

        public async Task<bool> Delete(LeaveRequest entity)
        {
            _db.LeaveRequests.Remove(entity);
            return await Save();
        }
        #endregion


        #region FIND ALL

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

        public async Task<ICollection<LeaveRequest>> GetLeaveRequestsByEmployee(string employeeid)
        {
            var leaveRequests = await FindAll();
            return leaveRequests
                .Where(x => x.RequestingEmployeeId == employeeid)
                .ToList();

        }
        #endregion


        #region isExists

        public async Task<bool> isExists(int id)
        {
            var exists = await _db.LeaveRequests.AnyAsync(x => x.Id == id);
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

        public async Task<bool> Update(LeaveRequest entity)
        {
            _db.LeaveRequests.Update(entity);
            return await Save();
        }
        #endregion
    }
}
