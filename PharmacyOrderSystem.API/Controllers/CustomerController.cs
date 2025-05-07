using Microsoft.AspNetCore.Mvc;
using PharmacyOrderSystem.Application.DTOs;
using PharmacyOrderSystem.Application.Services;

namespace PharmacyOrderSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDTO>> GetCustomer(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null)
            return NotFound();

        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCustomer(CustomerDTO customerDto)
    {
        await _customerService.CreateCustomerAsync(customerDto);
        return CreatedAtAction(nameof(GetCustomer), new { id = customerDto.Id }, customerDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCustomer(int id, CustomerDTO customerDto)
    {
        if (id != customerDto.Id)
            return BadRequest();

        await _customerService.UpdateCustomerAsync(customerDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer(int id)
    {
        await _customerService.DeleteCustomerAsync(id);
        return NoContent();
    }
}
