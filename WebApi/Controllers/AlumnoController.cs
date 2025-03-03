using intento1.Models;
using intento1.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {

        private AlumnoDAO _dao = new AlumnoDAO();
        [HttpGet("alumnoProfesor")]
        public List<AlumnoProfesor> GetAlumnoProfesor(string usuario)
        {
            return _dao.AlumnoProfesor(usuario);
        }
        [HttpGet("alumno")]
        public Alumno selectById(int id)
        {
            var alumno = _dao.GetById(id);
            return alumno;
        }

        [HttpPut("alumno")]
        public bool actualizarAlumno([FromBody] Alumno alumno)
        {
            return _dao.update(alumno.Id, alumno);
        }
        [HttpPost("alumno")]
        public bool insertarMatricula([FromBody] Alumno alumno, int idAsignatura)
        {
            return _dao.InsertarMatricula(alumno, idAsignatura);
        }
    }
}
