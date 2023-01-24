using Microsoft.EntityFrameworkCore;

namespace Movie
{
    public class MovieContext : DbContext
    {
        //Déclaration d'un DbSet<Movie> qui est utilisé pour accéder aux données de la table "Movies" dans la base de données
        public DbSet<Movie> Movies { get; set; }
        //Surcharge de la méthode OnConfiguring pour configurer la connexion à la base de données
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Utilisation de la source de données SQL Server avec les paramètres spécifiés (localDB) pour se connecter à la base de données "Movies"
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Database=Movies");
        }
    }
}
