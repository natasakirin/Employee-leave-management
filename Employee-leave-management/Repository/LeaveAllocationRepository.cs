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

        public bool CheckAllocation(int leavetypeid, string employeeid)
        {
            var period = DateTime.Now.Year;
            return FindAll()
                .Where(x => x.EmployeeId == employeeid && x.LeaveTypeId == leavetypeid && x.Period == period)
                .Any();
        }

        public bool Create(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Add(entity);
            return Save();
        }

        public bool Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return Save();
        }

        public ICollection<LeaveAllocation> FindAll()
        {
            return _db.LeaveAllocations
                .Include(x => x.LeaveType)
                .ToList();
        }

        public LeaveAllocation FindById(int id)
        {
            var leaveAllocation = _db.LeaveAllocations
                 .Include(x => x.LeaveType)
                 .Include(x => x.Employee)
                 .FirstOrDefault(x => x.Id == id);

            return leaveAllocation;
        }

        public ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string employeeid)
        {
            var period = DateTime.Now.Year;
            return FindAll()
                .Where(x => x.EmployeeId == employeeid && x.Period == period)
                .ToList();
        }


        public LeaveAllocation GetLeaveAllocationsByEmployeeAndType(string employeeid, int leaveTypeId)
        {
            var period = DateTime.Now.Year;
            return FindAll()
                .FirstOrDefault(x => x.EmployeeId == employeeid && x.Period == period && x.LeaveTypeId == leaveTypeId);
        }


        public bool isExists(int id)
        {
            var exists = _db.LeaveAllocations.Any(x => x.Id == id);
            return exists;
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return Save();
        }
    }
}
