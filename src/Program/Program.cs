//------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Ucu.Poo.PersonExporter
{
    /// <summary>
    /// Ejemplo de uso de la clase ReportGenerator. Crea varias personas y
    /// genera el reporte en un único formato elegido por el usuario.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Punto de entrada al programa.
        /// </summary>
        private static void Main()
        {
            List<Person> people = new List<Person>
            {
                new Person { FirstName = "Alice", LastName = "Johnson", Age = 30 },
                new Person { FirstName = "Bob", LastName = "Smith", Age = 42 },
                new Person { FirstName = "Charlie", LastName = "Brown", Age = 25 },
            };

            ReportGenerator generator = new ReportGenerator();

            Console.WriteLine("Seleccione el formato de reporte:");
            Console.WriteLine("1 - HTML");
            Console.WriteLine("2 - PDF");
            Console.Write("Opción: ");

            string option = Console.ReadLine();
            string format;
            string outputPath;

            if (option == "1")
            {
                format = "HTML";
                outputPath = "persons-report.html";
            }
            else if (option == "2")
            {
                format = "PDF";
                outputPath = "persons-report.pdf";
            }
            else
            {
                Console.WriteLine("Opción no válida. Saliendo del programa.");
                return;
            }

            bool result = generator.GenerateReport(people, format, outputPath);

            if (result)
            {
                Console.WriteLine("Reporte generado en el directorio actual:");
                Console.WriteLine(outputPath);
            }
            else
            {
                Console.WriteLine("No se pudo generar el reporte.");
            }
        }
    }
}
