using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020GG602.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020GG602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly blogContext _blogContext;
        public comentariosController(blogContext blogContext)
        {
            _blogContext = blogContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult Get()
        {
            List<comentarios> listado = (from e in _blogContext.comentarios
                                            select e).ToList();

            if (listado.Count == 0)
            {
                return NotFound();
            }

            return Ok(listado);
        }
        [HttpPost]
        [Route("Add")]
        public ActionResult GuardarComentarios([FromBody] comentarios com)
        {
            try
            {
                _blogContext.comentarios.Add(com);
                _blogContext.SaveChanges();
                return Ok(com);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarComentarios(int id, [FromBody] comentarios comModificar)
        {
            comentarios? comActual = (from e in _blogContext.comentarios
                                         where e.cometarioId == id
                                         select e).FirstOrDefault();

            if (comActual == null)
            {
                return NotFound();
            }

            comActual.comentario = comActual.comentario;

            _blogContext.Entry(comActual).State = EntityState.Modified;
            _blogContext.SaveChanges();
            return Ok(comModificar);


        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarComentarios(int id)
        {
            comentarios? comentarios = (from e in _blogContext.comentarios
                                              where e.cometarioId == id
                                              select e).FirstOrDefault();
            if (comentarios == null) { return NotFound(); }

            _blogContext.comentarios.Attach(comentarios);
            _blogContext.comentarios.Remove(comentarios);
            _blogContext.SaveChanges();
            return Ok(comentarios);
        }
    }
}
