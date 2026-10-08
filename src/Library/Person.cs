//------------------------------------------------------------------------------
// <copyright file="Person.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Ucu.Poo.PersonExporter
{
    /// <summary>
    /// Representa una persona.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Obtiene o  establece el nombre de pila de la persona.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el apellido de la persona.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene la edad de la persona calculada en base a la fecha de
        /// nacimiento y la fecha actual.
        /// </summary>
        public int Age { get; set; }
    }
}
