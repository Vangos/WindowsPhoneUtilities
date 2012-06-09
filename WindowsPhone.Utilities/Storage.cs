using System;
using System.IO.IsolatedStorage;

namespace WindowsPhone.Utilities
{
    /// <summary>
    /// Provides methods for saving, deleting and loading from application storage.
    /// </summary>
    public class Storage
    {
        #region Singleton

        private static volatile Storage _instance;

        private static object syncRoot = new object();

        private Storage()
        {
        }

        /// <summary>
        /// Storage application settings.
        /// </summary>
        public static Storage Settings
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new Storage();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region Members

        IsolatedStorageSettings _settings = IsolatedStorageSettings.ApplicationSettings;

        #endregion

        #region Public methods

        /// <summary>
        /// Saves the specified property.
        /// </summary>
        /// <param name="key">Property key.</param>
        /// <param name="value">Proeprty value.</param>
        public void Save(string key, object value)
        {
            if (_settings.Contains(key))
            {
                _settings[key] = value;
            }
            else
            {
                _settings.Add(key, value);
            }

            _settings.Save();
        }

        /// <summary>
        /// Deletes the specified property.
        /// </summary>
        /// <param name="key">Property key.</param>
        public void Delete(string key)
        {
            if (_settings.Contains(key))
            {
                _settings.Remove(key);
            }
        }

        /// <summary>
        /// Loads the specified property. If no corresponding key is found, returns null.
        /// </summary>
        /// <param name="key">Property key.</param>
        /// <returns>An object containing the property value.</returns>
        public object Load(string key)
        {
            if (_settings.Contains(key))
            {
                return _settings[key];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Loads the specified property. If no corresponding key is found, the default type value is returned.
        /// </summary>
        /// <typeparam name="ValueType">The type of the value.</typeparam>
        /// <param name="key">Property key.</param>
        /// <returns>The corresponding value if exists. The default type value otherwise.</returns>
        public ValueType Load<ValueType>(string key)
        {
            if (_settings.Contains(key))
            {
                if (_settings[key] is ValueType)
                {
                    return (ValueType)_settings[key];
                }
            }

            return default(ValueType);
        }

        #endregion
    }
}
