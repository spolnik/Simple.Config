using System.Collections.Generic;
using System.IO;
using System.Xml;
using Simple.Config.Domain;
using Simple.Config.Errors;

namespace Simple.Config.Handlers
{
    /// <summary>
    ///     A configuration file handler for XML files as they are defined
    ///     in the component specification.
    /// </summary>
    internal sealed class XmlConfigHandler: IConfigHandler
    {
        /// <summary>
        /// Determines whether the file is supported by the handler
        /// by looking at the extension.In this case
        /// the extension should be .xml.
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
            return filename.ToLower().EndsWith(".xml");
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
            try
            {
                var namespaces = new List<Namespace>();

                using (var reader = new XmlTextReader(filename))
                {
                    while (reader.Read())
                    {
                        if (reader.Name.ToLower() == "namespace" && reader.IsStartElement())
                        {
                            var namespaceName = reader.GetAttribute("name");
                            var currentNamespace = new Namespace(namespaceName);
                            namespaces.Add(currentNamespace);
                            while (reader.Read())
                            {
                                if (reader.Name.ToLower() == "property" && reader.IsStartElement())
                                {
                                    var propertyName = reader.GetAttribute("name");
                                    var valueList = new List<string>();

                                    while (reader.Read())
                                    {
                                        if (reader.Name.ToLower() == "value" && reader.IsStartElement())
                                        {
                                            reader.Read();
                                            valueList.Add(reader.Value);
                                        }
                                        else if (reader.Name.ToLower() == "property" && reader.NodeType == XmlNodeType.EndElement)
                                        {
                                            break;
                                        }
                                    }

                                    currentNamespace.AddProperty(new Property(propertyName, valueList));
                                }
                                else if (reader.Name.ToLower() == "namespace" && reader.NodeType == XmlNodeType.EndElement)
                                {
                                    break;
                                }
                            }
                        }
                        else if (reader.Name.ToLower() == "ConfigManager" && reader.NodeType == XmlNodeType.EndElement)
                        {
                            break;
                        }
                    }
                }

                return namespaces;
            }
            catch (XmlException e)
            {
                throw new InvalidConfigFileException("'" + filename + "' contains errors.", e);
            }
        }
    }
}
