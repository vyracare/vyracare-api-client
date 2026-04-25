using Microsoft.AspNetCore.Mvc;
using Vyracare.Api.Client.DTOS;
using Vyracare.Api.Client.Models;
using Vyracare.Api.Client.Services;

namespace Vyracare.Api.Client.Controllers;

[ApiController]
[Route("api/client/patients")]
public class PatientsController : ControllerBase
{
    private readonly PatientService _service;

    public PatientsController(PatientService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("cpf/{cpf}")]
    public async Task<IActionResult> GetByCpf(string cpf)
    {
        var item = await _service.GetByCpfAsync(cpf);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientRequest request)
    {
        if (await _service.ExistsByCpfAsync(request.Cpf))
        {
            return Conflict(new { message = "Ja existe um paciente cadastrado com este CPF." });
        }

        var item = new PatientModel
        {
            FullName = request.FullName,
            BirthDate = request.BirthDate,
            Gender = request.Gender,
            Cpf = request.Cpf,
            Rg = request.Rg,
            Email = request.Email,
            Phone = request.Phone,
            Whatsapp = request.Whatsapp,
            AddressStreet = request.AddressStreet,
            AddressNumber = request.AddressNumber,
            AddressComplement = request.AddressComplement,
            AddressNeighborhood = request.AddressNeighborhood,
            AddressCity = request.AddressCity,
            AddressState = request.AddressState,
            AddressZip = request.AddressZip,
            EmergencyContactName = request.EmergencyContactName,
            EmergencyContactPhone = request.EmergencyContactPhone,
            MainComplaint = request.MainComplaint,
            Objectives = request.Objectives,
            MedicalConditions = request.MedicalConditions,
            Allergies = request.Allergies,
            Medications = request.Medications,
            PreviousSurgeries = request.PreviousSurgeries,
            AestheticProcedures = request.AestheticProcedures,
            SkinType = request.SkinType,
            SunExposure = request.SunExposure,
            Smoking = request.Smoking,
            Alcohol = request.Alcohol,
            PregnantOrBreastfeeding = request.PregnantOrBreastfeeding,
            Consent = request.Consent,
            Notes = request.Notes
        };

        await _service.CreateAsync(item);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }
}
