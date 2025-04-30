using System;
using System.Collections.Generic;

namespace Entrepriseproject.Models;

public partial class Commentaire
{
    public int IdCommentaire { get; set; }

    public int IdEntreprise { get; set; }

    public string Commentaire1 { get; set; } = null!;

    public DateTime? DateCreation { get; set; }

    public int Note { get; set; }

    public int? IdUser { get; set; }

    public virtual Entreprise IdEntrepriseNavigation { get; set; } = null!;

    public virtual User? IdUserNavigation { get; set; }
}
