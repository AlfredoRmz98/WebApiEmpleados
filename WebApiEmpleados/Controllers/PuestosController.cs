using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEmpleados.Entidades;

namespace WebApiEmpleados.Controllers
{
    [ApiController]
    [Route("api/clases")]

    public class PuestosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public PuestosController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Puesto>>> GetAll()
        {
            return await dbContext.Puestos.ToListAsync();
        }
        [HttpGet("id:int")]
        public async Task<ActionResult<Puesto>> GetById(int id)
        {
            return await dbContext.Puestos.FirstOrDefaultAsync(x => x.Id == id);
        }
        [HttpPost]
        public async Task<ActionResult> Post(Puesto puesto)
        {
            var existeEmpleado = await dbContext.Empleados.AnyAsync(x => x.Id == puesto.EmpleadoId);

            if (!existeEmpleado)
            {
                return BadRequest($"No existe el alumno con el id: {puesto.EmpleadoId}");
            }

            dbContext.Add(puesto);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Put(Puesto puesto,int id)
        {
            var exist = await dbContext.Puestos.AnyAsync(x => x.Id == id);  

            if (!exist)
            {
                return NotFound("El puesto especificado no existe.");
            }
            if(puesto.Id != id)
            {
                return BadRequest("El id del puesto no coincide con el establecido en la url.");
            }
            dbContext.Update(puesto);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Puestos.AnyAsync(x => x.Id == id);  
            if(!exist)
            {
                return NotFound("El Recurso no fue encontrado.");
            }
            dbContext.Remove(new Puesto {Id = id});
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
