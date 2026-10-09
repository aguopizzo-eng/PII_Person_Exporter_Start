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
    /// Genera reportes de personas en distintos formatos: PDF y HTML.
    /// </summary>
    public class ReportGenerator
    {
        /// <summary>
        /// Genera un reporte de una lista de personas en el formato indicado.
        /// </summary>
        /// <param name="format">Formato de salida del reporte. Valores
        /// esperados: "PDF" o "HTML".</param>
        /// <returns>Retorna <c>true</c> si los reportes fueron generados y
        /// <c>false</c> en caso contrario.</returns>
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
