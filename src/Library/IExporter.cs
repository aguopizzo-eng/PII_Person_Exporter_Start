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
public interface IExporter
{
  string OutputPath { get; set; }
  void Export();
}