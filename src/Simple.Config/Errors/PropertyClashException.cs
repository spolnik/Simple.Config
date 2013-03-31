using System;

namespace Simple.Config.Errors
{
    /// <summary>
    ///     Exception thrown when a property name conflict occurs
    ///     (a property is loaded and a property with the same name 
    ///     already exists in the owner namespace).
    /// </summary>
    public sealed class PropertyClashException : Exception
    {
        /// <param name="message">the error message</param>
        internal PropertyClashException(string message)
            : base(message)
        {
        }
    }
}
