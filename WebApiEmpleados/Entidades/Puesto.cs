namespace WebApiEmpleados.Entidades
{
    public class Puesto
    {
        public int Id { get; set; }

        public string departamento { get; set; }

        public string nombpuesto { get; set; }

        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }
    }
}