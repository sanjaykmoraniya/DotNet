using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase {

        private readonly TodoContext todoContext;

        public StudentController (TodoContext context) {
            todoContext = context;
        }

        [HttpGet]
        [EnableCors ()]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentListAsync () {

            if (todoContext.Student == null || !todoContext.Student.Any ()) {
                todoContext.Student.Add (new Student { Id = 1, Name = "Krunal", EnrollmentNumber = 110470116021, College = "VVP Engineering College", University = "GTU" });
                todoContext.Student.Add (new Student { Id = 2, Name = "Rushabh", EnrollmentNumber = 110470116023, College = "VVP Engineering College", University = "GTU" });
                todoContext.Student.Add (new Student { Id = 3, Name = "Ankit", EnrollmentNumber = 110470116022, College = "VVP Engineering College", University = "GTU" });
                todoContext.Student.Add (new Student { Id = 4, Name = "Vijay", EnrollmentNumber = 110470116024, College = "Engineering College", University = "BTU" });
                todoContext.SaveChanges ();
            }

            var result = await todoContext.Student.ToListAsync ();
            return result;
        }
    }

}