using Microsoft.AspNetCore.Mvc;
using PharmacyOrderSystem.Application.DTOs;
using PharmacyOrderSystem.Application.Services;

namespace PharmacyOrderSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicineController : ControllerBase
{
    private readonly MedicineService _medicineService;

    public MedicineController(MedicineService medicineService)
    {
        _medicineService = medicineService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MedicineDTO>>> GetMedicines()
    {
        var medicines = await _medicineService.GetAllMedicinesAsync();
        return Ok(medicines);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MedicineDTO>> GetMedicine(int id)
    {
        var medicine = await _medicineService.GetMedicineByIdAsync(id);
        if (medicine == null)
            return NotFound();

        return Ok(medicine);
    }

    [HttpPost]
    public async Task<ActionResult> CreateMedicine(MedicineDTO medicineDto)
    {
        await _medicineService.CreateMedicineAsync(medicineDto);
        return CreatedAtAction(nameof(GetMedicine), new { id = medicineDto.Id }, medicineDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMedicine(int id, MedicineDTO medicineDto)
    {
        if (id != medicineDto.Id)
            return BadRequest();

        await _medicineService.UpdateMedicineAsync(medicineDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMedicine(int id)
    {
        await _medicineService.DeleteMedicineAsync(id);
        return NoContent();
    }
}
