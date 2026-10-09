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
public class CsvExporter : Exporter
{
  /// <summary>
  /// Inicializa una nueva instancia de la clase <see cref="CsvExporter"/>.
  /// </summary>
  /// <param name="people">Lista de personas a incluir en el reporte.</param>
  /// <param name="outputPath">Ruta del archivo CSV que se va a generar.</param>
  public CsvExporter(List<Person> people, string outputPath)
  : base (people, outputPath)
  {
  }

  /// <inheritdoc />
  public override void Export()
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