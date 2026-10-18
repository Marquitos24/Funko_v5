using System.ComponentModel.DataAnnotations;

namespace Funkos.Models;

public record Funko
{
    public long Id { get; init; }
    
    [Required (ErrorMessage =  "El nombre del funko es obligatorio")]
    [MaxLength(100, ErrorMessage = "El nombre del Funko no puede superar los 100 caracteres")]
    [MinLength(1, ErrorMessage = "El nombre del Funko debe tener más de 1 caracteres")]
    public required string Name { get; set; }
    
    [Required (ErrorMessage =  "El precio del funko es obligatorio")]
    [RegularExpression(@"^\d{1,4},\d{1,2}$", ErrorMessage = "Formato invalido de Precio, maximo de 9999,99€")]
    public required string Precio  { get; set; }
    
    [Required(ErrorMessage = "La categoría es obligatoria")]
    [EnumDataType(typeof(Categoria), ErrorMessage = "La categoría no es válida")]
    public required Categoria Categoria { get; set; }
    
    
    public DateTime CreateAt {get; init; }
    public DateTime? UpdateAt {get; set; } 
    public DateTime? DeleteAt {get; init; }
}