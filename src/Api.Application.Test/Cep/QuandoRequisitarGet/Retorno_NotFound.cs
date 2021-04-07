using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.Cep;
using Xunit;
using Moq;
using Api.Domain.DTOs.Cep;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Test.Cep.QuandoRequisitarGet
{
  public class Retorno_NotFound
  {
    private CepsController _controller;

    [Fact(DisplayName = "É possível realizar o Get")]
    public async Task E_Possivel_Invocar_a_Controller_Get()
    {
      var serviceMock = new Mock<ICepService>();

      serviceMock.Setup(c => c.Get(It.IsAny<Guid>())).Returns(Task.FromResult((CepDto)null));

      _controller = new CepsController(serviceMock.Object);

      var result = await _controller.Get(Guid.NewGuid());
      Assert.True(result is NotFoundResult);
    }
  }
}
