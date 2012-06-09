using System;
using Microsoft.Phone.Info;
using System.Net.NetworkInformation;
using System.Text;

namespace WindowsPhone.Utilities
{
    /// <summary>
    /// Provides access to some common device properties.
    /// </summary>
    public class Device
    {
        #region Singleton

        private static volatile Device _instance;

        private static object syncRoot = new object();

        private Device()
        {
        }

        /// <summary>
        /// Device properties.
        /// </summary>
        public static Device Properties
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new Device();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region Constants

        static readonly int ANID_LENGTH = 32;
        static readonly int ANID_OFFSET = 2;
        static readonly int SESSION_ID_LENGTH = 14;

        #endregion

        #region Members

        /// <summary>
        /// Random number to generate random alphanumeric strings.
        /// </summary>
        static Random _random = new Random((int)DateTime.Now.Ticks);

        #endregion

        #region Properties

        /// <summary>
        /// Returns the manufacturer of the device.
        /// </summary>
        public string Manufacturer
        {
            get { return DeviceExtendedProperties.GetValue("DeviceManufacturer").ToString(); }
        }

        /// <summary>
        /// Returns the unique device identifier of the device.
        /// </summary> 
        public string DeviceID
        {
            get
            {
                byte[] deviceID = (byte[])DeviceExtendedProperties.GetValue("DeviceUniqueId");

                StringBuilder result = new StringBuilder();

                foreach (byte number in deviceID)
                {
                    result.Append(number);
                }

                return result.ToString();
            }
        }

        /// <summary>
        /// Returns the unique user Windows Live Anonymous identifier.
        /// </summary> 
        public string WindowsLiveAnonymousID
        {
            get
            {
                string result = string.Empty;
                object anid;
                if (UserExtendedProperties.TryGetValue("ANID", out anid))
                {
                    if (anid != null && anid.ToString().Length >= (ANID_LENGTH + ANID_OFFSET))
                    {
                        result = anid.ToString().Substring(ANID_OFFSET, ANID_LENGTH);
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Returns the assembly application version number.
        /// </summary>
        public string ApplicationVersion
        {
            get
            {
                const string VERSION_LABEL = "Version=";
                string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().FullName;
                int startIndex = assemblyName.IndexOf(VERSION_LABEL) + VERSION_LABEL.Length;
                int endIndex = assemblyName.IndexOf(',', startIndex + 1);
                string version = assemblyName.Substring(startIndex, endIndex - startIndex);

                return string.Format("v{0}_{1}", version[0], version[2]);
            }
        }

        /// <summary>
        /// Generates a random identifier for the current session of usage.
        /// </summary>
        public string SessionID
        {
            get
            {
                string charPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
                StringBuilder builder = new StringBuilder();

                int length = SESSION_ID_LENGTH;
                while (length-- > 0)
                    builder.Append(charPool[(int)(_random.NextDouble() * charPool.Length)]);

                return builder.ToString();
            }
        }

        /// <summary>
        /// Detects whether an active Internet connection exists.
        /// </summary>
        public bool HasInternetConnectivity
        {
            get { return NetworkInterface.GetIsNetworkAvailable(); }
        }

        #endregion
    }
}
