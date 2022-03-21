using foodies_app.Entities.OrderItem;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MySql.Data.MySqlClient;

namespace foodies_app.Data.Repositories
{
    public class RepositoryOrder
    {

        public async Task<IEnumerable<Order>> GetOrder()
        {
            List OrderItems = new List<OrderItem>();
            using (MySqlConnection con = Helper.MySqlConnect.Connection)
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string query = "SELECT id, quantity, total FROM OrderItem";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;

                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            int id = sdr["id"].GetHashCode();
                            int quantity = sdr["quantity"].GetHashCode();
                            decimal total = sdr["total"].GetHashCode();
                            OrderItem orderItem = new OrderItem(id, quantity, total);
                            OrderItems.Add(orderItem);
                        }
                    }

                    con.Close();
                }
            }
            return OrderItems;
        }

        public async Task<IEnumerable<Order>> AddOrder(int id, int itemid, int quantity, decimal total)
        {
            using (MySqlConnection con = Helper.MySqlConnect.Connection)
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Order (id, itemid, quantity, total) VALUES (@id, @itemid, @quantity, @total)"))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@itemid", itemid);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.Parameters.AddWithValue("@total", total);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return;
            }
        }
       
    }
}
