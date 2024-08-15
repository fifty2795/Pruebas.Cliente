using System;
using System.Collections.Generic;

namespace Prueba.Core.Models;

public partial class TblProducto
{
    public int IdProducto { get; set; }

    public string? Nombre { get; set; }

    public string? Detalle { get; set; }

    public decimal? Precio { get; set; }

    public int? Cantidad { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? Activo { get; set; }
}
