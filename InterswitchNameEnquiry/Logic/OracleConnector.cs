using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;

namespace InterswitchNameEnquiry.Logic
{
    public class OracleConnector
    {
        public static double GetNumericValue(string sqlQuery, string oracleConnectionString)
        {
            double myValue;
            using (OleDbConnection myConnection = new OleDbConnection(oracleConnectionString))
            {
                using (OleDbCommand myCommand = new OleDbCommand(sqlQuery, myConnection))
                {
                    myCommand.CommandType = CommandType.Text;
                    myConnection.Open();
                    try
                    {
                        myValue = Conversions.ToDouble(myCommand.ExecuteScalar());
                    }
                    catch (Exception exception)
                    {
                        ProjectData.SetProjectError(exception);
                        myValue = 0;
                        ProjectData.ClearProjectError();
                    }
                }
                myConnection.Close();
            }
            return myValue;
        }

        public static OleDbDataReader GetRows(string sqlQuery, string oracleConnectionString)
        {
            OleDbConnection myConnection = new OleDbConnection(oracleConnectionString);
            OleDbCommand myCommand = new OleDbCommand(sqlQuery, myConnection)
            {
                CommandType = CommandType.Text
            };
            myConnection.Open();
            return myCommand.ExecuteReader();
        }

        public static string GetStringValue(string sqlQuery, string oracleConnectionString)
        {
            string myValue;
            using (OleDbConnection myConnection = new OleDbConnection(oracleConnectionString))
            {
                using (OleDbCommand myCommand = new OleDbCommand(sqlQuery, myConnection))
                {
                    myCommand.CommandType = CommandType.Text;
                    myConnection.Open();
                    myValue = Conversions.ToString(myCommand.ExecuteScalar());
                }
                myConnection.Close();
            }
            return myValue;
        }
    }
}
