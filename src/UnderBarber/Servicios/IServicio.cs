namespace UnderBarber.Servicios
{
    // PATRÓN CREACIONAL: FACTORY METHOD (interfaz de producto)
    public interface IServicio
    {
        string Descripcion { get; }
        double Precio { get; }
    }
}
