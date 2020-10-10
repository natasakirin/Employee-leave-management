using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Employee_leave_management.Data;
using Employee_leave_management.Interfaces;
using Employee_leave_management.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employee_leave_management.Controllers
{
    public class LeaveTypesController : Controller
    {
        private readonly ILeaveTypeRepository _repo;
        private readonly IMapper _mapper;

        public LeaveTypesController(ILeaveTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        // GET: LeaveTypesController
        public ActionResult Index()
        {
            var leavetypes = _repo.FindAll().ToList();
            /* Mapper: Map<source, destination>(what objects should be mapped) */
            var model = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leavetypes);
            return View(model);
        }

        // GET: LeaveTypesController/Details/5
        public ActionResult Details(int id)
        {
            if (!_repo.isExists(id))
            {
                return NotFound();
            }

            var leavetype = _repo.FindById(id);

            /* we're mapping LeaveType (data class) into LeaveTypeVM */
            var model = _mapper.Map<LeaveTypeVM>(leavetype);

            return View(model);
        }

        // GET: LeaveTypesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveTypeVM model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(model);
                }

                /* I want the model to be mapped into LeaveType (from the DB) */
                var leavetype = _mapper.Map<LeaveType>(model);
                leavetype.DateCreated = DateTime.Now;

                var isSuccess = _repo.Create(leavetype);

                if(!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }

        // GET: LeaveTypesController/Edit/5
        public ActionResult Edit(int id)
        {
            if(!_repo.isExists(id))
            {
                return NotFound();
            }

            var leavetype = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeVM>(leavetype);

            return View(model);
        }

        // POST: LeaveTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var leavetype = _mapper.Map<LeaveType>(model);

                var isSuccess = _repo.Update(leavetype);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong...");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong...");
                return View(model);
            }
        }

        // GET: LeaveTypesController/Delete/5
        public ActionResult Delete(int id)
        {
            var leavetype = _repo.FindById(id);
            if (leavetype == null)
            {
                return NotFound();
            }

            var isSuccess = _repo.Delete(leavetype);

            if (!isSuccess)
            {
                return BadRequest(); 
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
