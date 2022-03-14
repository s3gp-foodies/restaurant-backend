namespace foodies_app.Data
{
    using foodies_app.Entities;
    using Microsoft.EntityFrameworkCore;
    public class DataContext : DbContext, IDisposable
    {
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
