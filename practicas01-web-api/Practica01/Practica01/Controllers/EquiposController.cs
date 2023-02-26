using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practica01.Data;
using Practica01.Models;
using Microsoft.EntityFrameworkCore;
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

            List<Equipo> listadoEquipos = _equipoContext.equipos.ToList();

            if (listadoEquipos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoEquipos);

        }



        [HttpGet]
        [Route("getbyid")]
        public IActionResult Get(int id)
        {
            Equipo? equipo = _equipoContext.equipos.Find(id);
            if (equipo == null) { return NotFound(); }
            return Ok(equipo);

        }



        
        [HttpGet]
        [Route("find")]
        public IActionResult FindByDescription(string filtro)
        {
            Equipo? equipo = (from e in _equipoContext.equipos
                               where (e.descripcion.Contains(filtro))
                               && e.estado =="A"
                               select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();

            }


            return Ok(equipo);

        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarEquipo([FromBody] Equipo equipo)
        {
            try
            {
                equipo.estado = "A";
                _equipoContext.equipos.Add(equipo);
                _equipoContext.SaveChanges();
                return Ok(equipo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



            [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizar(int id , [FromBody] Equipo equipoMod)
        {
                               
            Equipo? existente = (from e in _equipoContext.equipos where e.id_equipos == id && e.estado =="A" select e).FirstOrDefault();

            if(existente == null)
            {
                return NotFound();
            }
            equipoMod.estado = "A";

            existente.nombre = equipoMod.nombre;
            existente.descripcion = equipoMod.descripcion;

            _equipoContext.Entry(existente).State = EntityState.Modified;
            _equipoContext.SaveChanges();

            return Ok(existente);


        }

        [HttpPut]
        [Route("delete/{id}")]
        public IActionResult EliminarEquipo(int id)
        {
            Equipo? existente = _equipoContext.equipos.Find(id);

            if(existente == null)
            {
                return NotFound();

            }
            //_equipoContext.equipos.Attach(existente);


            existente.estado = "I";
            _equipoContext.Entry(existente).State = EntityState.Modified;


            return Ok(existente);

        }

    }
}
