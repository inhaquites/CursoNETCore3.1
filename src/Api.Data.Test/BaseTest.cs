using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
  public abstract class BaseTest
  {
    public BaseTest()
    {

    }
  }

  public class DbTeste : IDisposable
  {
    private string dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";

    public ServiceProvider ServiceProvider { get; private set; }

    public DbTeste()
    {
      var serviceCollection = new ServiceCollection();
      serviceCollection.AddDbContext<MyContext>(c =>
        c.UseSqlServer($"Server=.\\SQLEXPRESS;Initial Catalog={dataBaseName};Integrated Security=True"),
        ServiceLifetime.Transient
      );

      ServiceProvider = serviceCollection.BuildServiceProvider();
      using (var Context = ServiceProvider.GetService<MyContext>())
      {
        Context.Database.EnsureCreated();
      }

    }

    public void Dispose()
    {
      using (var Context = ServiceProvider.GetService<MyContext>())
      {
        Context.Database.EnsureDeleted();
      }
    }
  }
}
