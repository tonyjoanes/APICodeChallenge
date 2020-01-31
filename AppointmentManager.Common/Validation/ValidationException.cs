using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManager.Common.Validation
{
    /// <summary>
    /// Validation Exception
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ValidationException"/> class
        /// </summary>
        public ValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="message">The Message.</param>
        public ValidationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="message">The Message.</param>
        /// <param name="innerException">The Inner Exception.</param>
        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
