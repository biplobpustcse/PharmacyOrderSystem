using AutoMapper;
using PharmacyOrderSystem.Application.DTOs;
using PharmacyOrderSystem.Application.Interfaces;
using PharmacyOrderSystem.Domain.Entities;

namespace PharmacyOrderSystem.Application.Services;

public class CustomerService
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(IRepository<Customer> customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
    }

    public async Task<CustomerDTO?> GetCustomerByIdAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        return customer != null ? _mapper.Map<CustomerDTO>(customer) : null;
    }

    public async Task CreateCustomerAsync(CustomerDTO customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);
        await _customerRepository.AddAsync(customer);
        await _customerRepository.SaveChangesAsync();
    }

    public async Task UpdateCustomerAsync(CustomerDTO customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);
        _customerRepository.Update(customer);
        await _customerRepository.SaveChangesAsync();
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer != null)
        {
            _customerRepository.Remove(customer);
            await _customerRepository.SaveChangesAsync();
        }
    }
}
