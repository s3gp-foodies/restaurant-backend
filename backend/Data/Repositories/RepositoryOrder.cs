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
            List Order = new List<OrderItem>();
            using (MySqlConnection con = Helper.MySqlConnect.Connection)
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string query = "SELECT id, productid, quantity FROM OrderItem";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;

                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            int id = sdr["id"].GetHashCode();
                            int productid = sdr["productid"].GetHashCode();
                            int quantity = sdr["quantity"].GetHashCode();
                            OrderItem orderItem = new OrderItem(id, productid, quantity);
                            Order.Add(orderItem);
                        }
                    }

                    con.Close();
                }
            }
            return Order;
        }

        public async Task<IEnumerable<Order>> ConfirmOrder()
        {
            using (MySqlConnection con = Helper.MySqlConnect.Connection)
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Order (id, productid, quantity) VALUES (@id, @productid, @quantity)"))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@productid", productid);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
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
