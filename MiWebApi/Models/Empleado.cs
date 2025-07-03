namespace MiWebApi.Models
{
    public class Empleado
    {
        public int idEmpleado { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }

        public string telefono { get; set; }
        public string fechaContrato { get; set; }

        public string cargo { get; set; }

        public string departamento { get; set; }

        public bool activo { get; set; }



    }
}
