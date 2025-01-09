namespace Entrepriseproject.Models
{
    public class Entreprise
    {
        public int? Id { get; set; }
        public string? Nom { get; set; }
        public string? Siren { get; set; }
        public string? Siret { get; set; }
        public string? Adresse { get; set; }
        public string? Code_postal { get; set; }
        public string? Ville { get; set; }
        public string? Departement { get; set; }
        public string? Pays { get; set; } = "France";
        public DateTime? Date_Creation { get; set; }
        public string? Dirigeants { get; set; }
        public string? Activite { get; set; }
        public string? code_ape { get; set; }
        public string? CategorieEntreprise { get; set; }
        public string? Coordonnees { get; set; }
        public string? Commentaire { get; set; }
        public double? note { get; set; }

    }

    public class Result
    {
        public string Siren { get; set; }
        public string nom_complet { get; set; }
        public string NomRaisonSociale { get; set; }
        public string? Sigle { get; set; }
        public int NombreEtablissements { get; set; }
        public int NombreEtablissementsOuverts { get; set; }
        public Siege Siege { get; set; }
        public string activite_principale { get; set; }
        public string? Categorie_Entreprise { get; set; }
        public string? CaractereEmployeur { get; set; }
        public string DateCreation { get; set; }
        public string? DateFermeture { get; set; }
        public string DateMiseAJour { get; set; }
        public string DateMiseAJourInsee { get; set; }
        public string EtatAdministratif { get; set; }
        public string NatureJuridique { get; set; }
        public string SectionActivitePrincipale { get; set; }
        public List<Dirigeant> Dirigeants { get; set; }
        public List<MatchingEtablissement> MatchingEtablissements { get; set; }
        public Finances? Finances { get; set; }
        public Complements Complements { get; set; }
    }

    public class Siege
    {
        public string activite_principale { get; set; }
        public string? ActivitePrincipaleRegistreMetier { get; set; }
        public string? AnneeTrancheEffectifSalarie { get; set; }
        public string Adresse { get; set; }
        public string CaractereEmployeur { get; set; }
        public string? Cedex { get; set; }
        public string Code_Postal { get; set; }
        public string Commune { get; set; }
        public string? ComplementAdresse { get; set; }
        public string Coordonnees { get; set; }
        public string DateCreation { get; set; }
        public string DateDebutActivite { get; set; }
        public string? DateFermeture { get; set; }
        public string? DateMiseAJour { get; set; }
        public string DateMiseAJourInsee { get; set; }
        public string departement { get; set; }
        public string? DistributionSpeciale { get; set; }
        public string Epci { get; set; }
        public bool EstSiege { get; set; }
        public string EtatAdministratif { get; set; }
        public string GeoAdresse { get; set; }
        public string GeoId { get; set; }
        public string? IndiceRepetition { get; set; }
        public string Latitude { get; set; }
        public string? LibelleCedex { get; set; }
        public string Libelle_Commune { get; set; }
        public string? LibelleCommuneEtranger { get; set; }
        public string? LibellePaysEtranger { get; set; }
        public string LibelleVoie { get; set; }
        public string? ListeEnseignes { get; set; }
        public string NumeroVoie { get; set; }
        public string Region { get; set; }
        public string Siret { get; set; }
        public string StatutDiffusionEtablissement { get; set; }
        public string? TrancheEffectifSalarie { get; set; }
        public string TypeVoie { get; set; }
    }

    public class Dirigeant
    {
        public string Nom { get; set; }
        public string Prenoms { get; set; }
        public string AnneeDeNaissance { get; set; }
        public string DateDeNaissance { get; set; }
        public string Qualite { get; set; }
        public string Nationalite { get; set; }
        public string TypeDirigeant { get; set; }
    }

    public class MatchingEtablissement
    {
        public string ActivitePrincipale { get; set; }
        public bool AncienSiege { get; set; }
        public string? AnneeTrancheEffectifSalarie { get; set; }
        public string Adresse { get; set; }
        public string CaractereEmployeur { get; set; }
        public string CodePostal { get; set; }
        public string Commune { get; set; }
        public string DateCreation { get; set; }
        public string DateDebutActivite { get; set; }
        public string? DateFermeture { get; set; }
        public string Epci { get; set; }
        public bool EstSiege { get; set; }
        public string EtatAdministratif { get; set; }
        public string GeoId { get; set; }
        public string Latitude { get; set; }
        public string LibelleCommune { get; set; }
        public string Longitude { get; set; }
        public string? NomCommercial { get; set; }
        public string Region { get; set; }
        public string Siret { get; set; }
        public string StatutDiffusionEtablissement { get; set; }
        public string? TrancheEffectifSalarie { get; set; }
    }

    public class Finances
    {
        // Ajoutez les propriétés spécifiques aux finances si nécessaire
    }

    public class Complements
    {
        public bool ConventionCollectiveRenseignee { get; set; }
        public bool EstAssociation { get; set; }
        public bool EstBio { get; set; }
        public bool EstEntrepreneurIndividuel { get; set; }
        public bool EstEntrepreneurSpectacle { get; set; }
        public bool EstEss { get; set; }
        public bool EstFiness { get; set; }
        public bool EstOrganismeFormation { get; set; }
        public bool EstQualiopi { get; set; }
        public bool EstRge { get; set; }
        public bool EstServicePublic { get; set; }
        public bool EstSiae { get; set; }
        public bool EstSocieteMission { get; set; }
        public bool EstUai { get; set; }
    }

    public class EntrepriseResult
    {
        public List<Result> Results { get; set; }
        public int Total_Results { get; set; }
        public int Page { get; set; }
        public int Per_Page { get; set; }
        public int total_pages { get; set; }
    }

    public class Entreprisepage
    {
        public List<Entreprise> Results { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public string Query { get; set; }

    }

    public class Paginer<T>
    {
        public List<T> Entreprises { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }


}
