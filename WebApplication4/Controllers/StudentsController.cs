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
    public class StudentsController : Controller
    {
        private readonly Repository repository;

        public StudentsController(Repository repository)
        {
            this.repository = repository;
        }

        public IActionResult Students()
        {
            return View(repository.GetAllStudents());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var student = repository.GetStudentById(id);
            ViewData["Action"] = "Edit";
            return View("Create", student);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Student model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Action"] = "Edit";
                return View("Create", model);
            }
            repository.UpdateStudent(model);

            return RedirectToAction("Students");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            repository.DeleteStudent(id);
            return RedirectToAction("Students");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["Action"] = "Create";
            var student = new Student();
            return View("Create", student);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Student model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Action"] = "Create";
                return View("Create", model);
            }

            repository.CreateStudent(model);
            return RedirectToAction("Students");

        }

    }
}