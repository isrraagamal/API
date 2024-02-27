using Day1.DTO;
using Day1.Entity;
using Day1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day1.Controllers
{
    [Route("api/[controller]")] //api/Student
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly FacultyContext db;
        public StudentController(FacultyContext _std)
        {
            db = _std;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var student = db.Students.Include(dept => dept.Department).ToList();
            if (student is null)
            {
                return NotFound();
            }
            /*return Ok(student);*/
            List<StudentWithDepartmentName> studentsWithDepartmentNames = new List<StudentWithDepartmentName>();
            foreach (var std in student)
            {
                StudentWithDepartmentName studentWithDepartmentName = new StudentWithDepartmentName();
                studentWithDepartmentName.Student_Id = std.Id;
                studentWithDepartmentName.Student_Name = std.Name;
                studentWithDepartmentName.Student_Age = std.Age;
                studentWithDepartmentName.Student_Address = std.Address;
                studentWithDepartmentName.Student_Image = std.Image;
                studentWithDepartmentName.Student_Department = std.Department?.Name;
                studentsWithDepartmentNames.Add(studentWithDepartmentName);
            }

            return Ok(studentsWithDepartmentNames);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var student = db.Students.Include(dept => dept.Department).FirstOrDefault(s => s.Id == id);
            if (student is null)
            {
                return NotFound();
            }
            StudentWithDepartmentName studentWithDepartmentName = new StudentWithDepartmentName();
            studentWithDepartmentName.Student_Name = student.Name;
            studentWithDepartmentName.Student_Age = student.Age;
            studentWithDepartmentName.Student_Address = student.Address;
            studentWithDepartmentName.Student_Image = student.Image;
            studentWithDepartmentName.Student_Department = student.Department?.Name;
            return Ok(new { msg = $"Student with id {id} is found", Student = studentWithDepartmentName });
        }

       
        [HttpGet]
        [Route("GetByName/{name}")] //add segment GetByName (Related Path with essential URL)=> Domain/studnet/GetByName/israa
        // [Route("/GetByName/{name}")] this is called absolute (New) path => Domain/GetByName/israa
        public IActionResult GetByName(string name)
        {
            var student = db.Students.Include(dept => dept.Department).FirstOrDefault(s => s.Name == name);
        
            if (student == null)
            {
                return NotFound(new { msg = $"Student with name {name} is not found" });
            }
            StudentWithDepartmentName studentWithDepartmentName = new StudentWithDepartmentName();
            studentWithDepartmentName.Student_Id = student.Id;
            studentWithDepartmentName.Student_Name = student.Name;
            studentWithDepartmentName.Student_Age = student.Age;
            studentWithDepartmentName.Student_Address = student.Address;
            studentWithDepartmentName.Student_Image = student.Image;
            studentWithDepartmentName.Student_Department = student.Department?.Name;

            return Ok(new { msg = $"Student with name {name} is found", student = studentWithDepartmentName });
        }

        [HttpPost]
        public IActionResult Add(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return CreatedAtAction("GetById", new { id = student.Id }, student);
                /*return Ok(student);*/ //200
                
            }
            return NotFound(); //204
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Student student)
        {
            var std = db.Students.Find(id);
            if (std == null)
            {
                return NotFound(); // 404 
            }

            if (ModelState.IsValid)
            {
               
                std.Name = student.Name;
                std.Address = student.Address;
                std.Age = student.Age;

                db.Students.Update(std);
                db.SaveChanges();
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var std = db.Students.Find(id);
            if (std is null)
                return NotFound(); //404
             
            db.Students.Remove(std);
            db.SaveChanges();
            return Ok(std);

        }
           

     }

}

