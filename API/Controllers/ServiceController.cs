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
public class ServiceController: BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ServiceController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> Get()
    {
        var Servicees = await _unitOfWork.Services.GetAllAsync();
        return _mapper.Map<List<ServiceDto>>(Servicees);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceDto>> Get(int id)
    {
        var Service = await _unitOfWork.Services.GetIdAsync(id);
        if(Service == null)
        {
            return NotFound();
        }
        return _mapper.Map<ServiceDto>(Service);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceDto>> Post(ServiceDto ServiceDto)
    {
        var Service = _mapper.Map<Service>(ServiceDto);
        this._unitOfWork.Services.Add(Service);
        await _unitOfWork.SaveAsync();
        if(Service == null)
        {
            return BadRequest();
        }
        ServiceDto.Id = Service.Id;
        return CreatedAtAction(nameof(Post), new {id = ServiceDto.Id}, ServiceDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceDto>> Put(int id, [FromBody] ServiceDto ServiceDto)
    {
        if(ServiceDto == null)
        {
            return NotFound();
        }
        var Servicees = _mapper.Map<Service>(ServiceDto);
        _unitOfWork.Services.Update(Servicees);
        await _unitOfWork.SaveAsync();
        return ServiceDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var Service = await _unitOfWork.Services.GetIdAsync(id);
        if(Service == null)
        {
            return NotFound();
        }
        _unitOfWork.Services.Remove(Service);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}