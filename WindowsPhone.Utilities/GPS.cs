using System;
using System.Device.Location;

namespace WindowsPhone.Utilities
{
    /// <summary>
    /// Exposes the location service (GPS) functionality.
    /// </summary>
    public class GPS
    {
        #region Singleton

        private static volatile GPS _instance;

        private static object syncRoot = new object();

        private GPS()
        {
            _gps.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(Gps_StatusChanged);
            _gps.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(Gps_PositionChanged);
        }

        ~GPS()
        {
            if (_gps != null)
            {
                _gps.Stop();
                _gps.Dispose();
            }
        }

        /// <summary>
        /// GPS instance.
        /// </summary>
        public static GPS Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new GPS();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region Members

        GeoCoordinateWatcher _gps = new GeoCoordinateWatcher();

        #endregion

        #region Properties

        /// <summary>
        /// The desired accuracy for data returned from the GPS.
        /// </summary>
        public GeoPositionAccuracy Accuracy
        {
            get { return _gps.DesiredAccuracy; }
            set { _gps = new GeoCoordinateWatcher(value); }
        }

        /// <summary>
        /// The minimum distance that must be travelled between successive PositionChanged events.
        /// </summary>
        public double MovementThreshold
        {
            get { return _gps.MovementThreshold; }
            set { _gps.MovementThreshold = value; }
        }

        /// <summary>
        /// The application’s level of access to the location service.
        /// </summary>
        public GeoPositionPermission Permission
        {
            get { return _gps.Permission; }
        }

        /// <summary>
        /// The most recent position obtained from the GPS.
        /// </summary>
        public GeoPosition<GeoCoordinate> Position
        {
            get { return _gps.Position; }
        }

        /// <summary>
        /// The status of the GPS.
        /// </summary>
        public GeoPositionStatus Status
        {
            get { return _gps.Status; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Starts the GPS.
        /// </summary>
        public void Start()
        {
            _gps.Start();
        }

        /// <summary>
        /// Stops the GPS.
        /// </summary>
        public void Stop()
        {
            _gps.Stop();
        }

        #endregion

        #region Events

        public event EventHandler<GeoPositionStatusChangedEventArgs> StatusChanged;
        public event EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>> PositionChanged;

        #endregion

        #region Event handlers

        void Gps_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            if (StatusChanged != null)
            {
                StatusChanged(this, e);
            }
        }

        void Gps_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (PositionChanged != null)
            {
                PositionChanged(this, e);
            }
        }

        #endregion
    }
}
