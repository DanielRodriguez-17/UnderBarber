namespace UnderBarber.Notificaciones
{
    // Interfaz nueva que el resto del sistema espera usar.
    public interface INotificador
    {
        void Notificar(string destinatario, string mensaje);
    }
}
