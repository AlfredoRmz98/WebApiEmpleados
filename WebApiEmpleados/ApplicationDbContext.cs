using Microsoft.EntityFrameworkCore;
using WebApiEmpleados.Entidades;

namespace WebApiEmpleados
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
          
        }
         public DbSet<Empleado> Empleados { get; set; }
         public DbSet<Puesto> Puestos { get; set; }
    }
    

}
