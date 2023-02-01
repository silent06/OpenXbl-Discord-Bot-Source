using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
//using MySqlConnector;
namespace stealthbot
{
    class mysql
    {
    
        public static MySqlConnection Setup()
        {
            return new MySqlConnection(String.Format("Server={0};Port=3306;Database={1};Uid={2};Password={3};", Global.host, Global.Database, Global.Username, Global.password));
        }

        public static bool Connect(MySqlConnection connection)
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }

        public static void Disconnect(MySqlConnection connection)
        {
            try
            {
                connection.Close();
            }
            catch (MySqlException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }



        public static void SaveUserData(string discord, string discordid, string cpukey)
        {
            try
            {
                using (var db = Setup())
                {
                    Connect(db);
                    using (var command = db.CreateCommand())
                    {
                        command.CommandText = string.Format("UPDATE `users` SET Discord=@DISCORD, Discordid=@DISCORDID WHERE cpu=@CPU");
                        command.Parameters.AddWithValue("@CPU", cpukey);
                        command.Parameters.AddWithValue("@DISCORD", discord);
                        command.Parameters.AddWithValue("@DISCORDID", discordid);
                        command.ExecuteNonQuery();
                    }
                    Disconnect(db);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public static void UpdateEmail(string email, string cpukey)
        {
            using (var db = Setup())
            {
                Connect(db);
                using (var command = db.CreateCommand())
                {
                    command.CommandText = string.Format("UPDATE `users` SET Email=@EMAIL WHERE cpu=@CPUKEY");
                    command.Parameters.AddWithValue("@CPUKEY", cpukey);
                    command.Parameters.AddWithValue("@EMAIL", email);
                    command.ExecuteNonQuery();
                }
                Disconnect(db);
            }
        }


        public static bool GetFriendsCPUKey(string Discord, ref ClientInfo data)
        {
            using (var db = Setup())
            {
                Connect(db);
                using (var command = db.CreateCommand())
                {
                    command.CommandText = string.Format("SELECT * FROM users WHERE Discord = @Discord");
                    command.Parameters.AddWithValue("@Discord", Discord);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.CPUKey = reader.GetString("cpu");
                            data.Discord = reader.GetString("Discord");
                            data.Discordid = reader.GetString("Discordid");
                            data.Username = reader.GetString("Username");
                            data.email = reader.GetString("Email");
                            Disconnect(db);
                            return true;
                        }
                    }
                }
                Disconnect(db);
            }
            return false;
        }


        public static bool GetClientData(string cpukey, ref ClientInfo data)
        {
            using (var db = Setup())
            {
                Connect(db);
                using (var command = db.CreateCommand())
                {
                    command.CommandText = string.Format("SELECT * FROM users WHERE cpu = @cpu");
                    command.Parameters.AddWithValue("@cpu", cpukey);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.CPUKey = reader.GetString("cpu");
                            data.Discord = reader.GetString("Discord");
                            data.Discordid = reader.GetString("Discordid");
                            data.Username = reader.GetString("Username");
                            data.email = reader.GetString("Email");
                            Disconnect(db);
                            return true;
                        }
                    }
                }
                Disconnect(db);
            }
            return false;
        }


        public static void AddCPUKEY(string CPUKEY)
        {
            using (var db = Setup())
            {
                Connect(db);
                using (var command = db.CreateCommand())
                {
                    command.CommandText = string.Format("INSERT INTO users (cpu) VALUES (@cpu)");
                    command.Parameters.AddWithValue("@cpu", CPUKEY);
                    command.ExecuteNonQuery();
                }
                Disconnect(db);
            }
        }

        public static void DeleteXBLKey(string CPUKEY, string XblKey)
        {
            using (var db = Setup())
            {
                Connect(db);
                using (var command = db.CreateCommand())
                {
                    command.CommandText = string.Format("DELETE FROM OpenXbl WHERE `CPUKEY` = @cpu_key");
                    command.Parameters.AddWithValue("@cpu_key", CPUKEY);
                    command.ExecuteNonQuery();
                }
                Disconnect(db);
            }
        }

        public static void ChangeXBLKey(string CPUKEY, string XblKey)
        {
            try
            {
                using (var db = Setup())
                {
                    Connect(db);
                    using (var command = db.CreateCommand())
                    {
                        command.CommandText = string.Format("UPDATE `OpenXbl` SET APIKEY=@XblKey WHERE CPUKEY=@CPU");
                        command.Parameters.AddWithValue("@CPU", CPUKEY);
                        command.Parameters.AddWithValue("@XblKey", XblKey);
                        command.ExecuteNonQuery();
                    }
                    Disconnect(db);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void AddXBLkey(string CPUKEY, ref OpenXBL getxbldata)
        {
            using (var db = Setup())
            {
                Connect(db);
                using (var command = db.CreateCommand())
                {
                    command.CommandText = string.Format("INSERT INTO OpenXbl (CPUKEY, APIKEY) VALUES (@CPUKEY, @API_KEY)");
                    command.Parameters.AddWithValue("@CPUKEY", CPUKEY);
                    command.Parameters.AddWithValue("@API_KEY", getxbldata.APIKEY); 
                    command.ExecuteNonQuery();
                }
                Disconnect(db);
            }
        }

        public static bool GetXBLkey(string CPUKEY, ref OpenXBL getxbldata)
        {
            using (var db = Setup())
            {
                Connect(db);
                using (var command = db.CreateCommand())
                {         
                    
                    command.CommandText = string.Format("SELECT * FROM OpenXbl WHERE `CPUKEY` = @CPUKEY AND `APIKEY` = @API_KEY");
                    command.Parameters.AddWithValue("@CPUKEY", CPUKEY);
                    command.Parameters.AddWithValue("@API_KEY", getxbldata.APIKEY);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            CPUKEY = reader.GetString("CPUKEY");
                            getxbldata.APIKEY = reader.GetString("APIKEY ");
                            Disconnect(db);
                            return true;
                        }
                    }
                }
                Disconnect(db);
            }
            return false;
        }
       
    }
}