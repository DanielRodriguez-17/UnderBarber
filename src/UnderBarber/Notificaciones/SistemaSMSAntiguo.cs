using System;

namespace UnderBarber.Notificaciones
{
    // Sistema legado que ya existía antes (no se puede modificar).
    public class SistemaSMSAntiguo
    {
        public void EnviarMensajeSMS(string numero, string texto)
        {
            Console.WriteLine($"[SMS antiguo] Enviando a {numero}: {texto}");
        }
    }
}
