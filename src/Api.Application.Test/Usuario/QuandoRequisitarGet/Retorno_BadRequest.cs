using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.User;
using Xunit;
using Moq;
using Api.Domain.DTOs.User;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Test.Usuario.QuandoRequisitarGet
{
  public class Retorno_BadRequest
  {
    private UsersController _controller;

    [Fact(DisplayName = "É possível realizar o Get")]
    public async Task E_Possivel_Invocar_a_Controller_Get()
    {
      var serviceMock = new Mock<IUserService>();
      var nome = Faker.Name.FullName();
      var email = Faker.Internet.Email();

      serviceMock.Setup(c => c.Get(It.IsAny<Guid>())).ReturnsAsync(
          new UserDTO
          {
            Id = Guid.NewGuid(),
            Name = nome,
            Email = email,
            CreateAt = DateTime.UtcNow
          }
        );

      _controller = new UsersController(serviceMock.Object);
      _controller.ModelState.AddModelError("Id", "Formato invalido");

      var result = await _controller.Get(Guid.NewGuid());
      Assert.True(result is BadRequestObjectResult);
    }
  }
}
