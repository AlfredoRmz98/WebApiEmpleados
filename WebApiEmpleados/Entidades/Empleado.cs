using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiEmpleados.Validaciones;

namespace WebApiEmpleados.Entidades
{
    public class Empleado 
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:10, ErrorMessage = "El campo {0} solo puede tener hasta 10 caracteres")]

        //Utilizamos esto para ejecutar la validacion desde la carpeta Validaciones.
        [PrimeraLetraMayuscula]

        public string Nombre { get; set; }
        [Range(18,100, ErrorMessage = "El campo Edad no se encuentra dentro del rango")]
        [NotMapped]
        public int Edad { get; set; }

        [CreditCard] //Valida la cantidad de numeros de una tarjeta que son 16
        [NotMapped]
        public  string Tarjeta { get; set; }

        [Url] //valida una URL
        [NotMapped]
        public string Url { get; set; }
        public List<Puesto> puestos { get; set; }
    }
    
}
