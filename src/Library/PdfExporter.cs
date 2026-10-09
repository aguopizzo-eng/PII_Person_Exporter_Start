using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Ucu.Poo.PersonExporter;

/// <summary>
/// 
/// </summary>
public class PdfExporter : IExporter
{
    public IList<Person> People { get; set; }

    public string OutputPath { get; set; }

    public PdfExporter(IList<Person> people, string outputPath)
    {
        this.People = people;
        this.OutputPath = outputPath;
    }
    public void Export()
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