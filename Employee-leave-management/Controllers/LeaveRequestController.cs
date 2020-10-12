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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Employee_leave_management.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestRepository _leaveRequestRepo;
        private readonly ILeaveTypeRepository _leaveTypeRepo;
        private readonly ILeaveAllocationRepository _leaveAllocRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public LeaveRequestController(
            ILeaveRequestRepository leaveRequestRepo,
            ILeaveTypeRepository leaveTypeRepo,
            ILeaveAllocationRepository leaveAllocRepo,
            IMapper mapper,
            UserManager<Employee> userManager
        )
        {
            _leaveRequestRepo = leaveRequestRepo;
            _leaveTypeRepo = leaveTypeRepo;
            _leaveAllocRepo = leaveAllocRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        #region Index (FindAll)
        [Authorize(Roles ="Administrator")]

        /* Synchronous functions */
        // GET: LeaveRequestController
        //public ActionResult Index()
        //{
        //    var leaveRequests = _leaveRequestRepo.FindAll();
        //    var leaveRequestsModel = _mapper.Map<List<LeaveRequestVM>>(leaveRequests);
        //    var model = new AdminLeaveRequestVM
        //    {
        //        TotalRequests = leaveRequestsModel.Count,
        //        ApprovedRequests = leaveRequestsModel.Count(x => x.Approved == true),
        //        PendingRequests = leaveRequestsModel.Count(x => x.Approved == null),
        //        RejectedRequests = leaveRequestsModel.Count(x => x.Approved == false),
        //        LeaveRequests = leaveRequestsModel
        //    };

        //    return View(model);
        //}

        /* Asynchronous functions */
        public async Task<ActionResult> Index()
        {
            var leaveRequests = await _leaveRequestRepo.FindAll();
            var leaveRequestsModel = _mapper.Map<List<LeaveRequestVM>>(leaveRequests);
            var model = new AdminLeaveRequestVM
            {
                TotalRequests = leaveRequestsModel.Count,
                ApprovedRequests = leaveRequestsModel.Count(x => x.Approved == true),
                PendingRequests = leaveRequestsModel.Count(x => x.Approved == null),
                RejectedRequests = leaveRequestsModel.Count(x => x.Approved == false),
                LeaveRequests = leaveRequestsModel
            };

            return View(model);
        }
        #endregion


        #region MyLeave
        /* Synchronous functions */

        //public ActionResult MyLeave()
        //{
        //    var employee = _userManager.GetUserAsync(User).Result;
        //    var employeeid = employee.Id;
        //    var employeeAllocations = _leaveAllocRepo.GetLeaveAllocationsByEmployee(employeeid);
        //    var employeeRequests = _leaveRequestRepo.GetLeaveRequestsByEmployee(employeeid);

        //    var employeeAllocationsModel = _mapper.Map<List<LeaveAllocationVM>>(employeeAllocations);
        //    var employeeRequestsModel = _mapper.Map<List<LeaveRequestVM>>(employeeRequests);

        //    var model = new EmployeeLeaveRequestViewVM
        //    {
        //        LeaveAllocations = employeeAllocationsModel,
        //        LeaveRequests = employeeRequestsModel
        //    };

        //    return View(model);
        //}


        /* Asynchronous functions */
        public async Task<ActionResult> MyLeave()
        {
            var employee = await _userManager.GetUserAsync(User);
            var employeeid = employee.Id;
            var employeeAllocations = await _leaveAllocRepo.GetLeaveAllocationsByEmployee(employeeid);
            var employeeRequests =await  _leaveRequestRepo.GetLeaveRequestsByEmployee(employeeid);

            var employeeAllocationsModel = _mapper.Map<List<LeaveAllocationVM>>(employeeAllocations);
            var employeeRequestsModel = _mapper.Map<List<LeaveRequestVM>>(employeeRequests);

            var model = new EmployeeLeaveRequestViewVM
            {
                LeaveAllocations = employeeAllocationsModel,
                LeaveRequests = employeeRequestsModel
            };

            return View(model);
        }

        #endregion


        #region Details
        /* Synchronous functions */

        // GET: LeaveRequestController/Details/5
        //public ActionResult Details(int id)
        //{
        //    var leaveRequest = _leaveRequestRepo.FindById(id);
        //    var model = _mapper.Map<LeaveRequestVM>(leaveRequest);
        //    return View(model);
        //}

        /* Asynchronous functions */
        public async Task<ActionResult> Details(int id)
        {
            var leaveRequest = await _leaveRequestRepo.FindById(id);
            var model = _mapper.Map<LeaveRequestVM>(leaveRequest);
            return View(model);
        }
        #endregion


        #region ApproveRequest
        /* Synchronous functions */

        //public ActionResult ApproveRequest(int id)
        //{
        //    try
        //    {
        //        var user = _userManager.GetUserAsync(User).Result;
        //        var leaveRequest = _leaveRequestRepo.FindById(id);

        //        var employeeid = leaveRequest.RequestingEmployeeId;
        //        var leaveTypeId = leaveRequest.LeaveTypeId;
        //        var allocation = _leaveAllocRepo.GetLeaveAllocationsByEmployeeAndType(employeeid, leaveTypeId);

        //        int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
        //        allocation.NumberOfDays -= daysRequested;


        //        leaveRequest.Approved = true;
        //        leaveRequest.ApprovedById = user.Id;
        //        leaveRequest.DateActioned = DateTime.Now;

        //        _leaveRequestRepo.Update(leaveRequest);
        //        _leaveAllocRepo.Update(allocation);

        //        var isSuccess = _leaveRequestRepo.Update(leaveRequest);

        //        return RedirectToAction(nameof(Index));

        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //}

        /* Asynchronous functions */
        public async Task<ActionResult> ApproveRequest(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var leaveRequest = await _leaveRequestRepo.FindById(id);

                var employeeid = leaveRequest.RequestingEmployeeId;
                var leaveTypeId = leaveRequest.LeaveTypeId;
                var allocation = await _leaveAllocRepo.GetLeaveAllocationsByEmployeeAndType(employeeid, leaveTypeId);

                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                allocation.NumberOfDays -= daysRequested;


                leaveRequest.Approved = true;
                leaveRequest.ApprovedById = user.Id;
                leaveRequest.DateActioned = DateTime.Now;

                await _leaveRequestRepo.Update(leaveRequest);
                await _leaveAllocRepo.Update(allocation);

                var isSuccess = await _leaveRequestRepo.Update(leaveRequest);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        #endregion


        #region RejectRequest
        /* Synchronous functions */

        //public ActionResult RejectRequest(int id)
        //{
        //    try
        //    {
        //        var user = _userManager.GetUserAsync(User).Result;
        //        var leaveRequest = _leaveRequestRepo.FindById(id);
        //        leaveRequest.Approved = false;
        //        leaveRequest.ApprovedById = user.Id;
        //        leaveRequest.DateActioned = DateTime.Now;

        //        _leaveRequestRepo.Update(leaveRequest);
        //        return RedirectToAction(nameof(Index));

        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //}

        /* Asynchronous functions */
        public async Task<ActionResult> RejectRequest(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var leaveRequest = await _leaveRequestRepo.FindById(id);
                leaveRequest.Approved = false;
                leaveRequest.ApprovedById = user.Id;
                leaveRequest.DateActioned = DateTime.Now;

                await _leaveRequestRepo.Update(leaveRequest);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        #endregion


        #region Create
        /* Synchronous functions */
        //// GET: LeaveRequestController/Create
        //public ActionResult Create()
        //{

        //    var leaveTypes = _leaveTypeRepo.FindAll();
        //    var leaveTypeItems = leaveTypes.Select(q => new SelectListItem
        //    {
        //        Text = q.Name,
        //        Value = q.Id.ToString()
        //    });

        //    var model = new CreateLeaveRequestVM
        //    {
        //        LeaveTypes = leaveTypeItems
        //    };

        //    return View(model);

        //}

        //// POST: LeaveRequestController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(CreateLeaveRequestVM model)
        //{
        //    try
        //    {
        //        var startDate = Convert.ToDateTime(model.StartDate);
        //        var endDate = Convert.ToDateTime(model.EndDate);

        //        var leaveTypes = _leaveTypeRepo.FindAll();
        //        var leaveTypeItems = leaveTypes.Select(q => new SelectListItem
        //        {
        //            Text = q.Name,
        //            Value = q.Id.ToString()
        //        });
        //        model.LeaveTypes = leaveTypeItems;


        //        if (!ModelState.IsValid)
        //        {
        //            return View(model);
        //        }

        //        /* Validation: end date is not earlier than start date */
        //        if (DateTime.Compare(startDate, endDate) > 1)
        //        {
        //            ModelState.AddModelError("", "Start Date cannot be further in the future than the End Date");
        //            return View(model);
        //        }

        //        var employee = _userManager.GetUserAsync(User).Result;
        //        var allocation = _leaveAllocRepo.GetLeaveAllocationsByEmployeeAndType(employee.Id, model.LeaveTypeId);
        //        int daysRequested = (int)(endDate - startDate).TotalDays;

        //        if (daysRequested > allocation.NumberOfDays)
        //        {
        //            ModelState.AddModelError("", "You Do Not Have Sufficient Days For This Request");
        //            return View(model);
        //        }

        //        var leaveRequestModel = new LeaveRequestVM
        //        {
        //            RequestingEmployeeId = employee.Id,
        //            StartDate = startDate,
        //            EndDate = endDate,
        //            Approved = null,
        //            DateRequested = DateTime.Now,
        //            DateActioned = DateTime.Now,
        //            LeaveTypeId = model.LeaveTypeId,
        //            RequestComments = model.RequestComments
        //        };

        //        var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestModel);
        //        var isSuccess = _leaveRequestRepo.Create(leaveRequest);

        //        if (!isSuccess)
        //        {
        //            ModelState.AddModelError("", "Something went wrong with submitting your record");
        //            return View(model);
        //        }


        //        return RedirectToAction("MyLeave");
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", "Something went wrong...");
        //        return View(model);
        //    }
        //}

        /* Asynchronous functions */
        // GET: LeaveRequestController/Create
        public async Task<ActionResult> Create()
        {

            var leaveTypes = await _leaveTypeRepo.FindAll();
            var leaveTypeItems = leaveTypes.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });

            var model = new CreateLeaveRequestVM
            {
                LeaveTypes = leaveTypeItems
            };

            return View(model);

        }

        // POST: LeaveRequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveRequestVM model)
        {
            try
            {
                var startDate = Convert.ToDateTime(model.StartDate);
                var endDate = Convert.ToDateTime(model.EndDate);

                var leaveTypes = await _leaveTypeRepo.FindAll();
                var leaveTypeItems = leaveTypes.Select(q => new SelectListItem
                {
                    Text = q.Name,
                    Value = q.Id.ToString()
                });
                model.LeaveTypes = leaveTypeItems;


                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                /* Validation: end date is not earlier than start date */
                if (DateTime.Compare(startDate, endDate) > 1)
                {
                    ModelState.AddModelError("", "Start Date cannot be further in the future than the End Date");
                    return View(model);
                }

                var employee = await _userManager.GetUserAsync(User);
                var allocation = await _leaveAllocRepo.GetLeaveAllocationsByEmployeeAndType(employee.Id, model.LeaveTypeId);
                int daysRequested = (int)(endDate - startDate).TotalDays;

                if (daysRequested > allocation.NumberOfDays)
                {
                    ModelState.AddModelError("", "You Do Not Have Sufficient Days For This Request");
                    return View(model);
                }

                var leaveRequestModel = new LeaveRequestVM
                {
                    RequestingEmployeeId = employee.Id,
                    StartDate = startDate,
                    EndDate = endDate,
                    Approved = null,
                    DateRequested = DateTime.Now,
                    DateActioned = DateTime.Now,
                    LeaveTypeId = model.LeaveTypeId,
                    RequestComments = model.RequestComments
                };

                var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestModel);
                var isSuccess = await _leaveRequestRepo.Create(leaveRequest);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong with submitting your record");
                    return View(model);
                }


                return RedirectToAction("MyLeave");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }

        #endregion


        #region CancelRequest
        /* Synchronous functions */
        //public ActionResult CancelRequest(int id)
        //{
        //    var leaveRequest = _leaveRequestRepo.FindById(id);
        //    leaveRequest.Cancelled = true;
        //    _leaveRequestRepo.Update(leaveRequest);
        //    return RedirectToAction("MyLeave");
        //}

        /* Asynchronous functions */
        public async Task<ActionResult> CancelRequest(int id)
        {
            var leaveRequest = await _leaveRequestRepo.FindById(id);
            leaveRequest.Cancelled = true;
            await _leaveRequestRepo.Update(leaveRequest);
            return RedirectToAction("MyLeave");
        }
        #endregion


        #region Edit
        /* Synchronous functions */

        // GET: LeaveRequestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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


        #region Delete
        /* Synchronous functions */

        // GET: LeaveRequestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveRequestController/Delete/5
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
        #endregion
    }
}
