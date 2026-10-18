using Funkos.Mappers;
using Funkos.Models;
using Funkos.Dto;
using NUnit.Framework;
using FluentAssertions;

namespace Funkos.Test.Mappers;

[TestFixture]
public class FunkoMapperTest
{
    private FunkosDtoRequest _funkoDTORequest;
    private Funko funko;
    
    [SetUp]
    public void SetUp()
    {
        _funkoDTORequest = new FunkosDtoRequest()
        {
            Name = "Cristian",
            Precio = "24",
            Categoria = 0
        };
        
        funko = new Funko()
        {
            Id = 1,
            Name = "Cristian",
            Precio = "24",
            Categoria = 0
        };
    }
    
    [Test]
    public void ToEntity_Ok()
    {
        
        // Act (ejecutar)
        var funk = FunkoMapper.ToEntity(_funkoDTORequest);
    
        // Assert (Validar con FluentAssertions --> Mas comprensible)
        funk.Should().NotBeNull();  // Comprobamos que usuario no sea nulo
        // Comprobamos que ningun valor llegue como nulo, vacio o cambiado
        funk.Name.Should().Be(_funkoDTORequest.Name);
        funk.Precio.Should().Be(_funkoDTORequest.Precio);
        funk.Categoria.Should().Be(_funkoDTORequest.Categoria);
    }
    
    [Test]
    public void ToDTOResponse_Ok()
    {
        // Act (ejecutar)
        var funk = FunkoMapper.ToDtoResponse(funko);
        
        funk.Should().NotBeNull();  
        funk.Id.Should().Be(funko.Id); 
        funk.Name.Should().Be(funko.Name);
        funk.Precio.Should().Be(funko.Precio);
        funk.Categoria.Should().Be(funko.Categoria);
    }
}