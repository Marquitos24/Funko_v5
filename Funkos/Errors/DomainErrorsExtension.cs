using Microsoft.AspNetCore.Mvc;

namespace Funkos.Errors;

public static class DomainErrorsExtension
{
    public static ActionResult<T> ToHttpResult<T>(this DomainErrors error) => error switch
    {
        NotFoundError => new NotFoundObjectResult(new { message = error.Message }),
        ValidationError ve => new BadRequestObjectResult(new { message = ve.Message, errors = ve.Errors }),
        ConflictError => new ConflictObjectResult(new { message = error.Message }),
        ForbiddenError => new ObjectResult(new { message = error.Message }) { StatusCode = 403 },
        BusinessRuleError => new UnprocessableEntityObjectResult(new { message = error.Message }),
        _ => new ObjectResult(new { message = "Error interno del servidor" }) { StatusCode = 500 }
    };
}