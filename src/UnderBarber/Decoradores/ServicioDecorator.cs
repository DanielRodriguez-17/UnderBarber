using UnderBarber.Servicios;

namespace UnderBarber.Decoradores
{
    // PATRÓN ESTRUCTURAL: DECORATOR (clase base)
    public abstract class ServicioDecorator : IServicio
    {
        protected readonly IServicio servicioBase;

        protected ServicioDecorator(IServicio servicioBase)
        {
            this.servicioBase = servicioBase;
        }

        public abstract string Descripcion { get; }
        public abstract double Precio { get; }
    }
}
