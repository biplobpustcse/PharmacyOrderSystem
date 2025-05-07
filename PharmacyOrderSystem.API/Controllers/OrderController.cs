using Microsoft.AspNetCore.Mvc;
using PharmacyOrderSystem.Application.DTOs;
using PharmacyOrderSystem.Application.Services;

namespace PharmacyOrderSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    // GET: api/order
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    // GET: api/order/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDTO>> GetOrder(int id)
    {
        var order = await _orderService.GetOrderWithItemsAsync(id);
        if (order == null)
            return NotFound();

        return Ok(order);
    }

    // POST: api/order
    [HttpPost]
    public async Task<ActionResult> CreateOrder(OrderDTO orderDto)
    {
        await _orderService.CreateOrderAsync(orderDto);
        return CreatedAtAction(nameof(GetOrder), new { id = orderDto.Id }, orderDto);
    }

    // PUT: api/order/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrder(int id, OrderDTO orderDto)
    {
        if (id != orderDto.Id)
            return BadRequest();

        await _orderService.CreateOrderAsync(orderDto); // Modify according to actual update logic if needed
        return NoContent();
    }

    // DELETE: api/order/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        await _orderService.DeleteOrderAsync(id); 
        return NoContent();
    }
}
