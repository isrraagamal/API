using Azure.Messaging;
using Day1.DTO;
using Day1.Entity;
using Day1.Models;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Day1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly FacultyContext db;

        public DepartmentController(FacultyContext _db)
        {
            db = _db;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var dept = db.Departments.ToList();
            if (dept is null)
            {
                return NotFound();
            }

            List<DepartmentWithStudentNames> departmentsWithStudents = new List<DepartmentWithStudentNames>();

            foreach (var department in dept)
            {
                var departmentWithStudents = new DepartmentWithStudentNames
                {
                    Department_Number = department.DeptId,
                    Department_Name = department.Name,
                    Department_Location = department.Location,
                    Manager_Name = department.ManagerName
                };

             
                var studentsInDepartment = db.Students.Where(s => s.DeptId == department.DeptId).ToList();
                foreach (var student in studentsInDepartment)
                {
                    departmentWithStudents.Students_Names.Add(student.Name);
                }

                departmentsWithStudents.Add(departmentWithStudents);
            }

            return Ok(departmentsWithStudents);

        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var dept = db.Departments.Include(std => std.Students).FirstOrDefault(dep => dep.DeptId == id);
            if (dept is null)
            {
                return NotFound(new { msg = $"Department with id {id} is not found " });
            }
            DepartmentWithStudentNames departmentWithStudentNames = new DepartmentWithStudentNames();
            departmentWithStudentNames.Department_Number = dept.DeptId;
            departmentWithStudentNames.Department_Name = dept.Name;
            departmentWithStudentNames.Department_Location = dept.Location;
            departmentWithStudentNames.Manager_Name = dept.ManagerName;
            foreach (var std in dept.Students)
            {
                departmentWithStudentNames.Students_Names.Add(std.Name);
            }
            return Ok(new { msg = $"Department with id {id} is found", Department = departmentWithStudentNames });
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var dept = db.Departments.FirstOrDefault(s => s.Name == name);

            if (dept == null)
            {
                return NotFound(new { msg = $"Department with name {name} is not found" });
            }

            return Ok(new { msg = $"Department with name {name} is found", Department = dept });
        }

        [HttpPost]
        public IActionResult Add(Models.Department dept)
        {
            if (ModelState.IsValid)
            {
                var DeptName = dept.Name.ToUpper();
                if (db.Departments.Any(d => d.Name.ToUpper() == DeptName))
                {
                    return Conflict(new { message = "Department name must be unique." });
                }


                if (!string.Equals(dept.Location, "EG", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(dept.Location, "USA", StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest(new { message = "Department location must be 'EG' or 'USA'." });
                }

                db.Departments.Add(dept);
                db.SaveChanges();
                return CreatedAtAction("GetById", new { id = dept.DeptId }, dept);

            }
            return NotFound();


        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Models.Department dep)
        {
            var dept = db.Departments.Find(id);
            if (dept == null)
            {
                return NotFound(); // 404 
            }

            if (ModelState.IsValid)
            {

                dept.Name = dep.Name;
                dept.Location = dep.Location;
                dept.ManagerName = dep.ManagerName;

                db.Departments.Update(dep);
                db.SaveChanges();
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dept = db.Departments.Find(id);
            if (dept is null)
                return NotFound(); //404

            db.Departments.Remove(dept);
            db.SaveChanges();
            return Ok(dept);

        }


    }
}
