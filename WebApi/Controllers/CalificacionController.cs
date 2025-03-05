using intento1.Models;
using intento1.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CalificacionController : ControllerBase
    {
        // Instancia del elemento CalificacionDao
        private CalificacionDao _cdao = new CalificacionDao();

        [HttpGet("calificaciones")]
        public List<Calificacion> get(int idMatricula)
        {
            // Invocando al metodo CalificacionDao
            return _cdao.seleccion(idMatricula);
        }
    }
}
