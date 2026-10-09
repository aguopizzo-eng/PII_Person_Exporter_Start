using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ucu.Poo.PersonExporter;

/// <summary>
/// Exporta una lista de personas a un archivo Markdown con una tabla.
/// </summary>
public class MdExporter : IExporter
{
  /// <summary>
  /// Inicializa una nueva instancia de la clase <see cref="MdExporter"/>.
  /// </summary>
  /// <param name="people">Lista de personas a incluir en el reporte.</param>
  /// <param name="outputPath">Ruta del archivo Markdown que se va a
  /// generar.</param>
  public MdExporter(IList<Person> people, string outputPath)
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
    StringBuilder sb = new StringBuilder();
    sb.AppendLine("# Person Report");
    sb.AppendLine();
    sb.AppendLine("| First Name | Last Name | Age |");
    sb.AppendLine("|-----------|-----------|-----|");

    foreach (Person person in this.People)
    {
        sb.AppendLine($"| {EscapeMarkdown(person.FirstName)} | {EscapeMarkdown(person.LastName)} | {person.Age} |");
    }

    string markdown = sb.ToString();
    File.WriteAllText(this.OutputPath, markdown, Encoding.UTF8);
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
}