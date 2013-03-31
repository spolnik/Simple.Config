using System.Collections.Generic;
using System.IO;
using Simple.Config.Domain;
using Simple.Config.Errors;

namespace Simple.Config.Handlers
{
    /// <summary>
    ///     This interface abstract a configuration file handler.
    ///     A configuration file handler has the task of identifying whether
    ///     it can process the file, loading namespaces with their properties
    ///     from a file.
    /// </summary>
    internal interface IConfigHandler
    {
        /// <summary>
        ///     Determines whether the file is supported by the handler,
        ///     by looking at the extension.
        /// </summary>
        /// 
        /// <param name="filename">The name of the file.</param>
        /// <returns>Whether the file is supported or not.</returns>
        /// 
        /// <exception cref="IOException">
        ///     If an I/O error occurs.
        /// </exception>
        bool Supports(string filename);
        
        /// <summary>
        ///     Loads the namespaces and properties from the file into
        ///     Namespace and Property objects.
        /// </summary>
        /// 
        /// <param name="filename">The name of the file.</param>
        /// <returns>The populated Namespace objects</returns>
        /// 
        /// <exception cref="NamespaceClashException">
        ///     If a namespace clash occurs.
        /// </exception>
        /// 
        /// <exception cref="PropertyClashException">
        ///     If a property clash occurs.
        /// </exception>
        /// 
        /// <exception cref="InvalidConfigFileException">
        ///     If the configuration file is invalid.
        /// </exception>
        /// 
        /// <exception cref="UnknownFormatException">
        ///     If the format of the configuration file is unkown.
        /// </exception>
        /// 
        /// <exception cref="IOException">
        ///     If an I/O error occurs.
        /// </exception>
        List<Namespace> LoadFromFile(string filename);
    }
}
