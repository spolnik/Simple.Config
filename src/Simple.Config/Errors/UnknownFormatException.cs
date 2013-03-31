using System;

namespace Simple.Config.Errors
{
    /// <summary>
    ///     Exception thrown when the configuration file format is unknown
    ///     (no handler supports it).
    /// </summary>
    [Serializable]
    public sealed class UnknownFormatException : Exception
    {
        internal UnknownFormatException()
        {
        }
    }
}
