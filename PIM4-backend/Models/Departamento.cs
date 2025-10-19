using PIM4_backend.Models;
using System.ComponentModel.DataAnnotations;

public class Departamento
{
    [Key]
    public int IdDepartamento { get; set; }

    [Required]
    [MaxLength(150)]
    public string Nome { get; set; } = null!;

    [MaxLength(300)]
    public string? Descricao { get; set; }

    public ICollection<Usuario>? Usuarios { get; set; }
}
