using foodies_app.Entities;


namespace foodies_app.Interfaces;


    public interface IRepositoryUsers
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUsers(int id);
        void Add(User user);
        void Delete(User user);
        void Edit(User user);
      

    }