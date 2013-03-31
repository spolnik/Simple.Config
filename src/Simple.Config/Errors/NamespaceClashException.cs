using System;

namespace Simple.Config.Errors
{
    /// <summary>
    ///     Exception thrown when a namespace name conflict occurs
    ///     (a namespace is loaded and a namespace with the same name 
    ///     already exists).
    /// </summary>
    public sealed class NamespaceClashException: Exception
    {
        /// <param name="message">the error message</param>
        internal NamespaceClashException(string message): base(message)
        {
        }
    }
}

