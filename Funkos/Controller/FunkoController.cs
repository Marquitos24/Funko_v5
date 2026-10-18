using Funkos.Models;
using Funkos.Repositories;
using Funkos.Dto;
using Funkos.Mappers;
using Funkos.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CSharpFunctionalExtensions;
using Funkos.Errors;

namespace Funkos.Controller;

[ApiController]
[Route("api/[controller]")]
public class FunkoController(IFunkoService service) : ControllerBase
{

    [HttpGet]
    public ActionResult<List<FunkosDtoResponse>> GetAll()
    {
       var resultado = service.GetAll();
       if (resultado.Value.Count <= 0) { return Ok("Aún no hay Funkos"); }
       
       return resultado.IsSuccess 
           ? Ok(resultado.Value.Select(f => FunkoMapper.ToDtoResponse(f)).ToList())
           : resultado.Error.ToHttpResult<List<FunkosDtoResponse>>();
    }
    
    [HttpGet("{id:long}")]
    public ActionResult<FunkosDtoResponse> GetById([FromRoute] long id)
    {
        var resultado = service.GetById(id);
        return resultado.IsSuccess 
            ? Ok(FunkoMapper.ToDtoResponse(resultado.Value))
            : resultado.Error.ToHttpResult<FunkosDtoResponse>();
    }
    
    [HttpPost]
    public ActionResult<FunkosDtoResponse> Create([FromBody] FunkosDtoRequest dto)
    {
        var request = FunkoMapper.ToEntity(dto);
        var resultado = service.Add(request);
        
            return resultado.IsSuccess 
                ? CreatedAtAction(nameof(GetById), new { id = resultado.Value.Id}, FunkoMapper.ToDtoResponse(resultado.Value) )
                : resultado.Error.ToHttpResult<FunkosDtoResponse>();
    }
    
    [HttpPut("{id:long}")]
    public ActionResult<FunkosDtoResponse> Update([FromRoute] long id, [FromBody] FunkosDtoRequest dto)
    {
        var funko = FunkoMapper.ToEntity(dto);
        var actualizado = service.Update(id, funko);
        return actualizado.IsSuccess 
            ? Ok(FunkoMapper.ToDtoResponse(actualizado.Value))
            : actualizado.Error.ToHttpResult<FunkosDtoResponse>();
        
    }
    
    [HttpDelete("{id:long}")]
    public ActionResult<bool> Delete([FromRoute] long id)
    {
            var deleted = service.Delete(id);
            if (deleted.IsFailure) { return deleted.Error.ToHttpResult<bool>(); }
            return NoContent();
    
    }
}