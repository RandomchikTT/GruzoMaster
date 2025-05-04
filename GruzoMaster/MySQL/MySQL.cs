using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GruzoMaster
{
    public static class MySQL
    {
        public const String Connection = "Host=localhost;User=root;Password=;Database=gruzomaster;SslMode=None";
        public static async void AddUserLog(String login, String action)
        {
            try
            {
                await MySQL.QueryAsync($"INSERT INTO `userlogs` (`login`,`time`,`action`) " +
                    $"VALUES ('{login}','{DateTime.Now.ToString("G")}','{action}')");
            }
            catch (Exception e) { MessageBox.Show("AddUserLog: " + e.ToString()); }
        }   
        public static async Task<DataTable> QueryRead(String cmd)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Connection))
                {
                    MySqlCommand command = new MySqlCommand(cmd);
                    await connection.OpenAsync();
                    command.Connection = connection;
                    DbDataReader reader = await command.ExecuteReaderAsync();
                    DataTable result = new DataTable();
                    result.Load(reader);
                    await connection.CloseAsync();
                    return result;
                }
            }
            catch (Exception e) { MessageBox.Show("QueryRead: " + e.ToString() + "\nCMD: " + cmd); return null; }
        }
        public static async Task QueryAsync(String cmd)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Connection))
                {
                    MySqlCommand command = new MySqlCommand(cmd);
                    await connection.OpenAsync();
                    command.Connection = connection;
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                }
            }
            catch (Exception e) { MessageBox.Show("QueryAsync: " + e.ToString() + "\nCMD: " + cmd); }
        }
        public static async Task<Int64> QueryLastInsertAsync(String cmd)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Connection))
                {
                    MySqlCommand command = new MySqlCommand(cmd);
                    await connection.OpenAsync();
                    command.Connection = connection;
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                    return command.LastInsertedId;
                }
            }
            catch (Exception e) { MessageBox.Show("QueryLastInsertAsync: " + e.ToString()); return -1; }
        }
        public static async Task<Int32> QueryCountRowsAsync(String cmd)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(Connection))
                {
                    MySqlCommand command = new MySqlCommand(cmd);
                    await connection.OpenAsync();
                    command.Connection = connection;
                    Int32 rowCount = Convert.ToInt32(command.ExecuteScalar());
                    await connection.CloseAsync();
                    return rowCount;
                }
            }
            catch (Exception e) { MessageBox.Show("QueryCountRowsAsync: " + e.ToString()); return -1; }
        }
    }
}
