using HotelManagement.Models;
using HotelManagement.Services;

namespace HotelManagement.Helpers
{
    public class Validaciones
    {
        public static void ValidarNoNulo<T>(T objeto, string nombreDato)
        {
            if (objeto == null)
                throw new ArgumentNullException(nombreDato, " no puede ser nulo.");
        }

        public static void ValidarValorPositivo(int numero, string nombreDato)
        {
            if(numero < 0)
                throw new ArgumentOutOfRangeException($"El {nombreDato} no puede ser negativo.");
        }

        public static void ValidarTextoNoVacio(string texto, string nombreDato)
        {
            if(string.IsNullOrWhiteSpace(texto))
                throw new ArgumentNullException($"El {nombreDato} no puede ser nulo ni contener espacios");
        }
    }
}