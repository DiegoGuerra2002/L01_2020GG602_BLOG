using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020GG602.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020GG602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class calificacionesController : ControllerBase
    {
        private readonly blogContext _blogContext;
        public calificacionesController(blogContext blogContext)
        {
            _blogContext = blogContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult Get()
        {
            List<calificaciones> listado = (from e in _blogContext.calificaciones
                                      select e).ToList();

            if (listado.Count == 0)
            {
                return NotFound();
            }

            return Ok(listado);
        }
    }
}
