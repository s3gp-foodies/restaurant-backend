using AutoMapper;
using foodies_app.Data.Repositories;
using foodies_app.Interfaces;
using foodies_app.Interfaces.Repositories;

namespace foodies_app.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UnitOfWork(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ICategoryRepository CategoryRepository => new CategoryRepository(_context, _mapper);
    public IMenuRepository MenuRepository => new MenuRepository(_context, _mapper);
    public IOrderRepository OrderRepository => new OrderRepository(_context, _mapper);
    public ISessionRepository SessionRepository => new SessionRepository(_context, _mapper);

    public async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }
}