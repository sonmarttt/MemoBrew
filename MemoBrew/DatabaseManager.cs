using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace MemoBrew
{
    public static class DatabaseManager
    {
        private static readonly string _connectionString = Properties.Settings.Default.MemoDataConnectionString;
        private static readonly List<SqlConnection> _activeConnections = new List<SqlConnection>();

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            try
            {
                connection.Open();
                lock (_activeConnections)
                {
                    _activeConnections.Add(connection);
                }
                Debug.WriteLine($"Opened SQL connection. Active connections: {_activeConnections.Count}");
                return connection;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error opening connection: {ex.Message}");
                throw;
            }
        }

        public static void CloseConnection(SqlConnection connection)
        {
            if (connection == null)
                return;

            try
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();

                lock (_activeConnections)
                {
                    _activeConnections.Remove(connection);
                }

                connection.Dispose();

                Debug.WriteLine($"Closed SQL connection. Active connections: {_activeConnections.Count}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error closing connection: {ex.Message}");
            }
        }

        public static void CloseAllConnections()
        {
            lock (_activeConnections)
            {
                Debug.WriteLine($"Closing all {_activeConnections.Count} SQL connections");

                foreach (SqlConnection connection in _activeConnections.ToArray())
                {
                    try
                    {
                        if (connection.State != ConnectionState.Closed)
                            connection.Close();

                        connection.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error closing connection: {ex.Message}");
                    }
                }

                _activeConnections.Clear();

                SqlConnection.ClearAllPools();

                Debug.WriteLine("All SQL connections closed and pools cleared");
            }
        }

        public static bool IsDatabaseAccessible()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Database access check failed: {ex.Message}");
                return false;
            }
        }

        public static bool HasUsers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Users", connection))
                    {
                        int count = (int)command.ExecuteScalar();
                        Debug.WriteLine($"User count in database: {count}");
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error checking users: {ex.Message}");
                return false;
            }
        }

        public static string GetDatabaseFilePath()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(_connectionString);
                if (builder.ContainsKey("AttachDbFilename"))
                {
                    string path = builder["AttachDbFilename"].ToString();
                    if (File.Exists(path))
                    {
                        return path;
                    }
                    else
                    {
                        Debug.WriteLine($"Database file not found at: {path}");
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting database file path: {ex.Message}");
                return null;
            }
        }
    }
}