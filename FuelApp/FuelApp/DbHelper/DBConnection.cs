using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace FuelApp.DbHelper
{
    public class DBConnection
    {
        public MySqlConnection SQLConnection;
        protected MySqlCommand SQLCommand;
        protected List<MySqlParameter> SQLParameters;
        protected string StoredProcedure;

        internal class GetConfiguration
        {
            public IConfigurationRoot GetConnectionString()
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build();
            }
        }
        public DBConnection()
        {
            SQLConnection = new MySqlConnection();
            var con = new GetConfiguration();
            SQLConnection.ConnectionString = con.GetConnectionString().GetSection("ConnectionStrings").GetSection("CoreDB").Value;
            SQLConnection.Open();
            SQLCommand = new MySqlCommand();
            SQLCommand.Connection = SQLConnection;
            SQLCommand.CommandType = CommandType.StoredProcedure;
            SQLParameters = new List<MySqlParameter>();
        }

        protected DataTable ExecuteSelect()
        {
            try
            {
                SQLCommand.CommandText = StoredProcedure;
                this.SQLCommand.Parameters.Clear();

                if (SQLParameters != null)
                {
                    this.SQLCommand.Parameters.Clear();
                    foreach (MySqlParameter item in SQLParameters)
                    {
                        this.SQLCommand.Parameters.Add(item);
                    }
                }
                MySqlDataAdapter da = new MySqlDataAdapter(SQLCommand);
                DataTable dt = new DataTable();
                da.Fill(dt);
                SQLParameters.Clear();
                SQLConnection.Close();
                return dt;
            }
            catch (MySqlException ex)
            {
                SQLParameters.Clear();
                SQLCommand.Parameters.Clear();
                SQLConnection.Close();
                MySqlDataAdapter da = new MySqlDataAdapter(SQLCommand);
                DataTable dt = new DataTable();
                return dt;

            }
            catch (Exception ex)
            {
                SQLParameters.Clear();
                SQLCommand.Parameters.Clear();
                SQLConnection.Close();
                MySqlDataAdapter da = new MySqlDataAdapter(SQLCommand);
                DataTable dt = new DataTable();
                return dt;
            }

        }

        protected bool ExecuteInsert()
        {
            SQLCommand.CommandText = StoredProcedure;
            if (SQLConnection.State.ToString() == "Closed")
            {
                SQLConnection.Open();
            }
            this.SQLCommand.Parameters.Clear();
            foreach (MySqlParameter item in SQLParameters)
            {
                this.SQLCommand.Parameters.Add(item);
            }
            int result = this.SQLCommand.ExecuteNonQuery();
            SQLCommand.Parameters.Clear();
            SQLConnection.Close();
            return true;


        }

        protected void AddParameter(string name, int value)
        {
            MySqlParameter param = new MySqlParameter(name, MySqlDbType.Int32);
            param.Value = value;
            this.SQLParameters.Add(param);
        }

        protected void AddParameter(string name, string value)
        {
            MySqlParameter param = new MySqlParameter(name, MySqlDbType.VarChar);
            param.Value = value;
            this.SQLParameters.Add(param);
        }

        protected void AddParameter(string name, double value)
        {
            MySqlParameter param = new MySqlParameter(name, MySqlDbType.Float);
            param.Value = value;
            this.SQLParameters.Add(param);
        }

        protected void AddParameter(string name, bool value)
        {
            MySqlParameter param = new MySqlParameter(name, MySqlDbType.Bit);
            param.Value = value?1:0;
            this.SQLParameters.Add(param);
        }

        protected void AddParameter(string name, DateTime value)
        {
            MySqlParameter param = new MySqlParameter(name, MySqlDbType.DateTime);
            param.Value = value;
            this.SQLParameters.Add(param);
        }
    }
}
