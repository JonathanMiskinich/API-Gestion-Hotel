using HotelManagement.Models;
using HotelManagement.Services;
using System.Diagnostics.CodeAnalysis;


namespace HotelManagement.Helpers
{
    public class Validaciones
    {
        public static void ValidarNoNulo<T>([NotNull]T objeto, string nombreDato)
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

        public static void ValidarRangoFechas(DateOnly inicio, DateOnly fin)
        {
            if(inicio > fin)
                throw new InvalidOperationException("La fecha de incio no puede ser despues de la fecha de finalizacion.");
        }
    }
}