//------------------------------------------------------------------------------
// <copyright file="ReportGenerator.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Ucu.Poo.PersonExporter
{
    /// <summary>
    /// Genera reportes de personas en distintos formatos: PDF y HTML.
    /// </summary>
    public class ReportGenerator
    {
        /// <summary>
        /// Genera un reporte de una lista de personas en el formato indicado.
        /// </summary>
        /// <param name="people">Lista de personas a incluir en el
        /// reporte.</param>
        /// <param name="format">Formato de salida del reporte. Valores
        /// esperados: "PDF" o "HTML".</param>
        /// <param name="outputPath">Ruta completa del archivo de salida que se
        /// va a generar.</param>
        /// <returns>Retorna <c>true</c> si los reportes fueron generados y
        /// <c>false</c> en caso contrario.</returns>
        public bool GenerateReport(IList<Person> people, string format, string outputPath)
        {
            if (people == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(format))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(outputPath))
            {
                return false;
            }

            format = format.Trim().ToUpperInvariant();

            if (format == "HTML")
            {
                this.GenerateHtml(people, outputPath);
                return true;
            }
            else if (format == "PDF")
            {
                this.GeneratePdf(people, outputPath);
                return true;
            }
            else
            {
                return false;
            }
        }

        // Escapa caracteres especiales de Markdown en un texto simple.
        private static string EscapeMarkdown(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            string[] charsToEscape = new[] { "|", "*", "_", "`" };
            string result = value;

            foreach (string c in charsToEscape)
            {
                result = result.Replace(c, "\\" + c, StringComparison.Ordinal);
            }

            return result;
        }

        // Genera un archivo HTML con una tabla de personas.
        private void GenerateHtml(IList<Person> people, string outputPath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"es\">");
            sb.AppendLine("<head>");
            sb.AppendLine("  <meta charset=\"utf-8\" />");
            sb.AppendLine("  <title>Person Report</title>");
            sb.AppendLine("  <style>");
            sb.AppendLine("    table { border-collapse: collapse; width: 100%; }");
            sb.AppendLine("    th, td { border: 1px solid #ccc; padding: 4px 8px; }");
            sb.AppendLine("    th { background-color: #f0f0f0; }");
            sb.AppendLine("  </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("  <h1>Person Report</h1>");
            sb.AppendLine("  <table>");
            sb.AppendLine("    <thead>");
            sb.AppendLine("      <tr>");
            sb.AppendLine("        <th>First Name</th>");
            sb.AppendLine("        <th>Last Name</th>");
            sb.AppendLine("        <th>Age</th>");
            sb.AppendLine("      </tr>");
            sb.AppendLine("    </thead>");
            sb.AppendLine("    <tbody>");

            foreach (Person person in people)
            {
                sb.AppendLine("      <tr>");
                sb.AppendLine($"        <td>{System.Net.WebUtility.HtmlEncode(person.FirstName)}</td>");
                sb.AppendLine($"        <td>{System.Net.WebUtility.HtmlEncode(person.LastName)}</td>");
                sb.AppendLine($"        <td>{person.Age}</td>");
                sb.AppendLine("      </tr>");
            }

            sb.AppendLine("    </tbody>");
            sb.AppendLine("  </table>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }

        // Genera un archivo PDF con una tabla básica de personas.
        private void GeneratePdf(IList<Person> people, string outputPath)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            IDocument document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);
                    page.Size(PageSizes.A4);
                    page.PageColor("#FFFFFF");

                    page.Header()
                        .Text("Person Report")
                        .SemiBold().FontSize(20).AlignCenter();

                    page.Content().Table(table =>
                    {
                        // Definir columnas
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(150);
                            columns.ConstantColumn(150);
                            columns.ConstantColumn(50);
                        });

                        // Encabezados
                        table.Header(header =>
                        {
                            header.Cell().Element(this.CellStyle).Text("First Name");
                            header.Cell().Element(this.CellStyle).Text("Last Name");
                            header.Cell().Element(this.CellStyle).Text("Age");
                        });

                        // Filas
                        foreach (Person person in people)
                        {
                            table.Cell().Element(this.CellStyle).Text(person.FirstName);
                            table.Cell().Element(this.CellStyle).Text(person.LastName);
                            table.Cell().Element(this.CellStyle).Text(person.Age.ToString(CultureInfo.InvariantCulture));
                        }
                    });

                    page.Footer()
                        .AlignRight()
                        .Text(text =>
                        {
                            text.Span("Generated at: ");
                            text.Span(DateTime.Now.ToString("u"));
                        });
                });
            });

            document.GeneratePdf(outputPath);
        }

        // Aplica el estilo común de celda para la tabla del PDF.
        private IContainer CellStyle(IContainer container)
        {
            return container
                .Padding(4)
                .Border(1)
                .BorderColor("#CCCCCC");
        }
    }
}
