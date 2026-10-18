using CSharpFunctionalExtensions;
using Funkos.Errors;
using Funkos.Models;
using Funkos.Repositories;

namespace Funkos.Service;

public class FunkoService(IFunkoRepository repository) : IFunkoService
{
    public Result<List<Funko>, DomainErrors>  GetAll()
    {
        List<Funko> funkos = repository.GetAll();
        return Result.Success<List<Funko>, DomainErrors>(funkos);
    }
    
    public Result<Funko, DomainErrors> GetById(long id)
    {
        if (id <= 0) return Result.Failure<Funko, DomainErrors>(FunkoError.NotFound(id));
        Funko? funko = repository.GetById(id);
        if(funko == null) return Result.Failure<Funko, DomainErrors>(FunkoError.NotFound());
        return Result.Success<Funko, DomainErrors>(funko);
    }
    
    public Result<Funko, DomainErrors> Add(Funko newFunko)
    {
        if (newFunko == null) return Result.Failure<Funko, DomainErrors>(FunkoError.NotFound());
        
        //Comprobamos que el funko nuevo NO tiene un nombre igual a otro Funko
        List<Funko> listaFunkos = repository.GetAll();
        bool yaExiste = listaFunkos.Any(f => f.Name.Equals(newFunko.Name, StringComparison.OrdinalIgnoreCase));
        if (yaExiste) return Result.Failure<Funko, DomainErrors>(FunkoError.BadRequest(newFunko.Name));
        
        Funko funko = repository.Create(newFunko);
        return Result.Success<Funko, DomainErrors>(funko);
    }
    
    public Result<Funko, DomainErrors> Update(long id, Funko updatedFunko)
    {
        if (updatedFunko is null) return Result.Failure<Funko, DomainErrors>(FunkoError.NotFound());

        var funkoId = repository.GetById(id);
        //Comprobamos que el Id existe
        if (id <= 0 || funkoId is null) return Result.Failure<Funko, DomainErrors>(FunkoError.NotFound(id));
        
        //Comprobamos que el funko actualizado NO tiene un nombre igual a otro Funko
        List<Funko> listaFunkos = repository.GetAll();
        bool yaExiste = listaFunkos.Any(f => f.Name == updatedFunko.Name && f.Id != id);
        if (yaExiste) return Result.Failure<Funko, DomainErrors>(FunkoError.BadRequest(updatedFunko.Name));
        
        Funko? funko = repository.Update(id, updatedFunko);
        
        if (funko == null) return Result.Failure<Funko, DomainErrors>(FunkoError.NotFound());
        return Result.Success<Funko, DomainErrors>(funko);
    }

    public Result<bool, DomainErrors> Delete(long id)
    {
        var funkoId = repository.GetById(id);
        //Comprobamos que el Id existe
        if (id <= 0 || funkoId is null) return Result.Failure<bool, DomainErrors>(FunkoError.NotFound(id));
        
        var eliminado = repository.Delete(id);
        if (eliminado == false) return Result.Failure<bool, DomainErrors>(FunkoError.NotFound(funkoId.Name));
        return Result.Success<bool, DomainErrors>(true);
    }
    
}