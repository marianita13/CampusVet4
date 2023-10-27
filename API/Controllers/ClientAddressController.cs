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
public class ClientAddressController: BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClientAddressController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ClientAddressDto>>> Get()
    {
        var ClientAddresses = await _unitOfWork.ClientAddresses.GetAllAsync();
        return _mapper.Map<List<ClientAddressDto>>(ClientAddresses);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientAddressDto>> Get(int id)
    {
        var ClientAddress = await _unitOfWork.ClientAddresses.GetIdAsync(id);
        if(ClientAddress == null)
        {
            return NotFound();
        }
        return _mapper.Map<ClientAddressDto>(ClientAddress);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClientAddressDto>> Post(ClientAddressDto ClientAddressDto)
    {
        var ClientAddress = _mapper.Map<ClientAddress>(ClientAddressDto);
        this._unitOfWork.ClientAddresses.Add(ClientAddress);
        await _unitOfWork.SaveAsync();
        if(ClientAddress == null)
        {
            return BadRequest();
        }
        ClientAddressDto.Id = ClientAddress.Id;
        return CreatedAtAction(nameof(Post), new {id = ClientAddressDto.Id}, ClientAddressDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientAddressDto>> Put(int id, [FromBody] ClientAddressDto ClientAddressDto)
    {
        if(ClientAddressDto == null)
        {
            return NotFound();
        }
        var ClientAddresses = _mapper.Map<ClientAddress>(ClientAddressDto);
        _unitOfWork.ClientAddresses.Update(ClientAddresses);
        await _unitOfWork.SaveAsync();
        return ClientAddressDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var ClientAddress = await _unitOfWork.ClientAddresses.GetIdAsync(id);
        if(ClientAddress == null)
        {
            return NotFound();
        }
        _unitOfWork.ClientAddresses.Remove(ClientAddress);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}