using MarzanStored.Models;
using System;

namespace MarzanStored.Dtos
{
  public class OrdenesDto
  {
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Categoria { get; set; }
    public int Cantidad { get; set; }
    public DateTime Fecha { get; set; }
  }
}
