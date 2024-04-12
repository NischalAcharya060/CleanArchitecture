using Domain.StudentCRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StudentCRUD
{
    public interface IStudentService
    {
        Task<Student> AddStudent(Student student);
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student> UpdateStudent(Student student);
        Task DeleteStudent(Guid id);
        Task<IEnumerable<Student>> GetStudentById(string id);
    }
}
