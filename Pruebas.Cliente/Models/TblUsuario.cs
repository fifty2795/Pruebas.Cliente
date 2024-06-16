using System;
using System.Collections.Generic;

namespace Pruebas.Cliente.Models;

public partial class TblUsuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public int? Identificacion { get; set; }

    public string? Cargo { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaCreacionDateOnly => FechaCreacion.Date;
}
