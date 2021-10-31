using PhiKapStudyHours;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhiKapStudyHours
{
    public class IStudyData
    {
        public Boolean check_login(string username, string password)
        {
            using (var conn = new SqlConnection(Config.CONN)) //changes ip here
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from Study_Login where username=@username", conn))
                {
                    cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar).Value = username;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var password_2 = (string)reader["password"];
                            if (password == password_2)
                            {
                                return true;
                            }
                        }
                    }
                }
            conn.Close();
            }
            return false;
        }

        public string get_role(string username)
        {
            using (var conn = new SqlConnection(Config.CONN)) //changes ip here
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from Study_Login where username=@username", conn))
                {
                    cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar).Value = username;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var role = (string)reader["role"];
                            return role;
                        }
                    }
                }
                conn.Close();
            }
            return "na";
        }

        public List<Entry> get_entries_by_user(string username)
        {
            List<Entry> results = new List<Entry>();
            using (var conn = new SqlConnection(Config.CONN)) //changes ip here
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from Entries where Proctor=@proctor", conn))
                {
                    cmd.Parameters.Add("@proctor", System.Data.SqlDbType.VarChar).Value = username.ToLower().Trim();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new Entry((string)reader["Name"], (string)reader["Proctor"], (double)reader["Hours"],(DateTime)reader["Date"]));
                        }
                    }
                }
                conn.Close();
            }
            return results;
        }

        public List<string> get_students()
        {
            List<string> results = new List<string>();
            using (var conn = new SqlConnection(Config.CONN)) //changes ip here
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from Entries", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if(!results.Contains((string)reader["Name"]))
                            {
                                results.Add((string)reader["Name"]);
                            }
                        }
                    }
                }
                conn.Close();
            }
            return results;
        }

        public void create_entry(Entry entry)
        {
            using (var conn = new SqlConnection(Config.CONN)) //changes ip here
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("insert into Entries (Name, Proctor, Hours, Date) values (@name, @proctor, @hours, @date)", conn))
                {
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = entry.Name;
                    cmd.Parameters.Add("@proctor", System.Data.SqlDbType.VarChar).Value = entry.Proctor;
                    cmd.Parameters.Add("@hours", System.Data.SqlDbType.Float).Value = entry.Hours;
                    cmd.Parameters.Add("@date", System.Data.SqlDbType.DateTime).Value = entry.Date;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
