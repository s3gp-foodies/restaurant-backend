using foodies_app.Entities;


namespace foodies_app.Interfaces;


    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetTables();
        Task<Table> GetTable(int id);
        void Add(Table table);
        void Delete(Table table);
        void Edit(Table table);
      

    }