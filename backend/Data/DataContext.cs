namespace foodies_app.Data
{
    using foodies_app.Entities;
    using Microsoft.EntityFrameworkCore;
    using MySql.Data.MySqlClient;
    using System.Data;

    public class DataContext : DbContext, IDisposable
    {
        public static class MySqlConnect
        {
            private static MySqlConnection? _Connection;
            public static MySqlConnection Connection
            {
                get
                {
                    if (_Connection == null)
                    {
                        string csb = @"server=127.0.0.1; user id=root; password=''; database=spotify";
                        _Connection = new MySqlConnection(csb);
                    }

                    if (_Connection.State == ConnectionState.Closed)
                        try
                        {
                            _Connection.Open();
                        }
                        catch (Exception)
                        {
                            //handle your exception here
                        }
                    return _Connection;
                }
            }
        }
        public DataContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "food.db");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<AllergyCategory> Allergys { get; set; }
        public string DbPath { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlite($"Data Source={DbPath}");

        public MenuItem GetMenuItem(int id)
        {
            try
            {
                return MenuItems.Where(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
