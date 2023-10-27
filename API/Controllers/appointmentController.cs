using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class AppointmentController: BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AppointmentController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<AppointmentDto>>> Get()
    {
        var Appointmentes = await _unitOfWork.Appointments.GetAllAsync();
        return _mapper.Map<List<AppointmentDto>>(Appointmentes);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AppointmentDto>> Get(int id)
    {
        var Appointment = await _unitOfWork.Appointments.GetIdAsync(id);
        if(Appointment == null)
        {
            return NotFound();
        }
        return _mapper.Map<AppointmentDto>(Appointment);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AppointmentDto>> Post(AppointmentDto AppointmentDto)
    {
        var Appointment = _mapper.Map<Appointment>(AppointmentDto);
        this._unitOfWork.Appointments.Add(Appointment);
        await _unitOfWork.SaveAsync();
        if(Appointment == null)
        {
            return BadRequest();
        }
        AppointmentDto.Id = Appointment.Id;
        return CreatedAtAction(nameof(Post), new {id = AppointmentDto.Id}, AppointmentDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AppointmentDto>> Put(int id, [FromBody] AppointmentDto AppointmentDto)
    {
        if(AppointmentDto == null)
        {
            return NotFound();
        }
        var Appointmentes = _mapper.Map<Appointment>(AppointmentDto);
        _unitOfWork.Appointments.Update(Appointmentes);
        await _unitOfWork.SaveAsync();
        return AppointmentDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var Appointment = await _unitOfWork.Appointments.GetIdAsync(id);
        if(Appointment == null)
        {
            return NotFound();
        }
        _unitOfWork.Appointments.Remove(Appointment);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}