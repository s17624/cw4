using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using cw3.DAL;
using cw3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw3.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
 
        private const String conString = "Data Source=db-mssql;Initial Catalog=s17624;Integrated Security=True";
   
        /*
        private readonly IDbService _dbService;

        public StudentsController(IDbService dBservice)
        {
            _dbService = dBservice;
        }
        */

        [HttpGet]
        public IActionResult GetStudents()
        {
            var list = new List<Student>();

            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from student";

                con.Open();
                var dr = com.ExecuteReader();
                while(dr.Read())
                {
                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    list.Add(st);

                }
            }
            return Ok(list);
        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetStudents(string indexNumber)
        {
    
            using (SqlConnection con = new SqlConnection(conString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from student where indexnumber =@index";

                /*
                SqlParameter par = new SqlParameter();
                par.Value = indexNumber;
                par.ParameterName = "index";
                com.Parameters.Add(par);
                */

                com.Parameters.AddWithValue("index", indexNumber);

                con.Open();
                var dr = com.ExecuteReader();
                if(dr.Read())
                {
                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    return Ok(st);

                }
            }
            return NotFound();
        }

        /*
        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            return Ok(_dbService.GetStudents());
        }
        */

        /*
        [HttpGet]
        public string GetStudent(string orderBy)
        {
            return $"Kowalski, Malewski, Andrzejewski sortowanie = {orderBy}";
        }
        */

        /*
        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski");
            } else if (id == 2)
            {
                return Ok("Malewski");
            }

            return NotFound("Nie znaleziono studenta.");
        }
        */

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            //... add to database
            //... generating index number
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult PutStudent(string id)
        {
            return Ok("Aktualizacja dokończona");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(string id)
        {
            return Ok("Usuwanie ukończone");
        }

    }
}