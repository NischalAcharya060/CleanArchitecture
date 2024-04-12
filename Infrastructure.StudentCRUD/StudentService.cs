using Application.StudentCRUD;
using Domain.StudentCRUD;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.StudentCRUD
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDBContext _dbContext;

        public StudentService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Student> AddStudent(Student student)
        {
            Console.WriteLine(student);
            var result = await _dbContext.StudentData.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _dbContext.StudentData.ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentById(string id)
        {
            var result = await _dbContext.StudentData.Where(s => s.Id.ToString() == id).ToListAsync();
            return result;
        }
        public async Task<Student> UpdateStudent(Student student)
        {
            _dbContext.StudentData.Update(student);
            await _dbContext.SaveChangesAsync();
            return student;
        }

        public async Task DeleteStudent(Guid id)
        {
            var studentToDelete = await _dbContext.StudentData.FindAsync(id);
            if (studentToDelete != null)
            {
                _dbContext.StudentData.Remove(studentToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
