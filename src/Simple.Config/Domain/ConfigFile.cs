using System.Collections.Generic;
using System.Linq;
using Simple.Config.Errors;
using Simple.Config.Handlers;

namespace Simple.Config.Domain
{
    /// <summary>
    ///     This class represents a configuration file.
    /// </summary>
    internal sealed class ConfigFile : IConfigFile
    {
        /// <summary>
        ///     The currently implemented configuration file handlers are
        ///     listed here. When an additional handler is implemented it
        ///     should be added here.
        /// </summary>
        private static readonly IConfigHandler[] ConfigHandlers =
            {
                new XmlConfigHandler(),
                new IniConfigHandler()
            };

        /// <summary>
        ///     The contained namespaces.
        /// </summary>
        private readonly List<Namespace> _namespaces;

        /// <summary>
        ///     Internal constructor.
        ///     It tries to find an appropriate handler for the file by
        ///     iterating through a list of implemented handlers and then
        ///     loads the file using the handler.
        /// </summary>
        /// <param name="filename">The filename to process.</param>
        internal ConfigFile(string filename)
        {
            var fileHandler =
                ConfigHandlers.FirstOrDefault(fileHandlerObject => fileHandlerObject.Supports(filename));

            if (fileHandler == null)
                throw new UnknownFormatException();

            _namespaces = fileHandler.LoadFromFile(filename);
        }

        /// <summary>
        ///     The Namespaces value.
        /// </summary>
        /// <value>
        ///     The list of Namespace objects in the configuration file.
        /// </value>
        public IList<Namespace> Namespaces
        {
            get { return _namespaces.AsReadOnly(); }
        }
    }
}