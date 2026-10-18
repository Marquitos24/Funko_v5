using CSharpFunctionalExtensions;
using Funkos.Errors;
using Funkos.Models;

namespace Funkos.Service;

public interface IFunkoService
{
    Result<List<Funko>, DomainErrors> GetAll();
    Result<Funko, DomainErrors> GetById(long id);
    Result<Funko, DomainErrors> Add(Funko newFunko);
    Result<Funko, DomainErrors> Update(long id, Funko updatedFunko);
    Result<bool, DomainErrors> Delete(long id);
}