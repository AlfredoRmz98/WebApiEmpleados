using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEmpleados.Entidades;

namespace WebApiEmpleados.Controllers
{
    [ApiController]
    [Route("api/empleados")]
    public class EmpleadosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmpleadosController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post(Empleado empleado)
        {
            dbContext.Add(empleado);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<List<Empleado>>> Get()
        {
            return await dbContext.Empleados.Include(x => x.puestos).ToListAsync();
        }
        [HttpPut("{id:int}")]// api/empleados/1
        public async Task<ActionResult> Put(Empleado empleado, int id)
        {
            if(empleado.Id == id)
            {
                return BadRequest("El id del alumno no coincide con el establecido en la url.");
            }

            dbContext.Update(empleado);
            await dbContext.SaveChangesAsync();
            return Ok();
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
