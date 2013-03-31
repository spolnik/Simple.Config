using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Simple.Config.Domain;
using Simple.Config.Errors;

namespace Simple.Config
{
    /// <summary>
    ///     This is the main class which provides the interaction with the
    ///     user (a singleton). The class exposes methods for loading new
    ///     configuration files and getting information about loaded namespaces,
    ///     properties and their values.
    /// </summary>
    public sealed class ConfigManager
    {
        /// <summary>
        ///     The name of the file with the names of the configuration files to
        ///     be preloaded.
        /// </summary>
        private static readonly string PreloadFile = "preload.xml";

        /// <summary>
        ///     The one instance of the class.
        /// </summary>
        private static ConfigManager _instance;

        /// <summary>
        ///     Maps namespace names to Namespace objects.
        /// </summary>
        private readonly Hashtable _namespaceLookup = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        ///     Maps config file names to ConfigFile objects.
        /// </summary>
        private readonly Hashtable _configFileLookup = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        ///     Initializes the preload file location from the application configuration file, using the
        ///     key "Simple.Config.ConfigurationManager.Preload". If this setting is not found, the default
        ///     value is just the "preload.xml".
        /// </summary>
        static ConfigManager()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["Simple.Config.ConfigurationManager.Preload"] != null)
            {
                PreloadFile =
                    System.Configuration.ConfigurationManager.AppSettings["Simple.Config.ConfigurationManager.Preload"];
            }
        }

        /// <summary>
        ///     Private constructor. Prevents instantiation from outside the class.
        ///     Its task is to load the preloadFile and extract from there all
        ///     the names of the configuration files that need to be preloaded.
        ///     Then each file will be loaded.
        /// </summary>
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
        /// 
        /// <exception cref="NullReferenceException">
        ///     If the argument is null.
        /// </exception>
        private ConfigManager()
        {
            PreloadConfigFiles();
        }

        /// <summary>
        ///     Get the instance of the configuration manager.
        /// </summary>
        /// 
        /// <returns>The instance</returns>
        public static ConfigManager GetInstance()
        {
            lock (typeof (ConfigManager))
            {
                return _instance ?? (_instance = new ConfigManager());
            }
        }

        /// <summary>
        ///     Loads a new configuration file.
        /// </summary>
        /// 
        /// <param name="filename">The file to load from.</param>
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
        /// 
        /// <exception cref="NullReferenceException">
        ///     If the filename argument is null.
        /// </exception>
        public IConfigFile Load(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentNullException("filename");

            if (!_configFileLookup.ContainsKey(filename))
            {
                var configFile = new ConfigFile(filename);
                _configFileLookup.Add(filename, configFile);
                InsertNewNamespaces(configFile.Namespaces);
                return configFile;
            }

            return (ConfigFile) _configFileLookup[filename];
        }

        /// <summary>
        ///     This helper method adds new namespaces to the lookup table.
        /// </summary>
        /// 
        /// <param name="namespaces">The new namespaces to be added</param>
        /// 
        /// <exception cref="NamespaceClashException">
        ///     If a namespace clash occurs.
        /// </exception>
        /// 
        /// <exception cref="PropertyClashException">
        ///     If a property clash occurs.
        /// </exception>
        ///
        /// <exception cref="NullReferenceException">
        ///     If the argument is null.
        /// </exception>
        private void InsertNewNamespaces(IEnumerable<Namespace> namespaces)
        {
            if (namespaces == null)
                throw new NullReferenceException();

            foreach (var theNamespace in namespaces)
            {
                if (_namespaceLookup[theNamespace.Name] != null)
                    throw new NamespaceClashException(theNamespace.Name + " is already present");

                lock (this)
                {
                    _namespaceLookup.Add(theNamespace.Name, theNamespace);
                }
            }
        }

        /// <summary>
        ///     Removes the config file and the associated namespaces from the lookup tables.
        /// </summary>
        /// 
        /// <param name="filename">The config filename.</param>
        /// <returns>
        ///     true if the config file was found and removed, false otherwise.
        /// </returns>
        /// 
        /// <exception cref="NullReferenceException">
        ///     If the argument is null.
        /// </exception>
        public bool RemoveFile(string filename)
        {
            if (filename == null)
                throw new NullReferenceException();

            lock (this)
            {
                var configFile = GetConfigFile(filename);

                if (configFile != null)
                {
                    foreach (var theNamespace in configFile.Namespaces)
                        _namespaceLookup.Remove(theNamespace.Name);

                    _configFileLookup.Remove(filename);
                }

                return configFile != null;
            }
        }

        /// <summary>
        ///     The ConfigFiles property.
        /// </summary>
        /// 
        /// <value>
        ///     The array of config files which are currently loaded.
        ///     The results are in no particular order.
        /// </value>
        public IConfigFile[] ConfigFiles
        {
            get
            {
                lock (this)
                {
                    var files = new IConfigFile[_configFileLookup.Count];
                    _configFileLookup.Values.CopyTo(files, 0);
                    return files;
                }
            }
        }

        /// <summary>
        ///  Get a config file by its filename.
        /// </summary>
        /// 
        /// <param name="filename">The config filename.</param>
        /// <returns>
        ///     The config file or null if not found.
        /// </returns>
        /// 
        /// <exception cref="NullReferenceException">
        ///     If argument is null.
        /// </exception>
        public IConfigFile GetConfigFile(string filename)
        {
            if (filename == null)
                throw new NullReferenceException();

            return (ConfigFile) _configFileLookup[filename];
        }

        /// <summary>
        ///     The Namespaces property.
        /// </summary>
        /// 
        /// <value>
        ///     The array of namespaces which are currently loaded.
        ///     The results are in no particular order.
        /// </value>
        public Namespace[] Namespaces
        {
            get
            {
                lock (this)
                {
                    var namespaces = new Namespace[_namespaceLookup.Count];
                    _namespaceLookup.Values.CopyTo(namespaces, 0);
                    return namespaces;
                }
            }
        }

        /// <summary>
        ///     Get a namespace by its name.
        /// </summary>
        /// 
        /// <param name="name">The namespace.</param>
        /// <returns>
        ///     The namespace or null if not found.
        /// </returns>
        /// 
        /// <exception cref="NullReferenceException">
        ///     If argument is null.
        /// </exception>
        public Namespace GetNamespace(string name)
        {
            if (name == null)
                throw new NullReferenceException();

            return (Namespace) _namespaceLookup[name];
        }

        /// <summary>
        ///     Shortcut for getting a namespace by its name.
        ///     Same as GetNamespace(string name).
        /// </summary>
        /// 
        /// <param name="name">The namespace.</param>
        /// <returns>
        ///     The namespace or null if not found.
        /// </returns>
        /// 
        /// <exception cref="NullReferenceException">
        ///     If argument is null.
        /// </exception>
        public Namespace this[string name]
        {
            get { return GetNamespace(name); }
        }

        /// <summary>
        ///     Shortcut for GetNamespace(namespaceString).GetProperty(property).
        ///     Get a property by name.
        /// </summary>
        /// 
        /// <param name="namespaceString">The namespace to look in.</param>
        /// <param name="property">The name of the property.</param>
        /// <returns>
        ///     The property or null if not found.
        /// </returns>
        /// 
        /// <exception cref="NullReferenceException">
        ///     If any argument is null.
        /// </exception>
        public Property GetProperty(string namespaceString, string property)
        {
            return GetNamespace(namespaceString).GetProperty(property);
        }

        /// <summary>
        ///     Shortcut for GetNamespace(namespaceString).GetProperty(property).Value.
        ///     Get the value of a property.
        /// </summary>
        /// 
        /// <param name="namespaceString">The namespace to look in.</param>
        /// <param name="property">The name of the property.</param>
        /// <returns>
        ///     The value of the property (the first value if there are more)
        ///     or null if not found.
        /// </returns>
        /// 
        /// <exception cref="NullReferenceException">
        ///     If any argument is null.
        /// </exception>
        public string GetValue(string namespaceString, string property)
        {
            if (namespaceString == null || property == null)
                throw new ArgumentNullException();

            if (namespaceString.Trim().Length == 0 || property.Trim().Length == 0)
                throw new ArgumentException();

            var theNamespace = GetNamespace(namespaceString);
            if (theNamespace == null)
                return null;

            var theProperty = theNamespace.GetProperty(property);
            if (theProperty == null)
                return null;

            if (theProperty.Values.Count == 0)
                throw new ArgumentNullException();

            return theProperty.Value;
        }

        /// <summary>
        ///     Shortcut for GetNamespace(namespaceString).GetProperty(property).Values.
        ///     Get the values of a property.
        /// </summary>
        /// 
        /// <param name="namespaceString">The namespace to look in.</param>
        /// <param name="property">The name of the property.</param>
        /// <returns>
        ///     The values of the property or null if not found.
        /// </returns>
        /// 
        /// <exception cref="NullReferenceException">
        ///     If any argument is null.
        /// </exception>
        public IList<string> GetValues(string namespaceString, string property)
        {
            if (namespaceString == null || property == null)
                throw new ArgumentNullException();

            if (namespaceString.Trim().Length == 0 || property.Trim().Length == 0)
                throw new ArgumentException();

            var theNamespace = GetNamespace(namespaceString);
            if (theNamespace == null)
                return null;

            var theProperty = theNamespace.GetProperty(property);
            if (theProperty == null)
                return null;

            if (theProperty.Values.Count == 0)
                throw new ArgumentNullException();

            return theProperty.Values;
        }

        /// <summary>
        ///     Clears the config manager of all files and namespaces
        /// </summary>
        public void Clear()
        {
            Clear(true);
        }

        /// <summary>
        ///     Clear the config manager of all files and namespaces and reloads
        ///     the preload files if required.
        /// </summary>
        /// 
        /// <param name="preload">Reload the preload files?</param>
        public void Clear(bool preload)
        {
            lock (this)
            {
                _namespaceLookup.Clear();
                _configFileLookup.Clear();
                if (!preload) 
                    return;

                PreloadConfigFiles();
            }
        }

        private void PreloadConfigFiles()
        {
            if (!File.Exists(PreloadFile))
                return;

            var configFile = new ConfigFile(PreloadFile);
            if (configFile.Namespaces.Count == 0)
                return;

            var filenames = configFile.Namespaces[0].GetProperty("preload").Values;

            foreach (var filename in filenames)
                Load(filename);
        }
    }
}