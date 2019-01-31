using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Extensiones
{
    /// <summary>
    /// Determina si el objeto System.Object especificado es igual a cualquiera
    /// de la lista actual.
    /// </summary>
    /// <typeparam list="params T[]"></typeparam>
    /// <param list="Objects">
    /// Lista de objetos de la clase System.Object que se va a comparar con la clase System.Object actual.
    /// </param>
    /// <returns>
    /// true si la clase System.Object especificada es igual a cualquier objeto
    /// de la lista actual; de lo contrario, false.
    /// </returns>
    public static bool IsAnyOf<T>(this T source, params T[] list)
    {
        if (null == source) throw new ArgumentException("source is null");
        return list.Contains(source);
    }

    /// <summary>
    /// Determina si el objeto System.Object especificado es igual a cualquiera
    /// de la lista actual.
    /// </summary>
    /// <typeparam list="ICollection<T>"></typeparam>
    /// <param list="Objects">
    /// Lista de objetos de la clase System.Object que se va a comparar con la clase System.Object actual.
    /// </param>
    /// <returns>
    /// true si la clase System.Object especificada es igual a cualquier objeto
    /// de la lista actual; de lo contrario, false.
    /// </returns>
    public static bool IsAnyOf<T>(this T source, ICollection<T> list)
    {
        if (null == source) throw new ArgumentException("source is null");
        return list.Contains(source);
    }

    /// <summary>
    /// Convierte un string numerico a otro string numerico de dos decimales con el
    /// simbolo '$'. Utiliza 'Decimal.ToString("C2")'.
    /// Ej. 123234.1232432 -> $123,234.12.
    /// </summary>
    /// <returns>String numerico de dos decimales con simbolo '$'.</returns>
    public static string MoneyStringFormat(this string source)
    {
        decimal n;
        if (!decimal.TryParse(source, out n))
            throw new ArgumentException("source is not numeric");
        return n.ToString("C2");
    }
}