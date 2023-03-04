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
        [HttpPost]
        [Route("Add")]
        public ActionResult GuardarCalificaciones([FromBody] calificaciones cal)
        {
            try
            {
                _blogContext.calificaciones.Add(cal);
                _blogContext.SaveChanges();
                return Ok(cal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarCalificaciones(int id, [FromBody] calificaciones calModificar)
        {
            calificaciones? calActual = (from e in _blogContext.calificaciones
                                     where e.calificacionId == id
                                     select e).FirstOrDefault();

            if (calActual == null)
            {
                return NotFound();
            }

            calActual.calificacion = calModificar.calificacion;

            _blogContext.Entry(calActual).State = EntityState.Modified;
            _blogContext.SaveChanges();
            return Ok(calModificar);


        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarCalificaciones(int id)
        {
            calificaciones? calificaciones = (from e in _blogContext.calificaciones
                               where e.calificacionId == id
                               select e).FirstOrDefault();
            if (calificaciones == null) { return NotFound(); }

            _blogContext.calificaciones.Attach(calificaciones);
            _blogContext.calificaciones.Remove(calificaciones);
            _blogContext.SaveChanges();
            return Ok(calificaciones);
        }
    }
}
