using Microsoft.EntityFrameworkCore;
using System;
using Portfolie2.Domain;

namespace Portfolie2
{
    
    public class IMDBContext : DbContext
    {
     
        public DbSet<Award> Awards{ get; set; }
        public DbSet<Bookmark> Bookmarks{ get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<KnownForTitle> KnownForTitles { get; set; }
        public DbSet<NameBasic> NameBasics { get; set; }
        public DbSet<Plays> Plays{ get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<RatingHistory> RatingHistorys { get; set; }
        public DbSet<SearchHistory> SearchHistorys { get; set; }
        public DbSet<TitleAka> TitleAkas { get; set; }
        public DbSet<TitleBasic> TitleBasics { get; set; }
        public DbSet<TitleEpisode> TitleEpisodes { get; set; }
        public DbSet<TitleGenre> TitleGenres { get; set; }
        public DbSet<TitlePrincipal> TitlePrincipalss { get; set; }
        public DbSet<TitleRating> TitleRating { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wi> Wi{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseNpgsql("host=localhost;db=Northwind;uid=postgres;pwd=");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Award>().ToTable("awards");
            modelBuilder.Entity<Award>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Award>().Property(x => x.AwardName).HasColumnName("award");

            modelBuilder.Entity<Bookmark>().ToTable("bookmarks");
            modelBuilder.Entity<Bookmark>().Property(x => x.Username).HasColumnName("user_name");
            modelBuilder.Entity<Bookmark>().Property(x => x.TitleId).HasColumnName("title_id");

            modelBuilder.Entity<Comment>().ToTable("comments");
            modelBuilder.Entity<Comment>().Property(x => x.Username).HasColumnName("user_name");
            modelBuilder.Entity<Comment>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Comment>().Property(x => x.Content).HasColumnName("content");
            modelBuilder.Entity<Comment>().Property(x => x.TimeStamp).HasColumnName("time_stamp");

            modelBuilder.Entity<TitleEpisode>().ToTable("episode2");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.Id).HasColumnName("title_id");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.ParentTitleId).HasColumnName("parent_title_id");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.SeasonNumber).HasColumnName("season_number");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.EpisodeNumber).HasColumnName("episode_number");


            modelBuilder.Entity<KnownForTitle>().ToTable("know_for_titles");
            modelBuilder.Entity<KnownForTitle>().Property(x => x.NameId).HasColumnName("name_id");
            modelBuilder.Entity<KnownForTitle>().Property(x => x.TitleId).HasColumnName("title_id");

            modelBuilder.Entity<TitleAka>().ToTable("title_akas2");
            modelBuilder.Entity<TitleAka>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<TitleAka>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<TitleAka>().Property(x => x.Title).HasColumnName("title");
            modelBuilder.Entity<TitleAka>().Property(x => x.Region).HasColumnName("region");
            modelBuilder.Entity<TitleAka>().Property(x => x.Language).HasColumnName("language");
            modelBuilder.Entity<TitleAka>().Property(x => x.Types).HasColumnName("types");
            modelBuilder.Entity<TitleAka>().Property(x => x.Attributes).HasColumnName("attributes");
            modelBuilder.Entity<TitleAka>().Property(x => x.IsOriginalTitle).HasColumnName("is_original_title");


            modelBuilder.Entity<TitleBasic>().ToTable("title_basics2");
            modelBuilder.Entity<TitleBasic>().Property(x => x.Id).HasColumnName("title_id");
            modelBuilder.Entity<TitleBasic>().Property(x => x.TitleType).HasColumnName("title_type");
            modelBuilder.Entity<TitleBasic>().Property(x => x.PrimaryTitle).HasColumnName("primary_title");
            modelBuilder.Entity<TitleBasic>().Property(x => x.OriginalTitle).HasColumnName("original_title");
            modelBuilder.Entity<TitleBasic>().Property(x => x.IsAdult).HasColumnName("is_adult");
            modelBuilder.Entity<TitleBasic>().Property(x => x.StartYear).HasColumnName("start_year");
            modelBuilder.Entity<TitleBasic>().Property(x => x.EndYear).HasColumnName("end_year");
            modelBuilder.Entity<TitleBasic>().Property(x => x.Runtime).HasColumnName("runtime_minutes");
            modelBuilder.Entity<TitleBasic>().Property(x => x.Plot).HasColumnName("plot");
            modelBuilder.Entity<TitleBasic>().Property(x => x.Poster).HasColumnName("poster");


            modelBuilder.Entity<NameBasic>().ToTable("name_basics2");
            modelBuilder.Entity<NameBasic>().Property(x => x.Id).HasColumnName("name_id");
            modelBuilder.Entity<NameBasic>().Property(x => x.PrimaryName).HasColumnName("primary_name");
            modelBuilder.Entity<NameBasic>().Property(x => x.BirthYear).HasColumnName("birth_year");
            modelBuilder.Entity<NameBasic>().Property(x => x.DeathYear).HasColumnName("death_year");
            modelBuilder.Entity<NameBasic>().Property(x => x.Rating).HasColumnName("rating");

            modelBuilder.Entity<TitlePrincipal>().ToTable("title_principals2");
            modelBuilder.Entity<TitlePrincipal>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<TitlePrincipal>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<TitlePrincipal>().Property(x => x.NameId).HasColumnName("name_id");
            modelBuilder.Entity<TitlePrincipal>().Property(x => x.Category).HasColumnName("category");
            modelBuilder.Entity<TitlePrincipal>().Property(x => x.Job).HasColumnName("job");

            modelBuilder.Entity<TitleGenre>().ToTable("title_genre");
            modelBuilder.Entity<TitleGenre>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<TitleGenre>().Property(x => x.Genre).HasColumnName("genre");


            modelBuilder.Entity<Wi>().ToTable("wi2");
            modelBuilder.Entity<Wi>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Wi>().Property(x => x.Word).HasColumnName("word");
            modelBuilder.Entity<Wi>().Property(x => x.Field).HasColumnName("field");
            modelBuilder.Entity<Wi>().Property(x => x.Lexeme).HasColumnName("lexeme");

            modelBuilder.Entity<TitleRating>().ToTable("title_ratings2");
            modelBuilder.Entity<TitleRating>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<TitleRating>().Property(x => x.AvgRating).HasColumnName("avg_rating");
            modelBuilder.Entity<TitleRating>().Property(x => x.NumVotes).HasColumnName("num_votes");
             

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName("password");


            modelBuilder.Entity<Plays>().ToTable("plays");
            modelBuilder.Entity<Plays>().Property(x => x.NameId).HasColumnName("name_id");
            modelBuilder.Entity<Plays>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Plays>().Property(x => x.Character).HasColumnName("character");

            modelBuilder.Entity<Profession>().ToTable("profession");
            modelBuilder.Entity<Profession>().Property(x => x.NameId).HasColumnName("name_id");
            modelBuilder.Entity<Profession>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<Profession>().Property(x => x.ProfessionName).HasColumnName("profession");

            modelBuilder.Entity<RatingHistory>().ToTable("rating_history");
            modelBuilder.Entity<RatingHistory>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<RatingHistory>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<RatingHistory>().Property(x => x.Rating).HasColumnName("rating");
            modelBuilder.Entity<RatingHistory>().Property(x => x.TimeStamp).HasColumnName("time_stamp");



            modelBuilder.Entity<SearchHistory>().ToTable("search_history");
            modelBuilder.Entity<SearchHistory>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<SearchHistory>().Property(x => x.SearchInput).HasColumnName("search_input");
            modelBuilder.Entity<SearchHistory>().Property(x => x.TimeStamp).HasColumnName("time_stamp");


        }
    }
}
