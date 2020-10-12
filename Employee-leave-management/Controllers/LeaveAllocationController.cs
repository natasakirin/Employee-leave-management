using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Employee_leave_management.Data;
using Employee_leave_management.Interfaces;
using Employee_leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Employee_leave_management.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LeaveAllocationController : Controller
    {
        private readonly ILeaveTypeRepository _leaverepo;
        private readonly ILeaveAllocationRepository _leaveallocationrepo;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public LeaveAllocationController(
            ILeaveTypeRepository leaverepo,
            ILeaveAllocationRepository leaveallocationrepo,
            IMapper mapper,
            UserManager<Employee> userManager
        )
        {
            _leaverepo = leaverepo;
            _leaveallocationrepo = leaveallocationrepo;
            _mapper = mapper;
            _userManager = userManager;
        }


        #region Index (FindAll)
        // GET: LeaveAllocationController

        /* Synchronous functions */
        //public ActionResult Index()
        //{
        //    var leavetypes = _leaverepo.FindAll().ToList();
        //    var mappedLeaveTypes = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leavetypes);
        //    var model = new CreateLeaveAllocationVM
        //    {
        //        LeaveTypes = mappedLeaveTypes,
        //        NumberUpdated = 0
        //    };

        //    return View(model);
        //}

        /* Asynchronous functions */
        public async Task<ActionResult> Index()
        {
            var leavetypes = await _leaverepo.FindAll();
            var mappedLeaveTypes = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leavetypes.ToList());
            var model = new CreateLeaveAllocationVM
            {
                LeaveTypes = mappedLeaveTypes,
                NumberUpdated = 0
            };

            return View(model);
        }
        #endregion


        #region SetLeave
        /* Synchronous functions */
        //public ActionResult SetLeave(int id)
        //{
        //    var leavetype = _leaverepo.FindById(id);
        //    var employees = _userManager.GetUsersInRoleAsync("Employee").Result;

        //    foreach(var emp in employees)
        //    {
        //        /* in case that leaveallcoation for this type of leave for this employee already exists */
        //        if(_leaveallocationrepo.CheckAllocation(id, emp.Id))
        //        {
        //            continue;
        //        }


        //        var allocation = new LeaveAllocationVM
        //        {
        //            DateCreated = DateTime.Now,
        //            EmployeeId = emp.Id,
        //            LeaveTypeId = id,
        //            NumberOfDays = leavetype.DefaultDays,
        //            Period = DateTime.Now.Year
        //        };

        //        var leaveallocation = _mapper.Map<LeaveAllocation>(allocation);

        //        _leaveallocationrepo.Create(leaveallocation);
        //    }

        //    return RedirectToAction(nameof(Index));
        //}

        /* Asynchronous functions */
        public async Task<ActionResult> SetLeave(int id)
        {
            var leavetype = await _leaverepo.FindById(id);
            var employees = await _userManager.GetUsersInRoleAsync("Employee");

            foreach (var emp in employees)
            {
                /* in case that leaveallcoation for this type of leave for this employee already exists */
                if (await _leaveallocationrepo.CheckAllocation(id, emp.Id))
                {
                    continue;
                }


                var allocation = new LeaveAllocationVM
                {
                    DateCreated = DateTime.Now,
                    EmployeeId = emp.Id,
                    LeaveTypeId = id,
                    NumberOfDays = leavetype.DefaultDays,
                    Period = DateTime.Now.Year
                };

                var leaveallocation = _mapper.Map<LeaveAllocation>(allocation);

                await _leaveallocationrepo.Create(leaveallocation);
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region ListEmployees
        /* Synchronous functions */
        //public ActionResult ListEmployees()
        //{
        //    var employees = _userManager.GetUsersInRoleAsync("Employee").Result;
        //    var model = _mapper.Map<List<EmployeeVM>>(employees);
        //    return View(model);
        //}

        /* Asynchronous functions */
        public async Task<ActionResult> ListEmployees()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            var model = _mapper.Map<List<EmployeeVM>>(employees);
            return View(model);
        }
        #endregion


        #region Details
        /* Synchronous functions */

        // GET: LeaveAllocationController/Details/5
        //public ActionResult Details(string id)
        //{
        //    var employee = _mapper.Map<EmployeeVM>(_userManager.FindByIdAsync(id).Result);

        //    var allocations = _mapper.Map<List<LeaveAllocationVM>>(_leaveallocationrepo.GetLeaveAllocationsByEmployee(id));

        //    var model = new ViewAllocationsVM
        //    {
        //        Employee = employee,
        //        LeaveAllocations = allocations
        //    };

        //    return View(model);
        //}

        /* Asynchronous functions */
        public async Task<ActionResult> Details(string id)
        {
            var employee = _mapper.Map<EmployeeVM>(await _userManager.FindByIdAsync(id));

            var allocations = _mapper.Map<List<LeaveAllocationVM>>(await _leaveallocationrepo.GetLeaveAllocationsByEmployee(id));

            var model = new ViewAllocationsVM
            {
                Employee = employee,
                LeaveAllocations = allocations
            };

            return View(model);
        }

        #endregion


        #region Create
        /* Synchronous functions */

        // GET: LeaveAllocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveAllocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        /* Asynchronous functions */

        #endregion


        #region Edit
        /* Synchronous functions */

        //// GET: LeaveAllocationController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    var leaveallocation = _leaveallocationrepo.FindById(id);
        //    var model = _mapper.Map<EditLeaveAllocationVM>(leaveallocation);
        //    return View(model);
        //}

        //// POST: LeaveAllocationController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(EditLeaveAllocationVM model)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View(model);
        //        }

        //        var record = _leaveallocationrepo.FindById(model.Id);
        //        record.NumberOfDays = model.NumberOfDays;

        //        var isSuccess = _leaveallocationrepo.Update(record);
        //        if (!isSuccess)
        //        {
        //            ModelState.AddModelError("", "Error while saving");
        //            return View(model);
        //        }
        //        return RedirectToAction(nameof(Details), new { id = model.EmployeeId });
        //    }
        //    catch
        //    {
        //        return View(model);
        //    }
        //}

        /* Asynchronous functions */
        // GET: LeaveAllocationController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var leaveallocation = await _leaveallocationrepo.FindById(id);
            var model = _mapper.Map<EditLeaveAllocationVM>(leaveallocation);
            return View(model);
        }

        // POST: LeaveAllocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditLeaveAllocationVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var record = await _leaveallocationrepo.FindById(model.Id);
                record.NumberOfDays = model.NumberOfDays;

                var isSuccess = await _leaveallocationrepo.Update(record);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Error while saving");
                    return View(model);
                }
                return RedirectToAction(nameof(Details), new { id = model.EmployeeId });
            }
            catch
            {
                return View(model);
            }
        }

        #endregion


        #region Delete
        /* Synchronous functions */
        // GET: LeaveAllocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveAllocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        /* Asynchronous functions */

        #endregion
    }
}
