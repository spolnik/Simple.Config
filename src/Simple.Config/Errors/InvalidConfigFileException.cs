using System;

namespace Simple.Config.Errors
{
    /// <summary>
    ///     Exception thrown when an error occurs while loading a configuration
    ///     file (signaling the file is invalid). If the error is caused by
    ///     another exception (XML parsing exceptions for example) then the
    ///     other exception is wrapped as inner exception. I/O exceptions are
    ///     not included here. They should be propagated.
    /// </summary>
    public sealed class InvalidConfigFileException: Exception
    {
        internal InvalidConfigFileException(string message): base(message)
        {
        }

        /// <param name="message">the error message</param>
        /// <param name="inner">the inner exception</param>
        internal InvalidConfigFileException(string message, Exception inner): 
                base(message, inner)
        {
        }
    }
}
