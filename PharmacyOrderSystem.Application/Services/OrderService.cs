using AutoMapper;
using PharmacyOrderSystem.Application.DTOs;
using PharmacyOrderSystem.Application.Interfaces;
using PharmacyOrderSystem.Domain.Entities;

namespace PharmacyOrderSystem.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderDTO?> GetOrderWithItemsAsync(int id)
    {
        var order = await _orderRepository.GetWithItemsAsync(id);
        return order != null ? _mapper.Map<OrderDTO>(order) : null;
    }

    public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderDTO>>(orders);
    }

    public async Task CreateOrderAsync(OrderDTO orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);
        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveChangesAsync();
    }
    public async Task DeleteOrderAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order != null)
        {
            _orderRepository.Remove(order);
            await _orderRepository.SaveChangesAsync();
        }
    }
}
