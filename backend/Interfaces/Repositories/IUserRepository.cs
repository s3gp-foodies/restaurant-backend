using foodies_app.Entities;


namespace foodies_app.Interfaces;


    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetUsers();
        Task<AppUser> GetUser(int id);
        void Add(AppUser user);
        void Delete(AppUser user);
        void Edit(AppUser user);
      

    }