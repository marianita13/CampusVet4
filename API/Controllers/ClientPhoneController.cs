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
public class ClientPhoneController: BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClientPhoneController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ClientPhoneDto>>> Get()
    {
        var ClientPhonees = await _unitOfWork.ClientPhones.GetAllAsync();
        return _mapper.Map<List<ClientPhoneDto>>(ClientPhonees);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientPhoneDto>> Get(int id)
    {
        var ClientPhone = await _unitOfWork.ClientPhones.GetIdAsync(id);
        if(ClientPhone == null)
        {
            return NotFound();
        }
        return _mapper.Map<ClientPhoneDto>(ClientPhone);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClientPhoneDto>> Post(ClientPhoneDto ClientPhoneDto)
    {
        var ClientPhone = _mapper.Map<ClientPhone>(ClientPhoneDto);
        this._unitOfWork.ClientPhones.Add(ClientPhone);
        await _unitOfWork.SaveAsync();
        if(ClientPhone == null)
        {
            return BadRequest();
        }
        ClientPhoneDto.Id = ClientPhone.Id;
        return CreatedAtAction(nameof(Post), new {id = ClientPhoneDto.Id}, ClientPhoneDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientPhoneDto>> Put(int id, [FromBody] ClientPhoneDto ClientPhoneDto)
    {
        if(ClientPhoneDto == null)
        {
            return NotFound();
        }
        var ClientPhonees = _mapper.Map<ClientPhone>(ClientPhoneDto);
        _unitOfWork.ClientPhones.Update(ClientPhonees);
        await _unitOfWork.SaveAsync();
        return ClientPhoneDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var ClientPhone = await _unitOfWork.ClientPhones.GetIdAsync(id);
        if(ClientPhone == null)
        {
            return NotFound();
        }
        _unitOfWork.ClientPhones.Remove(ClientPhone);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}