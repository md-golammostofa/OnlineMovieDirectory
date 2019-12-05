using MovieDirectory.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieDirectory.Models
{
    public enum Origin
    {
        USA, China, UK, France, India, Japan, Indonesia, Bangladesh
    }
    public class Genre
    {
        public Genre()
        {
            this.Movies = new List<Movie>();
        }
        public int GenreId { get; set; }
        [Required, StringLength(50), Display(Name = "Genre Name")]
        public string GenreName { get; set; }
        [Required, StringLength(100)]
        public string Description { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
    
    public class Movie
    {
        public int MovieId { get; set; }
        [Required, StringLength(50), Display(Name = "Movie Name")]
        public string MovieName { get; set; }
        [Required, Range(0, 10)]
        public decimal Rating { get; set; }
        [Required, Range(1700, 2030), Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }
        [Required, EnumDataType(typeof(Origin))]
        public Origin Origin { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal? Budget { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal? Gross { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal? Profit
        {
            get { return Gross - Budget; }
        }
        [Required ,ForeignKey("Genre")]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        //public List<Genre> GenreCollection { get; set; }
        public IEnumerable<Genre> GenreCollection { get; set; }
    }
    public class MovieDbContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new Initializer());
            base.OnModelCreating(modelBuilder);
        }

    }
    public class Initializer : DropCreateDatabaseIfModelChanges<MovieDbContext>
    {
        protected override void Seed(MovieDbContext context)
        {
            base.Seed(context);
            var gen1 = new Genre { GenreName = "Action", Description= "Action story Includes fight scenes, daring escapes etc." };
            var gen2 = new Genre { GenreName = "Adventure", Description = "Adventure story is about journeys to epic to accomplish something." };
            var gen3 = new Genre { GenreName = "Comedy", Description = "Comedy is about a series of funny, or comical events" };
            var gen4 = new Genre { GenreName = "Drama", Description = "Drama is a genre of narrative fiction or semi-fiction" };
            var gen5 = new Genre { GenreName = "Animation", Description = "Animated movies are among the top-grossing Hollywood films today." };
            var gen6 = new Genre { GenreName = "Science fiction", Description = " It generally includes computers or machines, alien, etc." };
            context.Genres.Add(gen1);
            context.Genres.Add(gen2);
            context.Genres.Add(gen3);
            context.Genres.Add(gen4);
            context.Genres.Add(gen5);
            context.Genres.Add(gen6);
            context.SaveChanges();
            context.Movies.Add(new Movie { MovieName = "Avengers", Origin=Origin.USA, ReleaseYear= 2019, Rating=8.9M, Genre=gen1, Budget=562000, Gross=1247000 });
            context.Movies.Add(new Movie { MovieName = "Hot Shots", Origin=Origin.USA, ReleaseYear= 1996, Rating=7.4M, Genre=gen3, Budget=120000, Gross=238000 });
            context.Movies.Add(new Movie { MovieName = "Adventure Land", Origin=Origin.China, ReleaseYear= 2015, Rating=6.8M, Genre=gen2, Budget=150600, Gross=225000 });
            context.Movies.Add(new Movie { MovieName = "Robot 2.0", Origin=Origin.India, ReleaseYear= 2019, Rating=7.4M, Genre=gen1, Budget=194500, Gross=285500 });
            context.Movies.Add(new Movie { MovieName = "Monster Hunt", Origin=Origin.China, ReleaseYear= 2017, Rating=6.0M, Genre=gen2, Budget=115700, Gross=245600 });
            context.Movies.Add(new Movie { MovieName = "Emilia", Origin=Origin.France, ReleaseYear= 1992, Rating=8.8M, Genre=gen4, Budget=89000, Gross=230000 });
            context.Movies.Add(new Movie { MovieName = "Toy Story", Origin=Origin.USA, ReleaseYear= 2006, Rating=7.5M, Genre=gen5, Budget=189000, Gross=306000 });
            context.Movies.Add(new Movie { MovieName = "Mr Bean", Origin=Origin.UK, ReleaseYear= 2012, Rating=6.9M, Genre=gen3, Budget=230000, Gross=350000 });
            context.Movies.Add(new Movie { MovieName = "Moana", Origin=Origin.USA, ReleaseYear= 2018, Rating=7.2M, Genre=gen5, Budget=323000, Gross=465000 });
            context.Movies.Add(new Movie { MovieName = "Arrival", Origin=Origin.USA, ReleaseYear= 2016, Rating=7.9M, Genre=gen6, Budget=430400, Gross=765000 });
            context.Movies.Add(new Movie { MovieName = "Inception", Origin=Origin.USA, ReleaseYear= 2010, Rating=8.8M, Genre=gen6, Budget=650000, Gross=935400 });
            context.Movies.Add(new Movie { MovieName = "Gravity", Origin=Origin.UK, ReleaseYear= 2013, Rating=9.0M, Genre=gen6, Budget=450000, Gross=765400 });
            context.Movies.Add(new Movie { MovieName = "Spirited Away", Origin=Origin.Japan, ReleaseYear= 2001, Rating=8.4M, Genre=gen5, Budget=250000, Gross=425400 });
            context.Movies.Add(new Movie { MovieName = "Projapoti", Origin=Origin.Bangladesh, ReleaseYear= 2013, Rating=6.8M, Genre=gen4, Budget=56000, Gross=70400 });
            context.Movies.Add(new Movie { MovieName = "The One", Origin=Origin.China, ReleaseYear= 2008, Rating=5.9M, Genre=gen1, Budget=160000, Gross=240400 });
            context.SaveChanges();
        }
    }
}