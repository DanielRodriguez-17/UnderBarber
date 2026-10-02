using System;
using UnderBarber.Servicios;
using UnderBarber.Decoradores;
using UnderBarber.Notificaciones;

namespace UnderBarber.Facade
{
    // PATRÓN ESTRUCTURAL: FACADE
    // Esconde toda la complejidad (crear servicio, agregar extras,
    // notificar al cliente) detrás de un solo método simple.
    public class BarberiaFacade
    {
        private readonly INotificador notificador;

        public BarberiaFacade()
        {
            notificador = new AdaptadorSMS(new SistemaSMSAntiguo());
        }

        public void AgendarCita(string cliente, string telefono, string tipoServicio, bool lavado, bool tinte)
        {
            IServicio servicio = ServicioFactory.Crear(tipoServicio);

            if (lavado) servicio = new ConLavado(servicio);
            if (tinte) servicio = new ConTinte(servicio);

            Console.WriteLine($"Cita agendada para {cliente}");
            Console.WriteLine($"Servicio: {servicio.Descripcion}");
            Console.WriteLine($"Precio total: ${servicio.Precio}");

            notificador.Notificar(telefono, $"Hola {cliente}, tu cita fue confirmada: {servicio.Descripcion}");
            Console.WriteLine("---------------------------------------");
        }
    }
}
