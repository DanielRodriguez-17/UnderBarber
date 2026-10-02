using UnderBarber.Servicios;

namespace UnderBarber.Decoradores
{
    public class ConLavado : ServicioDecorator
    {
        public ConLavado(IServicio s) : base(s) { }
        public override string Descripcion => $"{servicioBase.Descripcion} + Lavado";
        public override double Precio => servicioBase.Precio + 5000;
    }
}
