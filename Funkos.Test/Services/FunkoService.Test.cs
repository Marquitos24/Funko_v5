using FluentAssertions;
using Funkos.Models;
using Funkos.Repositories;
using Funkos.Service;
using Moq;

namespace Funkos.Test.Services;

[TestFixture]
public class FunkoServiceTest
{
    private FunkoService _service;
    private Mock<IFunkoRepository> _funkoRepositoryMock = null;
    private static List<Funko> _funkos = new();
    
    [SetUp]
    public void SetUp()
    {
        _funkoRepositoryMock = new Mock<IFunkoRepository>();
        
        _service = new FunkoService(_funkoRepositoryMock.Object);
        
        _funkos = new List<Funko>()
        {
            new Funko 
            { 
                Id = 1,
                Name = "Cristian",
                Precio = "24",
                Categoria = 0
            },
            new Funko 
            { 
                Id = 2,
                Name = "Paco",
                Precio = "25",
                Categoria = 0
            }
        };
    }
    
    [Test]
    public void Create_Retorna_1Funko()
    {
        // ARRANGE: Configuramos el comportamiento del mock usando los datos del SetUp
        var funkoEsperado = new Funko
        {
            Id = 3,
            Name = "paz",
            Precio = "300",
            Categoria = 0
        };
        
        var newfunko = new Funko
        {
            Id = 3,
            Name = "paz",
            Precio = "300",
            Categoria = 0
        };
        // como el servicio llama a GetAll() internamente para validar, debemos mockearlo 
        // (le pasamos una lista vacía _funkos)
        _funkoRepositoryMock.Setup(r => r.GetAll()).Returns(new List<Funko>());
        
        _funkoRepositoryMock.Setup(r => r.Create(newfunko)).Returns(funkoEsperado);

        // ACT: Ejecutamos el método que queremos probar
        var resultado = _service.Add(newfunko);

        // ASSERT: Comprobamos que el resultado es el esperado
        resultado.IsSuccess.Should().BeTrue();
        resultado.Should().NotBe(null);
        resultado.Value.Should().BeEquivalentTo(funkoEsperado, options => options.ExcludingMissingMembers());
        _funkoRepositoryMock.Verify(repository => repository.Create(newfunko), Times.Once);
    }
    
    [Test]
    public void GetAll_Reotna_Todos_Los_Funkos()
    {
        // ARRANGE: Configuramos el comportamiento del mock usando los datos del SetUp
        _funkoRepositoryMock.Setup(r => r.GetAll()).Returns(_funkos);

        // ACT: Ejecutamos el método que queremos probar
        var resultado = _service.GetAll();

        // ASSERT: Comprobamos que el resultado es el esperado
        resultado.IsSuccess.Should().BeTrue();
        resultado.Should().NotBeNull();
        resultado.Value.Should().NotBeNullOrEmpty();
        resultado.Value.Should().BeEquivalentTo(_funkos, options => options.ExcludingMissingMembers());
        _funkoRepositoryMock.Verify(repository => repository.GetAll(), Times.Once);
    }
    
    [Test]
    public void GetById_Reotna_1Funko()
    {
        // ARRANGE: Configuramos el comportamiento del mock usando los datos del SetUp
        long id = 1;
        var funkoEsperado = new Funko
        {
            Id = 1,
            Name = "Cristian",
            Precio = "24",
            Categoria = 0
        };
        
        _funkoRepositoryMock.Setup(r => r.GetById(id)).Returns(funkoEsperado);

        // ACT: Ejecutamos el método que queremos probar
        var resultado = _service.GetById(id);

        // ASSERT: Comprobamos que el resultado es el esperado
        resultado.IsSuccess.Should().BeTrue();
        resultado.Value.Should().BeEquivalentTo(funkoEsperado, options => options.ExcludingMissingMembers());
        _funkoRepositoryMock.Verify(repository => repository.GetById(id), Times.Once);
    }
    
    [Test]
    public void Update_Retorna_1Funko()
    {
        // ARRANGE: Configuramos el comportamiento del mock usando los datos del SetUp
        long id = 1;
        var funko = new Funko
        {
            Id = 1,
            Name = "Cristian",
            Precio = "24",
            Categoria = 0
        };
        var funkoActualizado = new Funko
        {
            Id = 1,
            Name = "Superman",
            Precio = "200",
            Categoria = 0
        };
        
        _funkoRepositoryMock.Setup(r => r.GetById(id)).Returns(funko);
        _funkoRepositoryMock.Setup(r => r.GetAll()).Returns(_funkos);

        _funkoRepositoryMock.Setup(r => r.Update(id, funkoActualizado)).Returns(funkoActualizado);
        // ACT: Ejecutamos el método que queremos probar
        var resultado = _service.Update(id, funkoActualizado);

        // ASSERT: Comprobamos que el resultado es el esperado
        resultado.IsSuccess.Should().BeTrue();
        resultado.Value.Should().BeEquivalentTo(funkoActualizado, options => options.ExcludingMissingMembers());
        _funkoRepositoryMock.Verify(repository => repository.Update(id, funkoActualizado), Times.Once);
    }
    
    [Test]
    public void Delete_Retorna_Bool()
    {
        // ARRANGE: Configuramos el comportamiento del mock usando los datos del SetUp
        long id = 1;
        var funko = new Funko
        {
            Id = 1,
            Name = "Cristian",
            Precio = "24",
            Categoria = 0
        };
        var deleted = true;
        _funkoRepositoryMock.Setup(r => r.GetById(id)).Returns(funko);

        _funkoRepositoryMock.Setup(r => r.Delete(id)).Returns(deleted);

        // ACT: Ejecutamos el método que queremos probar
        var resultado = _service.Delete(id);

        // ASSERT: Comprobamos que el resultado es el esperado
        resultado.IsSuccess.Should().BeTrue();
        resultado.Value.Should().BeTrue();
        _funkoRepositoryMock.Verify(repository => repository.Delete(id), Times.Once);
    }
}


