using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : Controller
    {
        #region Prueba
        //indica la URL
        [HttpGet("Prueba")]
        //mtodo publico que se ejecutara si la URL es llamada

        public string Get() 
        {
            return "Hola Mundo";
        }
        #endregion
    }
}
