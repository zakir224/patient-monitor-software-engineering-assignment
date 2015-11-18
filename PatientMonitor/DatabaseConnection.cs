﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientMonitor
{
    class DatabaseConnection
    {
        //attributes
        private static string connectionStr;

        private static DatabaseConnection databaseConnectionInstance;

        //the SqlConnection object used to store the connection to the database 
        private System.Data.SqlClient.SqlConnection connectionToDB;

        // the DataAdapter object used to open a table of the database
        private System.Data.SqlClient.SqlDataAdapter dataAdapter;


        //properties
        public static string ConnectionStr
        {
            set { connectionStr = value; }
        }

        public static DatabaseConnection DatabaseConnectionInstance
        {
            get
            {
                if (databaseConnectionInstance == null)
                    databaseConnectionInstance = new DatabaseConnection();

                return databaseConnectionInstance;
            }
        }

        //methods

        // Open the connection
        public void openConnection()
        {
            // create the connection to the database as an instance of System.Data.SqlClient.SqlConnection
            connectionToDB = new SqlConnection(connectionStr);

            //open the connection
            connectionToDB.Open();
        }


        //close the connection to the database
        public void closeConnection()
        {
            if (connectionToDB != null)
                connectionToDB.Close();

        }


        // Get the data set generated by the sqlStatement
        public DataSet getDataSet(string sqlStatement)
        {
            DataSet dataSet;

            openConnection();

            // create the object dataAdapter to manipulate a table from the database StudentDissertations specified by connectionToDB
            dataAdapter = new SqlDataAdapter(sqlStatement, connectionToDB);

            // create the dataset
            dataSet = new System.Data.DataSet();

            dataAdapter.Fill(dataSet);

            closeConnection();
            //return the dataSet
            return dataSet;

        }



        // insert a row in the table SessionRecord
        public string InsertNewSession(SessionRecord session, string sqlStatement)
        {
            // create the sql connection
            using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
            {
                // create and initialise a sql command
                using (SqlCommand sqlCommand = new SqlCommand(sqlStatement, sqlConnection))
                {
                    // set the parameters of the sql statement
                    sqlCommand.Parameters.AddWithValue("@staff_id", session.StaffId);
                    sqlCommand.Parameters.AddWithValue("@session_start_time", session.StartTime);
                    sqlCommand.Parameters.AddWithValue("@session_end_time", session.EndTime);

                    // open the connection and execute the command containing the sql statement
                    try
                    {
                        sqlConnection.Open();
                        int noOfRows = sqlCommand.ExecuteNonQuery();

                        //return the no of rows inserted(is 1 if executed correctly)
                        return noOfRows+"";
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                        return ex.Message;
                    }
                }
            }

        }


    }
}
