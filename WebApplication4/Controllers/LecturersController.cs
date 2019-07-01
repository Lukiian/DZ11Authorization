using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.ADO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace DZ6MVC.Controllers
{
    [Authorize]
    public class LecturersController : Controller
    {
        private readonly Repository repository;

        public LecturersController(Repository repository)
        {
            this.repository = repository;
        }

        public IActionResult Lecturers()
        {
            return View(repository.GetAllLecturers());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["Action"] = "Create";
            var lecturer = new Lecturer();
            return View("Create", lecturer);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            ViewData["Action"] = "Edit";
            var lecturer = repository.GetLecturerById(id);
            return View("Create", lecturer);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Lecturer lecturer)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Action"] = "Edit";
                return View("Create", lecturer);
            }
            repository.UpdateLecturer(lecturer);
            return RedirectToAction("Lecturers");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Lecturer lecturer)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Action"] = "Edit";
                return View("Create", lecturer);
            }
            repository.CreateLecturer(lecturer);
            return RedirectToAction("Lecturers");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            repository.DeleteLecturer(id);
            return RedirectToAction("Lecturers");
        }
    }
}