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

            Exporter htmlFormat = new HtmlExporter(people, "persons-report.html");
            Exporter pdfFormat = new PdfExporter(people, "persons-report.pdf");
            Exporter mdFormat = new MdExporter(people, "persons-report.md");
            Exporter csvFormat = new CsvExporter(people, "persons-report.csv");

            Dictionary<string, Exporter> options = new Dictionary<string, Exporter>();
            options.Add("1", htmlFormat);
            options.Add("2", pdfFormat);
            options.Add("3", mdFormat);
            options.Add("4", csvFormat);

            ReportGenerator generator = new ReportGenerator();

            Console.WriteLine("Seleccione el formato de reporte:");
            Console.WriteLine("1 - HTML");
            Console.WriteLine("2 - PDF");
            Console.WriteLine("3 - MARKDOWN");
            Console.WriteLine("4 - CSV");
            Console.Write("Opción: ");

            string option = Console.ReadLine();
            
            if (options.ContainsKey(option))
            {
                Exporter selectedExporter = options[option];
                bool result = generator.GenerateReport(selectedExporter);

                if (result)
                {
                    Console.WriteLine("Reporte generado en el directorio actual:");
                    Console.WriteLine(selectedExporter.OutputPath);
                }
                else
                {
                    Console.WriteLine("No se pudo generar el reporte.");
                }
            }
            else
            {
                Console.WriteLine("Opción inválida.");
            }
        }
    }
}
