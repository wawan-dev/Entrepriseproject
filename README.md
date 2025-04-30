# Description du projet

Le but du projet est de développer une application qui permet de valoriser les données clients pour une entreprise. Ces données, auparavant dans un fichier CSV, seront désormais stockées dans une base de données.

## Tâches réalisées :

- Permettre de rechercher une entreprise par son nom, son numéro de SIRET ou son adresse.
- La recherche est similaire à celle de Google (un seul champ de saisie).
- Afficher les informations de l'entreprise (nom, adresse, SIRET, etc.).
- Permettre de commenter une entreprise (pour partager des informations avec les autres commerciaux).
- Permettre de noter une entreprise pour évaluer la qualité des informations.
- Ajouter en base de données en un clic le résultat de l'entreprise recherchée.
- Créer un lien cliquable qui mène vers son emplacement sur Google Maps.
- Intégrer une carte dans l'application qui affiche un marqueur pour toutes les entreprises présentes dans la base de données.

## Choix techniques :

- Développement en ASP.NET Core avec Entity Framework (EF Core), utilisation de pages Razor.
- Affichage en HTML, CSS et Bootstrap.
- Base de données sur phpMyAdmin en MySQL.

## Comment lancer l'application ?

1. Récupérer le code depuis le dépôt GitHub (lien plus bas).
2. Ouvrir la solution.
3. Lancer l'application et tester ses fonctionnalités.

## Aspects de sécurité

J'ai décidé de ne pas implémenter de système d'authentification, car le système de recherche n'a rien de personnel, et les commentaires et notes permettent d'être anonymes et d'informer les autres commerciaux.

La sécurité est assurée lors de l'envoi des commentaires et des notes dans la base de données. Un système d'encodage des commentaires est en place afin de prévenir les failles XSS, et le champ de note ne permet pas de saisir une note supérieure à 5, avec une validation côté serveur.

## Évolutions à prévoir

- Empêcher l'ajout d'une entreprise déjà présente dans la base de données.
- Présenter davantage de données sur une entreprise.

## 🚀 Mon GitHub, phpMyAdmin et présentation vidéo

- **GitHub** : [https://github.com/wawan-dev/Entrepriseproject](https://github.com/wawan-dev/Entrepriseproject)
- **YouTube** : [Présentation vidéo](https://www.youtube.com/watch?v=HpSBuT8ugpc&list=PLw4NwGqyg_7udAI5yNpPVi6fhWHCjZFEw)
- **phpMyAdmin** :
  - **Nom d'utilisateur** : `girard_erwan`
  - **Mot de passe** : `6rEtw3VB`
  - **Nom de la base** : `girard_erwan_entreprise`

## Auteur

- [@Erwan Girard](https://github.com/wawan-dev/Entrepriseproject) 
