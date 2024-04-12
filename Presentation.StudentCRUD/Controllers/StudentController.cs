using Application.StudentCRUD;
using Domain.StudentCRUD;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.StudentCRUD.Controllers
{
    public class StudentController: Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost, Route("AddStudent")]
        public async Task<IActionResult> AddStudent(Student student)
        {
            var addStudent = await _studentService.AddStudent(student);
            return Ok(addStudent);
        }

        [HttpGet, Route("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await _studentService.GetAllStudents());
        }

        [HttpGet, Route("GetStudentById")]
        public async Task<IActionResult> GetStudentById(string id)
        {
            var result = await _studentService.GetStudentById(id);
            return Ok(result);
        }
        [HttpPut, Route("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            var updatedStudent = await _studentService.UpdateStudent(student);
            return Ok(updatedStudent);
        }

        [HttpDelete, Route("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            await _studentService.DeleteStudent(id);
            return Ok();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
