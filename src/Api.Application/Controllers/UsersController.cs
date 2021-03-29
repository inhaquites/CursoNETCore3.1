using Api.Domain.DTOs.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Application.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
      _service = service;
    }

    [Authorize("Bearer")]
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState); //400 bad request - solicitacao inválida
      }
      try
      {
        return Ok(await _service.GetAll());
      }
      catch (ArgumentException e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);

      }
    }

    [Authorize("Bearer")]
    [HttpGet]
    [Route("{id}", Name = "GetWithId")]
    public async Task<ActionResult> Get(Guid id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState); //400 bad request - solicitacao inválida
      }
      try
      {
        var result = await _service.Get(id);
        if (result == null)
          return NotFound();
        return Ok(result);
      }
      catch (ArgumentException e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    [Authorize("Bearer")]
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] UserDTOCreate user)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState); //400 bad request - solicitacao inválida
      }
      try
      {
        var result = await _service.Post(user);

        if (result != null)
        {
          return Created(new Uri(Url.Link("GetWithId", new { id = result.Id })), result);
        }

        return BadRequest();
      }
      catch (ArgumentException e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    [Authorize("Bearer")]
    [HttpPut]
    public async Task<ActionResult> Put([FromBody] UserDTOUpdate user)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState); //400 bad request - solicitacao inválida
      }

      try
      {
        var result = await _service.Put(user);

        if (result != null)
        {
          return Ok(result);
        }

        return BadRequest();
      }
      catch (ArgumentException e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }


    [Authorize("Bearer")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState); //400 bad request - solicitacao inválida
      }
      try
      {
        return Ok(await _service.Delete(id));
      }
      catch (ArgumentException e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

  }
}
