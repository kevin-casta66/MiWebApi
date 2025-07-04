﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MiWebApi.Data;
using MiWebApi.Models;

namespace MiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly EmpleadoData _empleadoData;

        public EmpleadoController(EmpleadoData empleadoData)
        {
            _empleadoData = empleadoData;

        }

        [HttpGet]      
        public async Task<IActionResult> Lista()
        {
            List<Empleado>Lista=await _empleadoData.Lista();
            return StatusCode(StatusCodes.Status200OK,Lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            Empleado objeto = await _empleadoData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody]Empleado objeto)
        {
            bool respuesta = await _empleadoData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new {isSucess=respuesta});
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Empleado objeto)
        {
            bool respuesta = await _empleadoData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSucess = respuesta });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool respuesta = await _empleadoData.Eliminar(id);
            return StatusCode(StatusCodes.Status200OK, new { isSucess = respuesta });
        }
    }
        
}
