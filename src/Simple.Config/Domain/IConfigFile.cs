using System.Collections.Generic;

namespace Simple.Config.Domain
{
    /// <summary>
    ///     This interface represents abstraction for a configuration file.
    /// </summary>
    public interface IConfigFile
    {
        /// <summary>
        ///     The Namespaces value.
        /// </summary>
        /// <value>
        ///     The list of Namespace objects in the configuration file.
        /// </value>
        IList<Namespace> Namespaces { get; }
    }
}