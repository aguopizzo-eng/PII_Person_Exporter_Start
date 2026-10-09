using System.Collections.Generic;
using Ucu.Poo.PersonExporter;

/// <summary>
/// Clase base abstracta para los exportadores de personas. Contiene los datos
/// comunes a todos los formatos; cada subclase define cómo se genera el
/// archivo de salida.
/// </summary>
public abstract class Exporter
{
  /// <summary>
  /// Inicializa una nueva instancia de la clase <see cref="Exporter"/>.
  /// </summary>
  /// <param name="people">Lista de personas a incluir en el reporte.</param>
  /// <param name="outputPath">Ruta del archivo de salida que se va a
  /// generar.</param>
  public Exporter(List<Person> people, string outputPath)
  {
    this.People = people;
    this.OutputPath = outputPath;
  }

  /// <summary>
  /// Obtiene la ruta del archivo de salida.
  /// </summary>
  public string OutputPath { get; }

  /// <summary>
  /// Obtiene la lista de personas que se exportan.
  /// </summary>
  public List<Person> People { get; }

  /// <summary>
  /// Genera el archivo de salida en el formato de la subclase.
  /// </summary>
  public abstract void Export();
}