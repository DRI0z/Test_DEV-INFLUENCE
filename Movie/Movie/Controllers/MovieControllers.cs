using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Movie.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        // Injection de dépendance pour créer un client Http
        private readonly HttpClient _httpClient;
        public MovieController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // Déclaration d'une méthode GET pour obtenir un film aléatoire
        [HttpGet]
        public async Task<IActionResult> GetMovie()
        {
            Random aleatoire = new Random();
            int movieNumber = aleatoire.Next(5, 100); //Génère un entier aléatoire positif entre 5 et 100

            // Construire l'URL de la requête
            string url = "https://api.themoviedb.org/3/movie/"+ movieNumber;
            string apiKey = "?api_key=f68a1184cc09e2d8f1db382232207d37";

            // Envoi de la requête GET à l'API
            var response = await _httpClient.GetAsync(url + apiKey);

            // Vérifier si la réponse est valide
            if (response.IsSuccessStatusCode)
            {
                // Obtenir le corps de la réponse
                string responseBody = await response.Content.ReadAsStringAsync();

                // Parse la réponse HTTP en format JSON en utilisant JObject
                JObject json = JObject.Parse(responseBody);

                // Extraction des valeurs souhaitées à partir du JSON
                int movieId = (int)json["id"]; // identifiant du film
                bool adult = (bool)json["adult"]; // contenu pour adulte ou non
                string title = (string)json["title"]; // titre du film

                // Extraction des genres du film à partir d'un tableau JSON
                JArray genres = (JArray)json["genres"];
                List<string> genreNames = new List<string>();
                foreach (JObject genre in genres)
                {
                    genreNames.Add((string)genre["name"]);
                }
                string genresString = string.Join(", ", genreNames); //conversion de la liste des genres en chaîne de caractères

                string overview = (string)json["overview"];
                string language = (string)json["original_language"];
                int budget = (int)json["budget"];
                double popularity = (double)json["popularity"];
                string releaseDate = (string)json["release_date"];
                int runtime = (int)json["runtime"];
                int voteCount = (int)json["vote_count"];
                double voteAverage = (double)json["vote_average"];

                //Variable qui contient toute les informations récuperer au dessus sous forme de chaine de caractère
                string infos =
                    "\nid : " + movieId +
                    "\nadult : " + adult +
                    "\ntitle : " + title +
                    "\ngenre : " + genresString +
                    "\noverview : " + overview +
                    "\nlanguage : " + language +
                    "\nbudget : " + budget + " $" +
                    "\npopularity : " + popularity +
                    "\nrelease date : " + releaseDate +
                    "\nruntime : " + runtime + " minutes" +
                    "\nvote count : " + voteCount +
                    "\nvote average : " + voteAverage +
                    "\n";



                // Utilisation de la classe MovieContext pour accéder à la base de données
                using (var context = new MovieContext())
                {
                    // Récupération de la liste des identifiants de films existants dans la base de données
                    var moviesIdList = context.Movies.Select(m => m.MovieId).ToList();

                    // Vérification si l'identifiant du film passé en paramètre existe déjà dans la base de données
                    for (int i = 0; i < moviesIdList.Count(); i++)
                    {
                        if (movieId == moviesIdList[i])
                        {
                            // Si oui, retourne une réponse OK avec les informations du film
                            return Ok(infos);
                        }
                    }
                    // Si le film n'existe pas encore dans la base de données, on l'ajoute
                    var movie = new Movie
                    {
                        Adult = adult,
                        Title = title,
                        Genres = genresString,
                        Overview = overview,
                        Language = language,
                        Budget = budget,
                        Popularity = popularity,
                        ReleaseDate = releaseDate,
                        Runtime = runtime,
                        VoteCount = voteCount,
                        VoteAverage = voteAverage,
                        MovieId = movieId
                    };

                    context.Add(movie);
                    context.SaveChanges();
                }
                // On retourne les informations récupérées
                return Ok(infos);
            }
            else
            {
                return NotFound();
            }
        }
    }
}