using System;
using System.Collections.Generic;

namespace backendAPIPorzione.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string Categoria { get; set; } = null!;

    public decimal Precio { get; set; }

    public int? IdUsuario { get; set; }

    public virtual ICollection<Detalle> Detalles { get; set; } = new List<Detalle>();

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
