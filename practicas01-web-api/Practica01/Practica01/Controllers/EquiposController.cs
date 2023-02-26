using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica01.Data;
using Practica01.Models;

namespace Practica01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        private readonly EquipoContext _equipoContext;


        public EquiposController(EquipoContext equipoContext)
        {
            _equipoContext= equipoContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {

            List<Equipo> listadoEquipos = (from equipo in _equipoContext.equipos select equipo).ToList();

            if (listadoEquipos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoEquipos);

        }

    }
}
