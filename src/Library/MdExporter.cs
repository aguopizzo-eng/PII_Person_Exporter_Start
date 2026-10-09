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
public class MdExporter : IExporter
{
  public IList<Person> People { get; set; }

  public string OutputPath { get; set; }

  public MdExporter(IList<Person> people, string outputPath)
  {
    this.People = people;
    this.OutputPath = outputPath;
  }

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