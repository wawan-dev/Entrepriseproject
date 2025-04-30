using System;
using System.Collections.Generic;

namespace Entrepriseproject.Models;

public partial class User
{
    public int Id { get; set; }

    public string Psedo { get; set; } = null!;

    public string Motdepasse { get; set; } = null!;

    public virtual ICollection<Commentaire> Commentaires { get; set; } = new List<Commentaire>();
}
