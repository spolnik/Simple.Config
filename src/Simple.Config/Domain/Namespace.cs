using System;
using System.Collections;
using System.Collections.Generic;
using Simple.Config.Errors;

namespace Simple.Config.Domain
{
    /// <summary>
    ///     A namespace groups logically a set of properties.
    ///     As expected, the namespace contains a set of Property objects and
    ///     properties and methods to access them.
    /// </summary>
    public sealed class Namespace
    {
        /// <summary>
        ///     The name of the namespace
        /// </summary>
        private readonly string _name;
        
        /// <summary>
        ///     The properties contained in the namespace
        ///     in the order in which they appear in the configuration file.
        /// </summary>
        private readonly List<Property> _properties = new List<Property>();
        
        /// <summary>
        ///     A hash table mapping property names to Property objects
        ///     to speed up the lookup of properties by name.
        /// </summary>
        private readonly Hashtable _propertiesLookup = Hashtable.Synchronized(new Hashtable());
        
        /// <param name="name">The name of the namespace</param>
        internal Namespace(string name)
        {
            _name = name;
        }
        
        /// <summary>
        ///     Add a new property to the namespace.
        /// </summary>
        /// 
        /// <param name="property">The property to add</param>
        /// 
        /// <exception cref="PropertyClashException">
        ///     If a property clash occurs.
        /// </exception>
        internal void AddProperty(Property property)
        {
            lock(this)
            {
                if (!_propertiesLookup.ContainsKey(property.Name))
                {
                    _properties.Add(property);
                    _propertiesLookup.Add(property.Name, property);
                    return;
                }
                
                throw new PropertyClashException(property.Name + " clashed with a currently existing property.");
            }
        }
        
        /// <summary>
        ///     The Name property.
        /// </summary>
        /// 
        /// <value>
        ///     The name of the namespace.
        /// </value>
        public string Name
        {
            get { return _name; }
        }
        
        /// <summary>
        ///     The Properties property.
        /// </summary>
        /// 
        /// <value>
        ///     The array of properties owned by the namespace.
        /// </value>
        public Property[] Properties
        {
            get { return _properties.ToArray(); }
        }
        
        /// <summary>
        ///     Get the property with the specified name.
        /// </summary>
        /// 
        /// <param name="name">The name of the property</param>
        /// <returns>The property or null if not found.</returns>
        /// 
        /// <exception cref="NullReferenceException">
        ///     if name is null
        /// </exception>
        public Property GetProperty(string name)
        {
            if (name == null)
                throw new NullReferenceException();

            return (Property)_propertiesLookup[name];
        }
        
        /// <summary>
        ///     Shortcut for accessing a single value property by its name.
        ///     Same as GetProperty(string name).Value
        /// </summary>
        /// 
        /// <param name="name">The name of the property</param>
        /// 
        /// <exception cref="NullReferenceException">
        ///     if name is null
        /// </exception>
        public string this[string name]
        {
            get
            {
                if (name == null)
                    throw new NullReferenceException();

                return GetProperty(name).Value;
            }
        }
    }
}
