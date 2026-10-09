using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Ucu.Poo.PersonExporter;
using CsvHelper;
using CsvHelper.Configuration;

/// <summary>
/// 
/// </summary>
public class CsvExporter : IExporter
{
  public IList<Person> People { get; set; }

  public string OutputPath { get; set; }

  public CsvExporter(IList<Person> people, string outputPath)
  {
    this.People = people;
    this.OutputPath = outputPath;
  }

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