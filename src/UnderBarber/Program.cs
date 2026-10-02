using UnderBarber.Facade;

namespace UnderBarber
{
    class Program
    {
        static void Main(string[] args)
        {
            var barberia = new BarberiaFacade();

            barberia.AgendarCita("Juan", "3001234567", "corte", true, false);
            barberia.AgendarCita("Pedro", "3007654321", "combo", false, true);
            barberia.AgendarCita("Luis", "3009998888", "barba", false, false);
            barberia.AgendarCita("Ana", "3005554444", "cejas", false, false);
        }
    }
}
