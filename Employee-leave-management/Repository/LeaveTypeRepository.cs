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

        /* Synchronous functions */
        //public bool Create(LeaveType entity)
        //{
        //    _db.LeaveTypes.Add(entity);
        //    return Save();
        //}

        /* Asynchronous functions */
        public async Task<bool> Create(LeaveType entity)
        {
            await _db.LeaveTypes.AddAsync(entity);
            return await Save();
        }

        #endregion


        #region DELETE

        /* Synchronous functions */
        //public bool Delete(LeaveType entity)
        //{
        //    _db.LeaveTypes.Remove(entity);
        //    return Save();
        //}

        /* Asynchronous functions */
        public async Task<bool> Delete(LeaveType entity)
        {
             _db.LeaveTypes.Remove(entity);
            return await Save();
        }

        #endregion


        #region FIND ALL

        /* Synchronous functions */
        //public ICollection<LeaveType> FindAll()
        //{
        //    return _db.LeaveTypes.ToList();
        //}

        /* Asynchronous functions */
        public async Task<ICollection<LeaveType>> FindAll()
        {
            return await _db.LeaveTypes.ToListAsync();
        }

        #endregion


        #region FIND BY ID

        /* Synchronous functions */
        //public LeaveType FindById(int id)
        //{
        //    return _db.LeaveTypes.Find(id);
        //}

        /* Asynchronous functions */
        public async Task<LeaveType> FindById(int id)
        {
            return await _db.LeaveTypes.FindAsync(id);
        }

        #endregion


        #region GET EMPLOYEES BY LeaveType

        /* Synchronous functions */
        //public ICollection<LeaveType> GetEmployeesByLeaveType(int id)
        //{
        //    throw new NotImplementedException();
        //}

        /* Asynchronous functions */
        public async Task<ICollection<LeaveType>> GetEmployeesByLeaveType(int id)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region isExists

        /* Synchronous functions */
        //public bool isExists(int id)
        //{
        //    var exists = _db.LeaveTypes.Any(x => x.Id == id);
        //    return exists;
        //}

        /* Asynchronous functions */
        public async Task<bool> isExists(int id)
        {
            var exists = await _db.LeaveTypes.AnyAsync(x => x.Id == id);
            return exists;
        }

        #endregion


        #region Save

        /* Synchronous functions */
        //public bool Save()
        //{
        //    /* 1st way to write it - if we got more than 0 writen lines as changes */
        //    //var changes = _db.SaveChanges();
        //    //return changes > 0;

        //    /* 2nd shorter way to write it */
        //    return _db.SaveChanges() > 0;
        //}

        /* Asynchronous functions */
        public async Task<bool> Save()
        {
            /* 1st way to write it - if we got more than 0 writen lines as changes */
            //var changes = await _db.SaveChangesAsync();
            //return changes > 0;

            /* 2nd shorter way to write it */
            return await _db.SaveChangesAsync() > 0;
        }
        #endregion


        #region UPDATE
        /* Synchronous functions */
        //public bool Update(LeaveType entity)
        //{
        //    _db.LeaveTypes.Update(entity);
        //    return Save();
        //}

        /* Asynchronous functions */
        public async Task<bool> Update(LeaveType entity)
        {
            _db.LeaveTypes.Update(entity);
            return await Save();
        }

        #endregion

    }
}
