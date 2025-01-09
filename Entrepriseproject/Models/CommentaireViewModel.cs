namespace Entrepriseproject.Models
{

    using System.ComponentModel.DataAnnotations.Schema;
    public class Commentaire
    {
        [Column("id_commentaire")]  // Correspond au champ dans la base de données
        public int Id { get; set; }  // Clé primaire

        [Column("commentaire")]
        public string Texte { get; set; }  // Texte du commentaire

        [Column("note")]
        public double Note { get; set; }   // Note

        [Column("id_entreprise")]  // Correspondance avec le champ 'id_entreprise' dans la base de données
        public int EntrepriseId { get; set; }  // Clé étrangère vers Entreprise

        public Entreprise Entreprise { get; set; }  // Propriété de navigation

        [Column("date_creation")]
        public DateTime Date_Creation { get; set; }
    }

}
