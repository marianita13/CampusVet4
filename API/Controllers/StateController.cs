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
public class StateController: BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StateController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<StateDto>>> Get()
    {
        var Statees = await _unitOfWork.States.GetAllAsync();
        return _mapper.Map<List<StateDto>>(Statees);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StateDto>> Get(int id)
    {
        var State = await _unitOfWork.States.GetIdAsync(id);
        if(State == null)
        {
            return NotFound();
        }
        return _mapper.Map<StateDto>(State);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StateDto>> Post(StateDto StateDto)
    {
        var State = _mapper.Map<State>(StateDto);
        this._unitOfWork.States.Add(State);
        await _unitOfWork.SaveAsync();
        if(State == null)
        {
            return BadRequest();
        }
        StateDto.Id = State.Id;
        return CreatedAtAction(nameof(Post), new {id = StateDto.Id}, StateDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StateDto>> Put(int id, [FromBody] StateDto StateDto)
    {
        if(StateDto == null)
        {
            return NotFound();
        }
        var Statees = _mapper.Map<State>(StateDto);
        _unitOfWork.States.Update(Statees);
        await _unitOfWork.SaveAsync();
        return StateDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var State = await _unitOfWork.States.GetIdAsync(id);
        if(State == null)
        {
            return NotFound();
        }
        _unitOfWork.States.Remove(State);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}