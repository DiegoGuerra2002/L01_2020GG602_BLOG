using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020GG602.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020GG602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly blogContext _blogContext;
        public usuariosController(blogContext blogContext)
        {
            _blogContext = blogContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult Get()
        {
            List<usuarios> listado = (from e in _blogContext.usuarios
                                           select e).ToList();

            if (listado.Count == 0)
            {
                return NotFound();
            }

            return Ok(listado);
        }
        [HttpPost]
        [Route("Add")]
        public ActionResult GuardarUsuarios([FromBody] usuarios user)
        {
            try
            {
                _blogContext.usuarios.Add(user);
                _blogContext.SaveChanges();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarUsuarios(int id, [FromBody] usuarios userModificar)
        {
            usuarios? userActual = (from e in _blogContext.usuarios
                                         where e.usuarioId == id
                                         select e).FirstOrDefault();

            if (userActual == null)
            {
                return NotFound();
            }

            userActual.nombreUsuario = userModificar.nombreUsuario;

            _blogContext.Entry(userActual).State = EntityState.Modified;
            _blogContext.SaveChanges();
            return Ok(userModificar);


        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarUsuarios(int id)
        {
            usuarios? usuarios = (from e in _blogContext.usuarios
                                              where e.usuarioId == id
                                              select e).FirstOrDefault();
            if (usuarios == null) { return NotFound(); }

            _blogContext.usuarios.Attach(usuarios);
            _blogContext.usuarios.Remove(usuarios);
            _blogContext.SaveChanges();
            return Ok(usuarios);
        }
        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByNombreEquipo(string filtro)
        {
            usuarios? usuarios = (from e in _equiposContexto.equipos
                               where e.descripcion.Contains(filtro)
                               select e).FirstOrDefault();
            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }
    }
}
