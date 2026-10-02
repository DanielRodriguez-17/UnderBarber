using UnderBarber.Servicios;

namespace UnderBarber.Decoradores
{
    public class ConTinte : ServicioDecorator
    {
        public ConTinte(IServicio s) : base(s) { }
        public override string Descripcion => $"{servicioBase.Descripcion} + Tinte";
        public override double Precio => servicioBase.Precio + 25000;
    }
}
