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
public class BreedController: BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BreedController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BreedDto>>> Get()
    {
        var Breedes = await _unitOfWork.Breeds.GetAllAsync();
        return _mapper.Map<List<BreedDto>>(Breedes);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BreedDto>> Get(int id)
    {
        var Breed = await _unitOfWork.Breeds.GetIdAsync(id);
        if(Breed == null)
        {
            return NotFound();
        }
        return _mapper.Map<BreedDto>(Breed);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BreedDto>> Post(BreedDto BreedDto)
    {
        var Breed = _mapper.Map<Breed>(BreedDto);
        this._unitOfWork.Breeds.Add(Breed);
        await _unitOfWork.SaveAsync();
        if(Breed == null)
        {
            return BadRequest();
        }
        BreedDto.Id = Breed.Id;
        return CreatedAtAction(nameof(Post), new {id = BreedDto.Id}, BreedDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BreedDto>> Put(int id, [FromBody] BreedDto BreedDto)
    {
        if(BreedDto == null)
        {
            return NotFound();
        }
        var Breedes = _mapper.Map<Breed>(BreedDto);
        _unitOfWork.Breeds.Update(Breedes);
        await _unitOfWork.SaveAsync();
        return BreedDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var Breed = await _unitOfWork.Breeds.GetIdAsync(id);
        if(Breed == null)
        {
            return NotFound();
        }
        _unitOfWork.Breeds.Remove(Breed);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}