namespace WebApiEmpleados.Entidades
{
    public class Puesto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Departamento { get; set; }

        public string NPuesto { get; set; }

        public int NSS { get; set; }

        public string CURP { get; set; }

        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }
    }
}