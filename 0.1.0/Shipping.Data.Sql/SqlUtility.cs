using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Serialization;

using Shipping.Logging;

namespace Shipping.Data.Sql
{
    public static class SqlUtility
    {
        private const string STORED_PROCEDURE_PREFIX = "sp_";

        private static string GetParameterString(SqlParameter[] parameters = null)
        {
            if (parameters == null || parameters.Length == 0)
            {
                return null;
            }

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < parameters.Length; i++)
            {
                builder.Append(parameters[i].ParameterName);
                builder.Append("='");

                if (parameters[i].Value != System.DBNull.Value && parameters[i].Value != null)
                {
                    builder.Append(parameters[i].Value.ToString());
                }
                else
                {
                    builder.Append("null");
                }

                builder.Append("'");

                if (i < parameters.Length - 1)
                {
                    builder.Append(", ");
                }
            }

            return builder.ToString();
        }

        public static T ExecuteXmlStoredProcedure<T>(string connectionString, ILogger logger, string procedureName, SqlParameter[] parameters = null, int sqlCommandTimeout = -1) where T : class
        {
            T returnValue;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                logger.DebugFormat(typeof(SqlUtility), "Opening connection to server '{0}', database '{1}'.", connection.DataSource, connection.Database);
                connection.Open();

                using (SqlCommand command = new SqlCommand(STORED_PROCEDURE_PREFIX + procedureName, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    if (sqlCommandTimeout != -1)
                    {
                        command.CommandTimeout = sqlCommandTimeout;
                    }

                    if (parameters != null && parameters.Length > 0)
                    {
                        command.Parameters.AddRange(CleanParameters(parameters));
                    }

                    XmlReader reader = null;

                    logger.DebugFormat(typeof(SqlUtility), "Executing XML stored procedure {0} {1} on server '{2}', database '{3}'.", command.CommandText, GetParameterString(parameters), connection.DataSource, connection.Database);

                    try
                    {
                        reader = command.ExecuteXmlReader();

                        if (typeof(T) == typeof(string))
                        {
                            if (reader.Read())
                            {
                                returnValue = reader.ReadOuterXml() as T;
                            }
                            else
                            {
                                returnValue = null;
                            }

                        }
                        else
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(T));

                            returnValue = ((T)serializer.Deserialize(reader));
                        }
                    }
                    finally
                    {
                        if (reader != null)
                        {
                            reader.Close();
                            reader = null;
                        }
                    }
                }
            }

            return returnValue;
        }

        private static SqlParameter[] CleanParameters(SqlParameter[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].Value == null)
                    parameters[i].Value = System.DBNull.Value;

            }
            return parameters;
        }
    }
}
