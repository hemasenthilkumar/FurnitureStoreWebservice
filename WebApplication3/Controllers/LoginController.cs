using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;

namespace WebApplication3.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        
        public string Get(string username, string password)
        {
           
            string connstring = "Server=localhost; database=testdb; UID=root; password=hema";

            using (var con = new MySqlConnection(connstring))
            {
                string cm = String.Format("Select * from furniture_login where username='{0}' and password='{1}';", username, password);
                MySqlCommand cmd = new MySqlCommand(cm, con);
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return "OK";

                }
                else
                    return "NO";
            }

        }


        public void Getsignup(string us, string ps)
        {

            string connstring = "Server=localhost; database=testdb; UID=root; password=hema";
            using (var con = new MySqlConnection(connstring))
            {
                string cm = String.Format("insert into furniture_login values('{0}','{1}');", us, ps);
                MySqlCommand cmd = new MySqlCommand(cm, con);
                con.Open();
                cmd.ExecuteNonQuery();

            }
        }

        public string GetAllUsers()
        {
            List<login> logins = new List<login>();
            string connstring = "Server=localhost; database=testdb; UID=root; password=hema";
            using (var con = new MySqlConnection(connstring))
            {
                MySqlCommand cmd = new MySqlCommand("Select * from furniture_login", con);
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    login l = new login();
                    l.Username = reader["username"].ToString();
                    l.Password = reader["password"].ToString();
                    logins.Add(l);

                }

            }

            var logs = JsonConvert.SerializeObject(logins);
            return logs;
        }
    }
}