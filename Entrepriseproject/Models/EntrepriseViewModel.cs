namespace Entrepriseproject.Models
{
    public class Entreprise
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Siren { get; set; }
        public string Siret { get; set; }
        public string Adresse { get; set; }
        public string Code_postal { get; set; }
        public string Ville { get; set; }
        public string Departement { get; set; }
        public string Region { get; set; }
        public string Pays { get; set; } = "France";
        public DateTime Date_Creation { get; set; }
        public string Forme_Juridique { get; set; }
        public int? Effectif { get; set; }
        public string Dirigeants { get; set; }
        public string Activite { get; set; }
        public string code_ape { get; set; }
    }
}
