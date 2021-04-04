using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.DTOs.Cep;

namespace Api.Domain.Interfaces.Services.Cep
{
  public interface ICepService
  {
    Task<CepDto> Get(Guid Id);
    Task<CepDto> Get(string cep);
    Task<CepDtoCreateResult> Post(CepDtoCreate cep);
    Task<CepDtoUpdateResult> Put(CepDtoUpdate cep);
    Task<bool> Delete(Guid Id);
  }
}
