using FinalTest.DataAccess;
using FinalTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FinalTest.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            //var data1 = _dbContext.students.Join(
            //            _dbContext.studentCourses, s=>s.StudentId,sc => sc.StudentId,(s,sc) => new {Student = s,StudentCourses = sc})
            //    .Join(_dbContext.courses,sc => sc.StudentCourses.CourseId,c=>c.CourseId,(sc,c)=> new {sc.Student,Course = c}).ToList().
            //    GroupBy(x=>x.Student).Select(p=> new {Student = p.Key, TotalPrice = p.Sum(x=>x.Course.CoursePrice)});

            //List<StudentVM> students = new List<StudentVM>();
            //foreach(var student in data1)
            //{
            //    StudentVM studentVM = new StudentVM()
            //    {

            //    };
            //    students.Add(studentVM);
            //}

            //return View(students);
            var q = (from pd in _dbContext.courses
                     join od in _dbContext.studentCourses on pd.CourseId equals od.CourseId
                     join ct in _dbContext.students on od.StudentId equals ct.StudentId
                     select new StudentVM
                     {
                         Name = ct.Name,
                         Email = ct.Email,
                         ContactNo = ct.ContactNo.ToString(),
                         RollNo = ct.RollNo,
                         Address = ct.Address,
                         State = ct.State,
                         City = ct.City,  //define anonymous type Customer
                         Zipcode = ct.Zipcode.ToString(),
                         CourseTotalPrice = "ABC"
                     });

            //return View(q);
            var data = _dbContext.students.ToList();
            return View(data);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (id == null)
            {
                Student stu = new Student();
                return View(stu);
            }
            var data = _dbContext.students.Find(id);
            return View(data);
        }

        [HttpPost]    
        public IActionResult Upsert(int id, Student obj)
        {
            try
            {             
                if (id == 0 || id == null)
                {

                    var data = new Student()
                    {
                        Name = obj.Name,
                        RollNo = obj.RollNo,
                        Email = obj.Email,
                        Address = obj.Address,
                        ContactNo = obj.ContactNo,
                        State = obj.State,
                        City = obj.City,
                        Zipcode= obj.Zipcode,
                    };

                     _dbContext.students.Add(data);
                     _dbContext.SaveChanges();
                    TempData["success"] = "Student Added Successfully";
                }
                else
                {
                    var data = new Student()
                    {       
                        StudentId = id,
                        Name = obj.Name,
                        RollNo = obj.RollNo,
                        Email = obj.Email,
                        Address = obj.Address,
                        ContactNo = obj.ContactNo,
                        State = obj.State,
                        City = obj.City,
                        Zipcode = obj.Zipcode,
                    };

                    _dbContext.students.Update(data);
                     _dbContext.SaveChanges();
                    TempData["success"] = "Student Updated Successfully";
                }

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPost]
        public IActionResult DeleteStudent(int Id)
        {           
            var DelStu = _dbContext.students.Find(Id);
            if (DelStu == null)
            {
                TempData["error"] = "Student Delete not Successfully";
                return RedirectToAction("Index", "Admin");
            }           
            _dbContext.students.Remove(DelStu);
            _dbContext.SaveChanges();
            TempData["success"] = "Student Delete Successfully";
            return RedirectToAction("Index", "Admin");
        }


       
        [HttpGet]
        public IActionResult AddCourse()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(int id,Course obj)
        {
            if(id != 0)
            {
                Course data = new Course()
                {
                    CourseName = obj.CourseName,
                    CoursePrice = obj.CoursePrice
                };
                _dbContext.Add(data);
                _dbContext.SaveChanges();   
                StudentCourses data1 = new StudentCourses()
                {
                    StudentId = id,
                    CourseId = data.CourseId,
                };
                _dbContext.Add(data1);
                _dbContext.SaveChanges();
                TempData["success"] = "Course Added Successfully";
                return RedirectToAction("Index", "Admin");
            }

            return View();
        }


        [HttpGet]
        public IActionResult ViewCourse(int id)
        {
            if(id != 0)
            {
                var data = _dbContext.studentCourses.Where(x=>x.StudentId== id).ToList(); 
                List<List<Course>> data1 = new List<List<Course>>();
                foreach(var course in data)
                {
                    data1.Add(_dbContext.courses.Where(x => x.CourseId == course.CourseId).ToList());
                }
                return View(data1);
            }
            return View();        
        }
      

        [HttpPost]
        public IActionResult DeleteCourse(int Id)
        {
            var DelCou = _dbContext.courses.Find(Id);
            if (DelCou == null)
            {
                TempData["error"] = "Course Delete not Successfully";
                return RedirectToAction("Cource", "Admin");
            }
              //delete from the bridge table

              var data = _dbContext.studentCourses.Where(_x => _x.CourseId == Id).FirstOrDefault();         
              _dbContext.studentCourses.Remove(data);
              _dbContext.SaveChanges();

              //delete for the courses

              _dbContext.courses.Remove(DelCou);
              _dbContext.SaveChanges();
                     
            TempData["success"] = "Course Delete Successfully";
            return RedirectToAction("Cource", "Admin");
        }

        [HttpGet]
        public IActionResult EditCourse(int id)
        {
            var data = _dbContext.courses.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult EditCourse(int id,Course obj)
        {
            if(ModelState.IsValid)
            {
                var data = new Course()
                {
                    CourseId = id,
                    CourseName = obj.CourseName,
                    CoursePrice = obj.CoursePrice
                };
                _dbContext.courses.Update(data);
                _dbContext.SaveChanges();
                TempData["success"] = "Course Updated Successfully";
                return RedirectToAction("index","Admin");
            }
           
            return View();
        }



    }
}
