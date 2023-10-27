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
public class PetController: BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PetController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PetDto>>> Get()
    {
        var Petes = await _unitOfWork.Pets.GetAllAsync();
        return _mapper.Map<List<PetDto>>(Petes);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetDto>> Get(int id)
    {
        var Pet = await _unitOfWork.Pets.GetIdAsync(id);
        if(Pet == null)
        {
            return NotFound();
        }
        return _mapper.Map<PetDto>(Pet);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PetDto>> Post(PetDto PetDto)
    {
        var Pet = _mapper.Map<Pet>(PetDto);
        _unitOfWork.Pets.Add(Pet);
        await _unitOfWork.SaveAsync();
        if(Pet == null)
        {
            return BadRequest();
        }
        PetDto.Id = Pet.Id;
        return CreatedAtAction(nameof(Post), new {id = PetDto.Id}, PetDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetDto>> Put(int id, [FromBody] PetDto PetDto)
    {
        if(PetDto == null)
        {
            return NotFound();
        }
        var Petes = _mapper.Map<Pet>(PetDto);
        _unitOfWork.Pets.Update(Petes);
        await _unitOfWork.SaveAsync();
        return PetDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var Pet = await _unitOfWork.Pets.GetIdAsync(id);
        if(Pet == null)
        {
            return NotFound();
        }
        _unitOfWork.Pets.Remove(Pet);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}