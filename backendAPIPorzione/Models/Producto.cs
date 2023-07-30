using System;
using System.Collections.Generic;

namespace backendAPIPorzione.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string NombreProducto { get; set; } = null!;

    public virtual ICollection<Detalle> Detalles { get; set; } = new List<Detalle>();
}
