using intento1.Models;
using intento1.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private ProfesorDAO _proDao = new ProfesorDAO();

        // Creando endpoint con el metodo http post
        [HttpPost("autentificacion")]

        public string loginProfesor([FromBody] Profesor profesor)
        {
            var prof1 = _proDao.login(profesor.Usuario, profesor.Pass);

            if (prof1 != null)
            {
                return prof1.Usuario;
            }

            return "Elemento no encontrado";
        }
    }
}
