using AutoMapper;
using PharmacyOrderSystem.Application.DTOs;
using PharmacyOrderSystem.Application.Interfaces;
using PharmacyOrderSystem.Domain.Entities;

namespace PharmacyOrderSystem.Application.Services;

public class MedicineService
{
    private readonly IRepository<Medicine> _medicineRepository;
    private readonly IMapper _mapper;

    public MedicineService(IRepository<Medicine> medicineRepository, IMapper mapper)
    {
        _medicineRepository = medicineRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MedicineDTO>> GetAllMedicinesAsync()
    {
        var medicines = await _medicineRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<MedicineDTO>>(medicines);
    }

    public async Task<MedicineDTO?> GetMedicineByIdAsync(int id)
    {
        var medicine = await _medicineRepository.GetByIdAsync(id);
        return medicine != null ? _mapper.Map<MedicineDTO>(medicine) : null;
    }

    public async Task CreateMedicineAsync(MedicineDTO medicineDto)
    {
        var medicine = _mapper.Map<Medicine>(medicineDto);
        await _medicineRepository.AddAsync(medicine);
        await _medicineRepository.SaveChangesAsync();
    }

    public async Task UpdateMedicineAsync(MedicineDTO medicineDto)
    {
        var medicine = _mapper.Map<Medicine>(medicineDto);
        _medicineRepository.Update(medicine);
        await _medicineRepository.SaveChangesAsync();
    }

    public async Task DeleteMedicineAsync(int id)
    {
        var medicine = await _medicineRepository.GetByIdAsync(id);
        if (medicine != null)
        {
            _medicineRepository.Remove(medicine);
            await _medicineRepository.SaveChangesAsync();
        }
    }
}
