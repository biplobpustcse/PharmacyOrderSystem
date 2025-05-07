using Microsoft.EntityFrameworkCore;
using PharmacyOrderSystem.Application.Interfaces;
using PharmacyOrderSystem.Domain.Entities;
using PharmacyOrderSystem.Infrastructure.Persistence;

namespace PharmacyOrderSystem.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context) { }

    public async Task<Order?> GetWithItemsAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Medicine)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}
