using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApplication3.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DiningController : ApiController
    {
        public string GetTables()
        {
            List<Products> tables = new List<Products>();
            string connstring = "Server=localhost; database=testdb; UID=root; password=hema";
            using (var con = new MySqlConnection(connstring))
            {
                MySqlCommand cmd = new MySqlCommand("Select * from product_dining", con);
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Products b = new Products();
                    b.bid = reader["did"].ToString();
                    b.bname = reader["dname"].ToString();
                    b.description = reader["description"].ToString();
                    b.price = Int32.Parse(reader["price"].ToString());
                    b.rating = Single.Parse(reader["rating"].ToString());
                    tables.Add(b);

                }

            }

            var table = JsonConvert.SerializeObject(tables);
            return table;

        }
    }
}