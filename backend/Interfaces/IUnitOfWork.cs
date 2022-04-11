using foodies_app.Interfaces.Repositories;

namespace API.Interfaces;

public interface IUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }
    IMenuRepository MenuRepository { get; }
    IOrderRepository OrderRepository { get; }
    ISessionRepository SessionRepository { get; }

    Task<bool> Complete();
    bool HasChanges();
}