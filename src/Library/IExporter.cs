/// <summary>
/// Define el contrato que cumple todo exportador de personas, sin importar el
/// formato de salida.
/// </summary>
public interface IExporter
{
  /// <summary>
  /// Obtiene o establece la ruta del archivo de salida.
  /// </summary>
  string OutputPath { get; set; }

  /// <summary>
  /// Genera el archivo de salida en el formato del exportador.
  /// </summary>
  void Export();
}