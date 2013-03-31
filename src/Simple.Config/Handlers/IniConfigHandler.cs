using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Simple.Config.Domain;
using Simple.Config.Errors;

namespace Simple.Config.Handlers
{
    /// <summary>
    ///     A configuration file handler for INI files.
    /// </summary>
    internal sealed class IniConfigHandler : IConfigHandler
    {
        private const int DefaultBufferSize = 4096 * 2;

        /// <summary>
        ///     Determines whether the file is supported by the handler,
        ///     by looking at the extension. In this case
        ///     the extension should be .ini.
        /// </summary>
        /// 
        /// <param name="filename">The name of the file.</param>
        /// <returns>Whether the file is supported or not.</returns>
        /// 
        /// <exception cref="IOException">
        ///     If an I/O error occurs.
        /// </exception>
        public bool Supports(string filename)
        {
            return filename.ToLower().EndsWith(".ini");
        }
        
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
        public List<Namespace> LoadFromFile(string filename)
        {
            var namespaces = new List<Namespace>();

            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, FileOptions.SequentialScan))
            {
                using (var reader = new StreamReader(fs))
                {
                    LoadFile(reader, namespaces);
                }
            }

            return namespaces;
        }

        private static void LoadFile(TextReader reader, ICollection<Namespace> namespaces)
        {
            Namespace currentNamespace = null;
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("#") || line.StartsWith(";"))
                {
                    // Comment line
                }
                else if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    currentNamespace = new Namespace(line.Substring(1, line.Length - 2));
                    namespaces.Add(currentNamespace);
                }
                else if (line.IndexOf("=", StringComparison.Ordinal) != -1)
                {
                    if (currentNamespace != null)
                    {
                        var propertyName = line.Substring(0, line.IndexOf("=", StringComparison.Ordinal)).Trim();
                        var valuesString = line.Substring(line.IndexOf("=", StringComparison.Ordinal) + 1).Trim();

                        var values = valuesString.Split(new[] {';'});
                        
                        for (var i = 0; i < values.Length; i++)
                            values[i] = values[i].Trim();

                        currentNamespace.AddProperty(new Property(propertyName, values.ToList()));
                    }
                    else
                    {
                        throw new InvalidConfigFileException("We've got values, but they're not inside a namespace");
                    }
                }
                else if (line.Length > 0)
                {
                    throw new InvalidConfigFileException("Unsupported line: " + line);
                }
            }
        }
    }
}

