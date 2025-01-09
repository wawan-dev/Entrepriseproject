
# Description du projet

Le but du projet est développer une application qui permet de valoriser les données client pour une entreprise. Ces données étant avant dans un CSV, ils seront maintenant sur une base de données.
## Tâche réaliser :

● Permettre de rechercher une entreprise par son nom, son numéro de SIRET, ou son
adresse.

● La recherche est similaire à celle de Google (un seul champ de saisie).

● Afficher les informations de l'entreprise (nom, adresse, SIRET, etc.)

● Permettre de commenter une entreprise (pour partager des informations avec les autres
commerciaux).

● Permettre de noter une entreprise pour évaluer la qualité des informations

● Ajouter en base de donné en 1 clique le resultat de l'entreprise recherché

● Faire un lien cliquable qui mène vers sont emplacement sur googlemap

● Une map intégrer dans l'application qui met un petit tic sur la map pour toutes les entreprises présente dans la base de donnée

## Choix Technique :

Je développe en ASP.NET Core avec Entity Framework (EF Core), Utilisation de page razor.

Affichage en HTML, CSS et Bootstrap.

La base de données est sur phpmyadmin en MySql.


## Comment lancer l'application ?

Récupérer le code depuis le dépot github ( lien plus bas).
Lancer la solution et tester l'application.


## Aspect sécurité

Je n'est pas décider de faire un système d'authentification car le système de recherche n'a rien de personnel et les commentaire et note permette d'être anonyme et juste informer les autres commerciaux.

La sécurité est présente lorsque les commentaires et note sont envoyer dans la base de donnée, un système pour encoder le commentaires est présent afin de ne pas laisser de faille XSS, et le champs de note ne permet pas de saisir une note > 5 et le code en arrière plan le traite également.


## 🚀 Mon github et phpmyadmin
GITHUB

https://github.com/wawan-dev/Entrepriseproject

PHPMYADMIN:

Mdp -> 6rEtw3VB

Login -> girard_erwan

Nom de la base -> girard_erwan_entreprise





## Authors

- [@Erwan Girard](https://github.com/wawan-dev/Entrepriseproject)

