using System;

namespace UnderBarber.Servicios
{
    // PATRÓN CREACIONAL: FACTORY METHOD
    // Crea el tipo de servicio correcto sin que el cliente conozca los
    // detalles de construcción de cada uno.
    public static class ServicioFactory
    {
        public static IServicio Crear(string tipo) => tipo switch
        {
            "corte" => new Corte(),
            "barba" => new Barba(),
            "combo" => new Combo(),
            "cejas" => new Cejas(),
            _ => throw new ArgumentException("Servicio no existe")
        };
    }
}
