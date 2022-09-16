namespace WebApiEmpleados.Entidades
{
    public class Empleado
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        
        public List<Puesto> puestos { get; set; }
    }
    
}
