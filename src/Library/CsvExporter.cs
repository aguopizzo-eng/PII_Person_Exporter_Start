using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Ucu.Poo.PersonExporter;
using CsvHelper;
using CsvHelper.Configuration;

/// <summary>
/// Exporta una lista de personas a un archivo CSV con encabezado.
/// </summary>
public class CsvExporter : IExporter
{
  /// <summary>
  /// Inicializa una nueva instancia de la clase <see cref="CsvExporter"/>.
  /// </summary>
  /// <param name="people">Lista de personas a incluir en el reporte.</param>
  /// <param name="outputPath">Ruta del archivo CSV que se va a generar.</param>
  public CsvExporter(IList<Person> people, string outputPath)
  {
    this.People = people;
    this.OutputPath = outputPath;
  }

  /// <summary>
  /// Obtiene o establece la lista de personas que se exportan.
  /// </summary>
  public IList<Person> People { get; set; }

  /// <inheritdoc />
  public string OutputPath { get; set; }

  /// <inheritdoc />
  public void Export()
  {
    CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
          HasHeaderRecord = true,
    };

    using (StreamWriter writer = new StreamWriter(this.OutputPath, false, Encoding.UTF8))
    {
          using (CsvWriter csv = new CsvWriter(writer, config))
          {
            csv.WriteHeader<Person>();
            csv.NextRecord();
            csv.WriteRecords(this.People);
          }
    }
  }
}