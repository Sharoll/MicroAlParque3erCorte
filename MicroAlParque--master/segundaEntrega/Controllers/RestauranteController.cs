using System.Runtime.ExceptionServices;
using System.ComponentModel;
using System.IO.Pipes;
using System.Security.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entidad;
using Logica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using segundaEntrega.Models;
using Datos;

namespace segundaEntrega.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestauranteController: ControllerBase
    {
          private readonly RestauranteService _restauranteService;
        public RestauranteController(PersonaContext context){
    
            _restauranteService = new RestauranteService(context);
        }

        // GET: api/Restaurante
        [HttpGet()]
        public IEnumerable<RestauranteViewModel> Gets(){
            var restaurantes = _restauranteService.ConsultarTodos().Select(p=> new RestauranteViewModel(p));
            return restaurantes;
        }

        // POST: api/Restaurante
        [HttpPost]
        public ActionResult<RestauranteViewModel> Post(RestauranteInputModel restauranteInput){
            
            Restaurante restaurante = MapearRestaurante(restauranteInput);
            var response = _restauranteService.Guardar(restaurante);

            if (response.Error)
            {
            return BadRequest(response.Mensaje);
            }
            return Ok(response.Restaurante);
        }
        [HttpGet("{nombre}")]
        public ActionResult<RestauranteViewModel> Get(string nombre)
        {
            var response = _restauranteService.Buscar(nombre);
            if(response.Error)
            {
                return BadRequest(response.Mensaje);
            }
            return Ok(response.Restaurante);
        }

        private Restaurante MapearRestaurante(RestauranteInputModel restauranteInput){
            var restaurante = new Restaurante();
                restaurante.NIT=restauranteInput.Nit;
                restaurante.Nombre = restauranteInput.Nombre;
                restaurante.Propietario=restauranteInput.Propietario;
                restaurante.Direccion=restauranteInput.Direccion;
                restaurante.CantidadPersonal=restauranteInput.CantidadPersonal;
                restaurante.Telefono=restauranteInput.Telefono;
                restaurante.Email = restauranteInput.Email;
                restaurante.Sedes=restauranteInput.Sedes;
                restaurante.AñoFuncionamiento=restauranteInput.AñoFuncionamiento;
                restaurante.Especialidad=restauranteInput.Especialidad;
            return restaurante;
        }

        
    }
}