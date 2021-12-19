using Microsoft.EntityFrameworkCore;
using System;
using DataServiceLib.Domain;

namespace DataServiceLib
{
    
    public class IMDBContext : DbContext
    {
     
        public DbSet<Award> Awards{ get; set; }
        public DbSet<Bookmark> Bookmarks{ get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TitleRating> TitleRatings { get; set; }
        public DbSet<KnownForTitle> KnownForTitles { get; set; }
        public DbSet<NameBasic> NameBasics { get; set; }
        public DbSet<Plays> Plays{ get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<RatingHistory> RatingHistories { get; set; }
        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<TitleAka> TitleAkas { get; set; }
        public DbSet<TitleBasic> TitleBasics { get; set; }
        public DbSet<TitleEpisode> TitleEpisodes { get; set; }
        public DbSet<TitleGenre> TitleGenres { get; set; }
        public DbSet<TitlePrincipal> TitlePrincipals { get; set; }
        public DbSet<TitleRating> TitleRating { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wi> Wi{ get; set; }
        public DbSet<TitleBasicSearchResult> TitleBasicSearchResults { get; set; }
        public DbSet<NameBasicSearchResult> NameBasicSearchResults { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();

            
           //RUC host
           optionsBuilder.UseNpgsql("host=rawdata.ruc.dk;db=raw2;uid=raw2;pwd=OKaSaRYv");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<TitleBasicSearchResult>().HasNoKey();
            modelBuilder.Entity<TitleBasicSearchResult>().Property(x => x.Id).HasColumnName("title_id");
            modelBuilder.Entity<TitleBasicSearchResult>().Property(x => x.PrimaryTitle).HasColumnName("primary_title");

            modelBuilder.Entity<NameBasicSearchResult>().HasNoKey();
            modelBuilder.Entity<NameBasicSearchResult>().Property(x => x.Id).HasColumnName("name_id");
            modelBuilder.Entity<NameBasicSearchResult>().Property(x => x.PrimaryName).HasColumnName("primary_name");


            modelBuilder.Entity<Award>().ToTable("awards");
            modelBuilder.Entity<Award>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Award>().Property(x => x.AwardName).HasColumnName("award");
            modelBuilder.Entity<Award>().HasKey(c => new { c.TitleId});
            

            modelBuilder.Entity<Bookmark>().ToTable("bookmarks");
            modelBuilder.Entity<Bookmark>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<Bookmark>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Bookmark>().HasKey(c => new { c.TitleId, c.Username });
            modelBuilder.Entity<Bookmark>().HasOne<User>(s => s.User).WithMany(g => g.Bookmarks)
            .HasForeignKey(s => s.TitleId);
            modelBuilder.Entity<Bookmark>().HasOne<TitleBasic>(s => s.TitleBasic).WithMany(g => g.Bookmarks)
            .HasForeignKey(s => s.TitleId);

            modelBuilder.Entity<Comment>().ToTable("comments");
            modelBuilder.Entity<Comment>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<Comment>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Comment>().Property(x => x.Content).HasColumnName("context");
            modelBuilder.Entity<Comment>().Property(x => x.TimeStamp).HasColumnName("time_stamp");
            modelBuilder.Entity<Comment>().HasKey(c => new { c.Username, c.TitleId, c.TimeStamp });
            modelBuilder.Entity<Comment>().HasOne<TitleBasic>(s => s.TitleBasic).WithMany(g => g.Comments)
            .HasForeignKey(s => s.TitleId);


            modelBuilder.Entity<TitleEpisode>().ToTable("episode");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.Id).HasColumnName("title_id");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.ParentTitleId).HasColumnName("parent_title_id");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.SeasonNumber).HasColumnName("season_number");
            modelBuilder.Entity<TitleEpisode>().Property(x => x.EpisodeNumber).HasColumnName("episode_number");
            modelBuilder.Entity<TitleEpisode>().HasOne<TitleBasic>(s => s.TitleBasic).WithMany(g => g.TitleEpisodes)
            .HasForeignKey(s => s.ParentTitleId);


            modelBuilder.Entity<KnownForTitle>().ToTable("known_for_titles");
            modelBuilder.Entity<KnownForTitle>().Property(x => x.NameId).HasColumnName("name_id");
            modelBuilder.Entity<KnownForTitle>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<KnownForTitle>().HasKey(c => new { c.TitleId, c.NameId });

            modelBuilder.Entity<TitleAka>().ToTable("title_akas");
            modelBuilder.Entity<TitleAka>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<TitleAka>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<TitleAka>().Property(x => x.Title).HasColumnName("title");
            modelBuilder.Entity<TitleAka>().Property(x => x.Region).HasColumnName("region");
            modelBuilder.Entity<TitleAka>().Property(x => x.Language).HasColumnName("language");
            modelBuilder.Entity<TitleAka>().Property(x => x.Type).HasColumnName("type");
            modelBuilder.Entity<TitleAka>().Property(x => x.Attribute).HasColumnName("attribute");
            modelBuilder.Entity<TitleAka>().Property(x => x.IsOriginalTitle).HasColumnName("is_original_title");
            modelBuilder.Entity<TitleAka>().HasKey(c => new { c.TitleId, c.Ordering });


            modelBuilder.Entity<TitleBasic>().ToTable("title_basics");
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
            modelBuilder.Entity<TitleBasic>().HasOne(a => a.TitleRating).WithOne(x => x.TitleBasic)
                .HasForeignKey<TitleRating>(e => e.TitleId);
            modelBuilder.Entity<TitleBasic>().HasMany(a => a.Awards).WithOne(x => x.TitleBasic)
                .HasForeignKey(e => e.TitleId);
            modelBuilder.Entity<TitleBasic>().HasMany(a => a.TitlePrincipals).WithOne(x => x.TitleBasic)
                .HasForeignKey(e => e.TitleId);
            modelBuilder.Entity<TitleBasic>().HasMany(a => a.TitleAkas).WithOne(x => x.TitleBasic)
                .HasForeignKey(e => e.TitleId);
            modelBuilder.Entity<TitleBasic>().HasMany(a => a.TitleGenres).WithOne(x => x.TitleBasic)
                .HasForeignKey(e => e.TitleId);
            modelBuilder.Entity<TitleBasic>().HasMany(a => a.KnownForTitles).WithOne(x => x.TitleBasic)
                .HasForeignKey(e => e.TitleId);
            modelBuilder.Entity<TitleBasic>().HasMany(a => a.Plays).WithOne(x => x.TitleBasic)
                .HasForeignKey(e => e.TitleId);


            modelBuilder.Entity<NameBasic>().ToTable("name_basics");
            modelBuilder.Entity<NameBasic>().Property(x => x.Id).HasColumnName("name_id");
            modelBuilder.Entity<NameBasic>().Property(x => x.PrimaryName).HasColumnName("primary_name");
            modelBuilder.Entity<NameBasic>().Property(x => x.BirthYear).HasColumnName("birth_year");
            modelBuilder.Entity<NameBasic>().Property(x => x.DeathYear).HasColumnName("death_year");
            modelBuilder.Entity<NameBasic>().Property(x => x.Rating).HasColumnName("rating");
            modelBuilder.Entity<NameBasic>().HasMany(a => a.TitlePrincipals).WithOne(x => x.NameBasic)
                .HasForeignKey(e => e.NameId);
            modelBuilder.Entity<NameBasic>().HasMany(a => a.Professions).WithOne(x => x.NameBasic)
                .HasForeignKey(e => e.NameId);
            modelBuilder.Entity<NameBasic>().HasMany(a => a.Plays).WithOne(x => x.NameBasic)
                .HasForeignKey(e => e.NameId);
            modelBuilder.Entity<NameBasic>().HasMany(a => a.KnownForTitles).WithOne(x => x.NameBasic)
                .HasForeignKey(e => e.NameId);


            modelBuilder.Entity<TitlePrincipal>().ToTable("title_principals");
            modelBuilder.Entity<TitlePrincipal>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<TitlePrincipal>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<TitlePrincipal>().Property(x => x.NameId).HasColumnName("name_id");
            modelBuilder.Entity<TitlePrincipal>().Property(x => x.Category).HasColumnName("category");
            modelBuilder.Entity<TitlePrincipal>().Property(x => x.Job).HasColumnName("job");
            modelBuilder.Entity<TitlePrincipal>().HasKey(c => new { c.TitleId, c.NameId, c.Ordering });


            modelBuilder.Entity<TitleGenre>().ToTable("title_genre");
            modelBuilder.Entity<TitleGenre>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<TitleGenre>().Property(x => x.Genre).HasColumnName("genre");
            modelBuilder.Entity<TitleGenre>().HasKey(c => new { c.TitleId, c.Genre });


            modelBuilder.Entity<Wi>().ToTable("wi2");
            modelBuilder.Entity<Wi>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Wi>().Property(x => x.Word).HasColumnName("word");
            modelBuilder.Entity<Wi>().Property(x => x.Field).HasColumnName("field");
            modelBuilder.Entity<Wi>().Property(x => x.Lexeme).HasColumnName("lexeme");
            modelBuilder.Entity<Wi>().HasKey(c => new { c.TitleId, c.Word, c.Field });


            modelBuilder.Entity<TitleRating>().ToTable("title_ratings");
            modelBuilder.Entity<TitleRating>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<TitleRating>().Property(x => x.AvgRating).HasColumnName("avg_rating");
            modelBuilder.Entity<TitleRating>().Property(x => x.NumVotes).HasColumnName("num_votes");
            modelBuilder.Entity<TitleRating>().HasKey(c => new { c.TitleId });
            


            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName("password");
            modelBuilder.Entity<User>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<User>().Property(x => x.Salt).HasColumnName("salt");
            modelBuilder.Entity<User>().HasKey(c => new { c.Username });


            modelBuilder.Entity<Plays>().ToTable("plays");
            modelBuilder.Entity<Plays>().Property(x => x.NameId).HasColumnName("name_id");
            modelBuilder.Entity<Plays>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Plays>().Property(x => x.Character).HasColumnName("character");
            modelBuilder.Entity<Plays>().HasKey(c => new { c.TitleId, c.NameId, c.Character });


            modelBuilder.Entity<Profession>().ToTable("profession");
            modelBuilder.Entity<Profession>().Property(x => x.NameId).HasColumnName("name_id");
            modelBuilder.Entity<Profession>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<Profession>().Property(x => x.ProfessionName).HasColumnName("profession");
            modelBuilder.Entity<Profession>().HasKey(c => new { c.NameId, c.Ordering });


            modelBuilder.Entity<RatingHistory>().ToTable("rating_history");
            modelBuilder.Entity<RatingHistory>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<RatingHistory>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<RatingHistory>().Property(x => x.Rating).HasColumnName("rating");
            modelBuilder.Entity<RatingHistory>().Property(x => x.TimeStamp).HasColumnName("time_stamp");
            modelBuilder.Entity<RatingHistory>().HasKey(c => new { c.TitleId, c.Username });
            modelBuilder.Entity<RatingHistory>().HasOne<TitleBasic>(s => s.TitleBasic).WithMany(g => g.RatingHistories)
            .HasForeignKey(s => s.TitleId);


            modelBuilder.Entity<SearchHistory>().ToTable("search_history");
            modelBuilder.Entity<SearchHistory>().Property(x => x.Username).HasColumnName("username");
            modelBuilder.Entity<SearchHistory>().Property(x => x.SearchInput).HasColumnName("search_input");
            modelBuilder.Entity<SearchHistory>().Property(x => x.TimeStamp).HasColumnName("time_stamp");
            modelBuilder.Entity<SearchHistory>().HasKey(c => new { c.Username, c.TimeStamp });
            modelBuilder.Entity<SearchHistory>().HasOne<User>(s => s.User).WithMany(g => g.SearchHistories)
            .HasForeignKey(s => s.Username);
        }
    }
}
