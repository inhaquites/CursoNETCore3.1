using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.Cep;
using Xunit;
using Moq;
using Api.Domain.DTOs.Cep;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Test.Cep.QuandoRequisitarGetByCep
{
  public class Retorno_Get
  {
    private CepsController _controller;

    [Fact(DisplayName = "É possível realizar o Get por Cep")]
    public async Task E_Possivel_Invocar_a_Controller_Get_por_Cep()
    {
      var serviceMock = new Mock<ICepService>();
      var Cep = Faker.RandomNumber.Next(1, 10000).ToString();
      var Logradouro = Faker.Address.StreetName();
      var Numero = Faker.RandomNumber.Next(1, 10000).ToString();
      var MunicipioId = Guid.NewGuid();

      serviceMock.Setup(c => c.Get(It.IsAny<string>())).ReturnsAsync(
          new CepDto
          {
            Id = Guid.NewGuid(),
            Cep = Cep,
            Logradouro = Logradouro,
            Numero = Numero,
            MunicipioId = MunicipioId
          }
        );

      _controller = new CepsController(serviceMock.Object);

      var result = await _controller.Get("999");
      Assert.True(result is OkObjectResult);
    }
  }
}
