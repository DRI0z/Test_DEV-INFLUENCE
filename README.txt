Ouvrez "Movie.sln" dans le fichier "Movie" via l'IDE Visual Studio,

Une fois le projet ouvert sur Visual Studio,


1- Allez dans Outils > Gestionnaire de Packages NuGet > Console du Gestionnaire de Packages NuGet
2- Rentrer dans la console : "Update-Database" pour créer la base de données utile au programme
3- Ensuite, allez dans Affichage > Explorateur d'objets SQL Server
4- Dérouler le menu déroulant "SQL Server" >"(localdb)\\MSSQLLocalDB..." > "Bases de données"
5- Puis vérifier que la base de données "Movies" sois bien présente

Ensuite vous pourrez démarrer le code, une page s'ouvrira.

Vous devrez appuyer sur : le menu déroulant "GET/Movie" > "Try it Out" et enfin "Execute"

À chaque fois que vous cliquerez sur "Execute" une requete vers L'API TheMoviesDB seffecturera et
Les données seront récoltées dans la base de données "Movies"

Pour consulter ces données, vous devrez revenir dans "Explorateur d'objet SQL Server"

Dérouler "Movies" > "Tables"

Puis cliquer droit sur "dbo.movies" > Afficher les données.