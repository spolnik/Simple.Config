using System;
using System.Collections.Generic;

namespace Simple.Config.Domain
{
    /// <summary>
    ///     This class corresponds to a property.
    ///     A property has a name and more string values.
    ///     The class exposes properties to access the name and the values.
    /// </summary>
    public sealed class Property
    {
        /// <summary>
        ///     The name of the property.
        /// </summary>
        private readonly string _name;
        
        /// <summary>
        ///     The values of the property (only one element for most properties).
        /// </summary>
        private readonly List<string> _values;

        /// <param name="name">The name of the property</param>
        /// <param name="values">The values of the property (one or more)</param>
        internal Property(string name, List<string> values)
        {
            _name = name;
            _values = values;
        }
        
        /// <summary>
        ///     The Name property.
        /// </summary>
        /// 
        /// <value>
        ///     The name of the property.
        /// </value>
        public string Name
        {
            get { return _name; }
        }
        
        /// <summary>
        ///     The Value property.     
        ///     The getter will return the first element in the array
        ///     if there are more or an empty string if there isn't any.
        /// </summary>
        /// 
        /// <value>
        ///     The value of the property.
        /// </value>
        /// 
        /// <exception cref="NullReferenceException">
        ///     if value is null
        /// </exception>
        public string Value
        {
            get
            {
                if (_values == null)
                    throw new NullReferenceException();

                return _values.Count > 0 ? _values[0] : "";
            }
        }
        
        /// <summary>
        ///     The Values property.
        /// </summary>
        /// 
        /// <value>
        ///     The values of the property.
        /// </value>
        /// 
        /// <exception cref="NullReferenceException">
        ///     if values is null
        /// </exception>
        public IList<string> Values
        {
            get
            {
                if (_values == null)
                    throw new NullReferenceException();

                return _values.AsReadOnly();
            }
        }
    }
}
