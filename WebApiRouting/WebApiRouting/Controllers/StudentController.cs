using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiRouting.Models;

namespace WebApiRouting.Controllers
{
    [RoutePrefix("api/student")]
    public class StudentController : ApiController
    {
        static List<Student> students = new List<Student>()
        {
            new Student() {Id=1, Name = "Divya"},
            new Student() {Id=2, Name = "Kavya"},
            new Student() {Id=3, Name = "Madhurima"},
            new Student() {Id=4, Name = "Ajay"},
            new Student() {Id=5, Name = "Raju"},
            new Student() {Id=6, Name = "Srikanth"},
        };

        [Route("")]
        public IEnumerable<Student> Get()
        {
            return students;
        }

        [Route("~/api/teachers")]
        public IEnumerable<Teacher> GetTeachers()
        {
            return new List<Teacher>()
            {
                new Teacher() {Id=1, Name = "Lakshmi"},
                new Teacher() {Id=2, Name = "Subha"},
                new Teacher() {Id=3, Name = "Krishna"}
            };
        }

        [Route("{id:int:min(1)}")]
        public Student Get(int id)
        {
            return students.FirstOrDefault(student => student.Id == id);
        }

        [Route("{name:alpha}")]
        public Student Get(string name)
        {
            return students.FirstOrDefault(student => student.Name.ToLower() == name.ToLower());
        }

        [Route("{id}/courses")]
        public IEnumerable<string> GetCourseStudents(int id)
        {
            if (id == 1)
                return new List<String>() { "C#", "Asp.Net", "SQL Server" };
            else if (id == 2)
                return new List<String>() { "Asp.Net Web API", "C#", "SQL Server" };
            else
                return new List<String>() { "Bootstarp", "AngularJS", "JQuery" };
        }

        [Route("", Name="GetStudentById")]
        public HttpResponseMessage Post(Student student)
        {
            students.Add(student);
            var response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Url.Link("GetStudentById", new {id = student.Id}));
            return response;
        }
    }
}
