namespace Movie
{
    public class Movie
    {
        public int Id { get; set; }
        public bool Adult { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }
        public string? Overview { get; set; }
        public string Language { get; set; }
        public int Budget { get; set; }
        public double Popularity { get; set; }
        public string ReleaseDate { get; set; }
        public int? Runtime { get; set; }
        public int VoteCount { get; set; }
        public double VoteAverage { get; set; }
        public int MovieId { get; set; }

    }
}
