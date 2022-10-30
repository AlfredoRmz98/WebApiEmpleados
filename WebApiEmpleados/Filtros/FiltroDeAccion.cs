using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiEmpleados.Filtros
{
    namespace WebApiEmpleados.Filtros
    {
         public class FiltroDeAccion : IActionFilter
         {
            private readonly ILogger<FiltroDeAccion> log;

            public FiltroDeAccion(ILogger<FiltroDeAccion> log)
            {
                this.log = log;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                log.LogInformation("Antes de ejecutar la acion");
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                log.LogInformation("Despues de ejecutar la accion");
            }
         }
    }
   
}
