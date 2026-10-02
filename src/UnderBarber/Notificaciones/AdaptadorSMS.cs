namespace UnderBarber.Notificaciones
{
    // PATRÓN ESTRUCTURAL: ADAPTER
    // Adapta SistemaSMSAntiguo a la interfaz INotificador que el resto
    // del sistema espera usar, sin modificar el sistema viejo.
    public class AdaptadorSMS : INotificador
    {
        private readonly SistemaSMSAntiguo sistemaViejo;

        public AdaptadorSMS(SistemaSMSAntiguo sistemaViejo)
        {
            this.sistemaViejo = sistemaViejo;
        }

        public void Notificar(string destinatario, string mensaje)
        {
            sistemaViejo.EnviarMensajeSMS(destinatario, mensaje);
        }
    }
}
