//------------------------------------------------------------------------------
// <copyright file="ReportGenerator.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Ucu.Poo.PersonExporter
{
    /// <summary>
    /// Genera reportes de personas delegando en un <see cref="IExporter"/>,
    /// sin conocer el formato concreto de salida.
    /// </summary>
    public class ReportGenerator
    {
        /// <summary>
        /// Genera un reporte usando el exportador indicado.
        /// </summary>
        /// <param name="format">Exportador que define el formato de salida y
        /// la ruta del archivo.</param>
        /// <returns>Retorna <c>true</c> si el reporte fue generado y
        /// <c>false</c> si el exportador es <c>null</c>.</returns>
        public bool GenerateReport(IExporter format)
        {
            if (format != null)
            {
                format.Export();
                return true;
            }

            return false;
        }
    }
}
