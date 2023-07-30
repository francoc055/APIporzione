using System;
using System.Collections.Generic;

namespace backendAPIPorzione.Models;

public partial class Detalle
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdMenu { get; set; }

    public int? IdProducto { get; set; }

    public virtual Menu? IdMenuNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
