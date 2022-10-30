using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEmpleados.Entidades;
using WebApiEmpleados.Filtros.WebApiEmpleados.Filtros;
using WebApiEmpleados.Services;


namespace WebApiEmpleados.Controllers
{
    [ApiController]
    [Route("api/empleados")] 
    [Authorize]
    public class EmpleadosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<EmpleadosController> logger;
        private readonly IWebHostEnvironment env;

        public EmpleadosController(ApplicationDbContext context, IService service,
            ServiceTransient serviceTransient, ServiceScoped serviceScoped,
            ServiceSingleton serviceSingleton, ILogger<EmpleadosController> logger,
            IWebHostEnvironment env)
        {
            this.dbContext = context;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
            this.env = env;
        }
        [HttpGet("GUID")]
        [ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(FiltroDeAccion))]
        public ActionResult ObtenerGuid()
        {
            //throw new NotImplementedException();
            logger.LogInformation("Durante la ejecucion");
            return Ok(new
            {
                EmpleadosControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                EmpleadosControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                EmpleadosControllerSingleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Empleado empleado)
        {
            //Validar que no se ingrese el mismo empleado dos veces
            var existeEmpleadoMismoNombre = await dbContext.Empleados.AnyAsync(x => x.Nombre == empleado.Nombre); 

            if(existeEmpleadoMismoNombre)
            { 
                return BadRequest("Ya existe un empleado con el nombre");
            }
            dbContext.Add(empleado);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [HttpGet("listado")] //api/empleados/listado
        [HttpGet("/listado")] // /listado   -se esta sobreescribiendo la ruta del controlador
        //[Authorize]
        public async Task<ActionResult<List<Empleado>>> Get()
        {
            //* Niveles de logs
            // Critical
            // Error
            // Warning
            // Information
            // Debug
            // Trace
            // */
            throw new NotImplementedException();
            logger.LogInformation("Se obtiene el listado de alumnos");
            logger.LogWarning("Mensaje de prueba warning");
            service.EjecutarJob();
            return await dbContext.Empleados.Include(x => x.puestos).ToListAsync();
        }
        [HttpPut("{id:int}")]// api/empleados/1
        public async Task<ActionResult> Put(Empleado empleado, int id)
        {
            if (empleado.Id != id)
            {
                return BadRequest("El id del alumno no coincide con el establecido en la url.");
            }

            dbContext.Update(empleado);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("primero")] //api/empleados/primero?
        public async Task<ActionResult<Empleado>> PrimerEmpleado([FromHeader] int valor, [FromQuery] string empleado, [FromQuery] int empleadoId)
        {
           return await dbContext.Empleados.FirstOrDefaultAsync();
            
        }
        [HttpGet("primero2")] //api/empleados/primero
        public  ActionResult<Empleado> PrimerEmpleadoD()
        {
            return new Empleado() { Nombre = "DOS"};

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Empleado>> Get (int id)
        {
            var empleado = await dbContext.Empleados.FirstOrDefaultAsync(x => x.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }
            return empleado;
        }
        [HttpGet("{nombre}")]
        public async Task<ActionResult<Empleado>> Get([FromRoute] string nombre)
        {
            var empleado = await dbContext.Empleados.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));
            if (empleado == null)
            {
                logger.LogError("No se encuentra el empleado. ");
            return NotFound();
            }
             return empleado;
        }
        [HttpGet("{id:int}/{param=Alfredo}")] //para que el parametro no sea obligatorio se coloca un signo de ?
        public async Task<ActionResult<Empleado>> Get(int id, string param)
        {
            var empleado = await dbContext.Empleados.FirstOrDefaultAsync(x => x.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }
            return empleado;
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Empleados.AnyAsync(x => x.Id == id);
            if(!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Empleado()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
