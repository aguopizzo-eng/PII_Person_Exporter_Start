using System;
using System.Collections.Generic;
using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Ucu.Poo.PersonExporter;

/// <summary>
/// Exporta una lista de personas a un archivo PDF con una tabla.
/// </summary>
public class PdfExporter : Exporter
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="PdfExporter"/>.
    /// </summary>
    /// <param name="people">Lista de personas a incluir en el reporte.</param>
    /// <param name="outputPath">Ruta del archivo PDF que se va a generar.</param>
    public PdfExporter(List<Person> people, string outputPath)
  : base (people, outputPath)
  {
  }

    /// <inheritdoc />
    public override void Export()
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
                foreach (Person person in this.People)
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

        document.GeneratePdf(this.OutputPath);
    }

    private IContainer CellStyle(IContainer container)
    {
        return container
            .Padding(4)
            .Border(1)
            .BorderColor("#CCCCCC");
    }
}