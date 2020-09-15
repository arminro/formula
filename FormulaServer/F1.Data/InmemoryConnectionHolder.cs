using Microsoft.Data.Sqlite;
using System;
using System.Data.Common;

namespace F1.Data
{
    /// <summary>
    /// A quasi-singleton class holding the sql connection preventing SqlLite from closing it prematurely
    /// </summary>
    public sealed class InmemoryConnectionHolder : IDisposable
    {
        // based on: https://csharpindepth.com/articles/singleton

        /* this class also uses a simplified version of the c# implementation of the dispose design pattern: 
         * https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose */

        /// <summary>
        /// The property through which the connection can be used
        /// </summary>
        public static DbConnection Connection { get => _conn.Value; }

        private static readonly Lazy<DbConnection> _conn = new Lazy<DbConnection>(() =>
        {
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            return conn;
        });

        private bool _disposed = false;

        private InmemoryConnectionHolder()
        {
        }

        /// <summary>
        /// The method responsible for closing the connection, when the object is disposed of
        /// </summary>
        public void Dispose()
        {
            // if the obj is disposed of, then the dtor does not need to run
            this.Dispose(true);
            GC.SuppressFinalize(this);          
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    var conn = _conn.Value;
                    conn.Close();
                }

                _disposed = true;
            }
        }

        ~InmemoryConnectionHolder() => this.Dispose(false);
    }
}

