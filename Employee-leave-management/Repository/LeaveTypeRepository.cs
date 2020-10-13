using Employee_leave_management.Data;
using Employee_leave_management.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_leave_management.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        #region CREATE

        public async Task<bool> Create(LeaveType entity)
        {
            await _db.LeaveTypes.AddAsync(entity);
            return await Save();
        }

        #endregion


        #region DELETE

        public async Task<bool> Delete(LeaveType entity)
        {
             _db.LeaveTypes.Remove(entity);
            return await Save();
        }

        #endregion


        #region FIND ALL

        public async Task<ICollection<LeaveType>> FindAll()
        {
            return await _db.LeaveTypes.ToListAsync();
        }

        #endregion


        #region FIND BY ID

        public async Task<LeaveType> FindById(int id)
        {
            return await _db.LeaveTypes.FindAsync(id);
        }

        #endregion


        #region GET EMPLOYEES BY LeaveType

        public async Task<ICollection<LeaveType>> GetEmployeesByLeaveType(int id)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region isExists

        public async Task<bool> isExists(int id)
        {
            var exists = await _db.LeaveTypes.AnyAsync(x => x.Id == id);
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

        public async Task<bool> Update(LeaveType entity)
        {
            _db.LeaveTypes.Update(entity);
            return await Save();
        }

        #endregion

    }
}
