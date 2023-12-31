﻿using System;
using System.Collections.Generic;

namespace backendAPIPorzione.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string? Clave { get; set; }

    public byte[]? ClaveHash { get; set; }

    public byte[]? ClaveSalt { get; set; }

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
}
