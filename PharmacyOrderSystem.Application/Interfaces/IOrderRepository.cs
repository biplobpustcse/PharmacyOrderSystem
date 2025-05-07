using PharmacyOrderSystem.Domain.Entities;

namespace PharmacyOrderSystem.Application.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order?> GetWithItemsAsync(int id);
}
