﻿namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// A location object with information about a Url.
    /// </summary>
    sealed class Location : ILocation
    {
        #region Fields

        readonly Url _url;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new location with no URL.
        /// </summary>
        internal Location()
            : this(String.Empty)
        {
        }

        /// <summary>
        /// Creates a new location based on the given URL.
        /// </summary>
        /// <param name="url">The URL to represent.</param>
        internal Location(String url)
        {
            _url = new Url(url);
        }

        #endregion

        #region Properties

        public String Origin
        {
            get { return _url.Href; }
        }

        /// <summary>
        /// Gets if the stored location is relative and requires
        /// a base URL.
        /// </summary>
        public Boolean IsRelative
        {
            get { return _url.IsRelative; }
        }

        /// <summary>
        /// Gets or sets the username for authorization.
        /// </summary>
        public String UserName
        {
            get { return _url.UserName; }
            set { _url.UserName = value; }
        }

        /// <summary>
        /// Gets or sets the password for authorization.
        /// </summary>
        public String Password
        {
            get { return _url.Password; }
            set { _url.Password = value; }
        }

        /// <summary>
        /// Gets the additional stored data of the URL.
        /// This is data that could not be assigned.
        /// </summary>
        public String Data
        {
            get { return _url.Data; }
        }

        /// <summary>
        /// Gets or sets the hash, e.g.  "#myhash".
        /// </summary>
        public String Hash
        {
            get { return NonEmptyPrefix(_url.Fragment, "#"); }
            set { _url.Fragment = value; }
        }

        /// <summary>
        /// Gets or sets the host, e.g. "localhost:8800" or "www.w3.org".
        /// </summary>
        public String Host
        {
            get { return _url.Host; }
            set { _url.Host = value; }
        }

        /// <summary>
        /// Gets or sets the host name, e.g. "localhost" or "www.w3.org".
        /// </summary>
        public String HostName
        {
            get { return _url.HostName; }
            set { _url.HostName = value; }
        }

        /// <summary>
        /// Gets or sets the hyper reference, i.e. the full path.
        /// </summary>
        public String Href
        {
            get { return _url.Href; }
            set { _url.Href = value; }
        }

        /// <summary>
        /// Gets or sets the pathname, e.g. "/mypath".
        /// </summary>
        public String PathName
        {
            get { return "/" + _url.Path; }
            set { _url.Path = value; }
        }

        /// <summary>
        /// Gets or sets the port, e.g. "8800"
        /// </summary>
        public String Port
        {
            get { return _url.Port; }
            set { _url.Port = value; }
        }

        /// <summary>
        /// Gets or sets the protocol, e.g. "http:".
        /// </summary>
        public String Protocol
        {
            get { return NonEmptyPostfix(_url.Scheme, ":"); }
            set { _url.Scheme = value; }
        }

        /// <summary>
        /// Gets or sets the query, e.g. "?id=...".
        /// </summary>
        public String Search
        {
            get { return NonEmptyPrefix(_url.Query, "?"); }
            set { _url.Query = value; }
        }

        #endregion

        #region Methods

        public void Assign(String url)
        {
            _url.Href = url;
        }

        public void Replace(String url)
        {
            _url.Href = url;
        }

        public void Reload()
        {
            _url.Href = Href;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns the string representation of the current location.
        /// </summary>
        /// <returns>The string that equals the hyper reference.</returns>
        public override String ToString()
        {
            return _url.Href;
        }

        #endregion

        #region Helpers

        static String NonEmptyPrefix(String check, String prefix)
        {
            if (String.IsNullOrEmpty(check))
                return String.Empty;

            return String.Concat(prefix, check);
        }

        static String NonEmptyPostfix(String check, String postfix)
        {
            if (String.IsNullOrEmpty(check))
                return String.Empty;

            return String.Concat(check, postfix);
        }

        #endregion

        #region Internal Helpers

        /// <summary>
        /// Checks if the given URL is an absolute URI.
        /// </summary>
        /// <param name="url">The given URL.</param>
        /// <returns>True if the url is absolute otherwise false.</returns>
        internal static Boolean IsAbsolute(String url)
        {
            return new Location(url).IsRelative == false;
        }

        /// <summary>
        /// Creates an absolute URI out of the given baseURI and (relative) URL.
        /// </summary>
        /// <param name="basePath">The baseURI of the page or element.</param>
        /// <param name="relativePath">The relative path for the URL creation.</param>
        /// <returns>THe absolute URI created out of the baseURI and pointing to the relative path.</returns>
        internal static String MakeAbsolute(String basePath, String relativePath)
        {
            Uri baseUri;
            
            if (Uri.TryCreate(basePath, UriKind.Absolute, out baseUri))
                return new Uri(baseUri, relativePath).ToString();

            return relativePath;
        }

        #endregion
    }
}
