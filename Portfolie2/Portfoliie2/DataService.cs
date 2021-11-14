using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portfolie2.Domain;
using Portfolie2;

namespace Portfolie2
{
    public interface IDataService
    {

        // Bookmark
        public Bookmark GetBookmark(string username);
        public bool DeleteBookMark(string titleId);
        public bool CreateBookMark(Bookmark titleId);
        public Bookmark CreateBookMark(string titleId);

        // Comments
        public Comment GetComment(string username, string titleId);
        // public IList<Comment> GetCommentByTitleId(string username, string titleId);
        public bool CreateComment(Comment comment);
        public Comment CreateComment(string username, string titleId, string content);
        public bool UpdateComment(Comment comment);
        public bool DeleteComment(string username, string titleId);


        //RatingHistory
        public RatingHistory GetRatingHistory(string username, string titleId);
        public bool DeleteRatingHistory(string username, string titleId);
        public bool CreateRatingHistory(RatingHistory history);
        public RatingHistory CreateRatingHistory(string username, string titleId, int rating);


        //Users
        public User GetUser(string username);
        public User CreateUser(string name, string username, string password = null, string salt = null);
        public User GetUser(int id);

        //Award CRUD
        public Award GetAward(string titleId, string award);
        public IList<Award> GetAwardsByTitleId(string titleId);
        public bool CreateAward(Award award);
        public Award CreateAward(string titleId, string awardName);
        public bool UpdateAward(Award award);
        public bool DeleteAward(string titleId, string award);

        //TitleAkas CRUD
        public IList<TitleAka> GetTitleAkasByTitleId(string titleId);
        public TitleAka GetTitleAka(string titleId, int ordering);
        public bool CreateTitleAka(TitleAka titleAka);
        //WIP   public TitleAka CreateTitleAka(string titleid, int ordering);
        public bool UpdateTitleAka(TitleAka titleAka);
        public bool DeleteTitleAka(string titleId, int ordering);

        //Plays CRUD
        public Plays GetPlays(string titleId, string nameId, string character);
        public IList<Plays> GetPlaysByNameId(string nameId);
        public bool CreatePlays(Plays plays);
        public Plays CreatePlays(string nameId, string titleId, string character);
        public bool UpdatePlays(Plays plays);
        public bool DeletePlays(string titleId, string nameId, string character);


        //KnownForTitles CRUD
        public KnownForTitle GetKnownForTitle(string titleId, string nameId);
        public bool CreateKnownForTitle(KnownForTitle knownForTitle);
        public KnownForTitle CreateKnownForTitle(string nameId, string titleId);
        public bool UpdateKnownForTitle(KnownForTitle knownForTitle);
        public bool DeleteKnownForTitle(string titleId, string nameId);

        //Profession
        public Profession GetProfession(string nameId, int ordering);
        public IList<Profession> GetProfessionsByNameId(string nameId);
        public bool CreateProfession(Profession ordering);
        public Profession CreateProfession(string nameId, string professionName);
        public bool UpdateProfession(Profession profession);
        public bool DeleteProfession(string nameId, int ordering);

        //titleGenre CRUD
        public TitleGenre GetTitleGenre(string titleId, string genre);
        public IList<TitleGenre> GetTitleGenresByTitleId (string titleId);
        public bool CreateTitleGenre(TitleGenre genre);
        public TitleGenre CreateTitleGenre(string titleId, string genre);
        public bool UpdateTitleGenre(TitleGenre genre);
        public bool DeleteTitleGenre(string titleId, string genre);

        //titlePrincipal CRUD
        public TitlePrincipal GetTitlePrincipal(string titleId, int ordering, string nameId);
        public IList<TitlePrincipal> GetTitlePrincipalsByTitleId(string titleId);
        public bool CreateTitlePrincipal(TitlePrincipal titlePrincipal);
        public TitlePrincipal CreateTitlePrincipal(string titleId, string nameId, string category, string job);
        public bool UpdateTitlePrincipal(TitlePrincipal titlePrincipal);
        public bool DeleteTitlePrincipal(string titleId, int ordering, string nameId);

        //titleEpisode CRUD
        public TitleEpisode GetTitleEpisode(string id);
        public IList<TitleEpisode> GetTitleEpisodesByParentTitleId(string parentTitleId, QueryString queryString);          
        public bool CreateTitleEpisode(TitleEpisode titleEpisode);
        public TitleEpisode CreateTitleEpisode(string id, string parentTitleId, int seasonNumber, int episodeNumber);
        public bool UpdateTitleEpisode(TitleEpisode titleEpisode);
        public bool DeleteTitleEpisode(string titleId);
        int NumberOfEpisodes(IList<TitleEpisode> episodes);
        public IList<TitleEpisode> GetTitleEpisodesByParentTitleId(string parentTitleId);

        //TitleBasics CRUD
        public TitleBasic GetTitleBasic(string titleId);
        public bool CreateTitleBasic(TitleBasic titleBasic);
        public TitleBasic CreateTitleBasic(string id, string primarytitle, bool isadult);
        public bool UpdateTitleBasic(TitleBasic titleBasic);
        public bool DeleteTitleBasic(string titleId);

        //NameBasic CRUD
        public NameBasic GetNameBasic(string nameId);
        public bool CreateNameBasic(NameBasic nameBasic);
        public NameBasic CreateNameBasic(string nameId, string primaryName);
        public bool UpdateNameBasic(NameBasic nameBasic);
        public bool DeleteNameBasic(string nameId);


    }

    public class DataService : IDataService
    {

        //---------------------------Bookmark ----------------------------------\\


        public Bookmark GetBookmark(string userName)
        {
            var ctx = new IMDBContext();
            Bookmark result = ctx.Bookmarks.FirstOrDefault(x => x.Username == userName);
            return result;
        }
        public bool DeleteBookMark(string titleId)
        {
            var ctx = new IMDBContext();
            try
            {
                //Need a user aswell
                ctx.Bookmarks.Remove(ctx.Bookmarks.Find(titleId));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return ctx.SaveChanges() > 0;
        }
        public bool CreateBookMark(Bookmark bookmark)
        {
            var ctx = new IMDBContext();

            bookmark.TitleId = ctx.Bookmarks.Max(x => x.TitleId) + 1;
            ctx.Add(bookmark);
            return ctx.SaveChanges() > 0;
        }
        public Bookmark CreateBookMark(string titleId)
        {
            var ctx = new IMDBContext();

            Bookmark bookmark = new Bookmark();
            bookmark.TitleId = titleId;

            ctx.Add(bookmark);
            ctx.SaveChanges();

            return bookmark;
        }

        //---------------------------Comment ----------------------------------\\

        public Comment GetComment(string username, string titleId)
        {
            var ctx = new IMDBContext();
            Comment result = ctx.Comments.FirstOrDefault(x => x.TitleId == titleId && x.Username == username);
            return result;
        }
        /*
            public Comment GetComment(string username, string titleId)
            {
                var ctx = new IMDBContext();

                var comment = ctx.Comments
                           .Include(x => x.TitleBasic)
                           .Where(p => p.TitleId == titleId)
                           .ToList();

                return comment;
            }
        */

        public bool CreateComment(Comment comment)
        {
            var ctx = new IMDBContext();
            ctx.Add(comment);
            return ctx.SaveChanges() > 0;
        }

        public Comment CreateComment(string username, string titleId, string content)
        {
            var ctx = new IMDBContext();

            Comment comment = new Comment();
            comment.TitleId = titleId;
            comment.Username = username;
            comment.Content = content;

            ctx.Add(comment);
            ctx.SaveChanges();

            return comment;
        }

        public bool UpdateComment(Comment comment)
        {
            var ctx = new IMDBContext();
            Comment temp = ctx.Comments.Find(comment.TitleId, comment.Username);

            temp.Content = comment.Content;
            return ctx.SaveChanges() > 0;
        }

        public bool DeleteComment(string username, string titleId)
        {
            var ctx = new IMDBContext();

            Comment comment = new Comment() { TitleId = titleId, Username = username };
            ctx.Comments.Attach(comment);
            ctx.Comments.Remove(ctx.Comments.Find(titleId, username));

            return ctx.SaveChanges() > 0;
        }

        //---------------------------RatingHistory ----------------------------------\\


        public RatingHistory GetRatingHistory(string username, string titleId)
        {
            var ctx = new IMDBContext();
            RatingHistory result = ctx.RatingHistorys.FirstOrDefault(x => x.Username == username && x.TitleId == titleId);
            return result;
        }

        public bool DeleteRatingHistory(string username, string titleId)
        {
            var ctx = new IMDBContext();

            RatingHistory ratingHistory = new RatingHistory() { Username = username, TitleId = titleId };
            ctx.RatingHistorys.Attach(ratingHistory);
            ctx.RatingHistorys.Remove(ctx.RatingHistorys.Find(username, titleId));

            return ctx.SaveChanges() > 0;
        }
        public bool CreateRatingHistory(RatingHistory history) {

            var ctx = new IMDBContext();

            ctx.Add(history);
            return ctx.SaveChanges() > 0;
        }
        public RatingHistory CreateRatingHistory(string username, string titleid, int rating) {
            
            var ctx = new IMDBContext();

            RatingHistory ratingHistory = new RatingHistory();
           
            ratingHistory.Username = username;
            ratingHistory.TitleId = titleid;
            ratingHistory.Rating = rating;
            ctx.Add(ratingHistory);
            ctx.SaveChanges();

            return ratingHistory;
        }

        //---------------------------Users ----------------------------------\\

        public User GetUser(string username)
        {
            var ctx = new IMDBContext();

            User result = ctx.Users.FirstOrDefault(x => x.Username == username);

            return result;

        }

        public User GetUser(int id)
        {
            var ctx = new IMDBContext();

            User result = ctx.Users.FirstOrDefault(x => x.Id == id);
            return result;

        }
        public User CreateUser(string name, string username, string password = null, string salt = null)
        {
            // Test meget vigtig
            var ctx = new IMDBContext();
            User user = new User();
            user.Name = name;
            user.Username = username;
            user.Password = password;
            user.Salt = salt;

            user.Id = ctx.Users
                .Where(x => x.Id == user.Id)
                .Max(x => x.Id) + 1; ;
           
            ctx.Add(user);
            ctx.SaveChanges();

            return user;
            /*
            User result = ctx.Users
                .FirstOrDefault(
                x => x.Username == username &&
                x.Name == name &&
                x.Password == password &&
                x.Salt == salt)
                .Max(x => x.id) + 1;

          

            User user = new User();
            //user.Name = name;
            user.Username = username;

            ctx.Add(user);
            ctx.SaveChanges();

            return user;
            */

           
        }

        //---------------------------Award CRUD----------------------------------
        public Award GetAward(string titleId, string awardName) {
            var ctx = new IMDBContext();
            Award result = ctx.Awards.FirstOrDefault(x => x.TitleId == titleId && x.AwardName == awardName);

            /*result = result
                .Skip(queryString.Page * queryString.PageSize)
                .Take(queryString.PageSize); 
            */

            return result;
        }

        public IList<Award> GetAwardsByTitleId(string titleId)
        {
            var ctx = new IMDBContext();

            var awards = ctx.Awards
                       .Include(x => x.TitleBasic)
                       .Where(p => p.TitleId == titleId)
                       .ToList();

            return awards;
        }

        public bool CreateAward(Award award)
        {
            var ctx = new IMDBContext();
            ctx.Add(award);
            return ctx.SaveChanges() > 0;
        }

        public Award CreateAward(string titleId, string awardName)
        {
            var ctx = new IMDBContext();

            Award award = new Award();
            award.TitleId = titleId;
            award.AwardName = awardName;

            ctx.Add(award);
            ctx.SaveChanges();

            return award;
        }

        public bool UpdateAward(Award award)
        {
            var ctx = new IMDBContext();
            Award temp = ctx.Awards.Find(award.TitleId, award.AwardName);

            temp.AwardName = award.AwardName;
            return ctx.SaveChanges() > 0;
        }

        public bool DeleteAward(string titleId, string awardName)
        {
            var ctx = new IMDBContext();

            Award award = new Award() { TitleId = titleId, AwardName = awardName };
            ctx.Awards.Attach(award);
            ctx.Awards.Remove(ctx.Awards.Find(titleId, awardName));

            return ctx.SaveChanges() > 0;
        }


        //---------------------------Title Aka CRUD----------------------------------
        public TitleAka GetTitleAka(string titleId, int ordering)
        {
            var ctx = new IMDBContext();
            TitleAka result = ctx.TitleAkas.FirstOrDefault(x => x.TitleId == titleId && x.Ordering.Equals(ordering));
            return result;
        }

        public IList<TitleAka> GetTitleAkasByTitleId(string titleId)
        {
            var ctx = new IMDBContext();

            var titleAkas = ctx.TitleAkas
                       .Include(x => x.TitleBasic)
                       .Where(p => p.TitleId == titleId)
                       .ToList();
            return titleAkas;
        }

        public bool CreateTitleAka(TitleAka titleAka)
        {
            var ctx = new IMDBContext();
            //FIX THIS
            titleAka.Ordering = ctx.TitleAkas
                    .Where(t => t.TitleId == titleAka.TitleId && t.Ordering == titleAka.Ordering)
                    .Max(x => x.Ordering) + 1;

            ctx.Add(titleAka);
            return ctx.SaveChanges() > 0;
        }

        public bool UpdateTitleAka(TitleAka titleAka)
        {
            var ctx = new IMDBContext();
            TitleAka temp = ctx.TitleAkas.Find(titleAka.TitleId, titleAka.Ordering);

            temp.Title = titleAka.Title;
            temp.Region = titleAka.Region;
            temp.Language = titleAka.Language;
            temp.Types = titleAka.Types;
            temp.Attributes = titleAka.Attributes;
            temp.IsOriginalTitle = titleAka.IsOriginalTitle;
            
            return ctx.SaveChanges() > 0;
        }

        public bool DeleteTitleAka(string titleId, int ordering)
        {
            var ctx = new IMDBContext();

            TitleAka titleAka = new TitleAka() { TitleId = titleId, Ordering = ordering};
            ctx.TitleAkas.Attach(titleAka);
            ctx.TitleAkas.Remove(ctx.TitleAkas.Find(titleId, ordering));

            return ctx.SaveChanges() > 0;
        }


        //---------------------------Plays CRUD-------------------------------------
        public Plays GetPlays(string titleId, string nameId, string character)
        {
            var ctx = new IMDBContext();
            Plays result = ctx.Plays.FirstOrDefault(x => x.TitleId == titleId && x.NameId == nameId && x.Character == character);
            return result;
        }

        public IList<Plays> GetPlaysByNameId(string nameId)
        {
            var ctx = new IMDBContext();

            var plays = ctx.Plays
                       .Include(x => x.NameBasic)
                       .Where(p => p.NameId == nameId)
                       .ToList();
            return plays;
        }

        public bool CreatePlays(Plays plays)
        {
            var ctx = new IMDBContext();
           
            ctx.Add(plays);
            return ctx.SaveChanges() > 0;
        }

        public Plays CreatePlays(string nameId, string titleId, string character)
        {
            var ctx = new IMDBContext();

            Plays plays = new Plays();
            plays.NameId = nameId;
            plays.TitleId = titleId;
            plays.Character = character;

            ctx.Add(plays);
            ctx.SaveChanges();

            return plays;
        }


        public bool UpdatePlays(Plays plays)
        {
            var ctx = new IMDBContext();
            Plays temp = ctx.Plays.Find(plays.TitleId, plays.NameId, plays.Character);

            temp.TitleId = plays.TitleId;
            temp.NameId = plays.NameId;
            temp.Character = plays.Character; ;
            return ctx.SaveChanges() > 0;
        }

        public bool DeletePlays(string titleId, string nameId, string character)
        {
            var ctx = new IMDBContext();

            Plays plays = new Plays() { TitleId = titleId, NameId = nameId, Character = character };
            ctx.Plays.Attach(plays);
            ctx.Plays.Remove(ctx.Plays.Find(titleId, nameId, character));
 
            return ctx.SaveChanges() > 0;
        }


        //---------------------------Known For Title CRUD-----------------------------
        public KnownForTitle GetKnownForTitle(string titleId, string nameId)
        {
            var ctx = new IMDBContext();
            KnownForTitle result = ctx.KnownForTitles.FirstOrDefault(x => x.TitleId == titleId && x.NameId == nameId);
            return result;
        }

        public bool CreateKnownForTitle(KnownForTitle knownForTitle)
        {
            var ctx = new IMDBContext();
            
            ctx.Add(knownForTitle);
            return ctx.SaveChanges() > 0;
        }

        public KnownForTitle CreateKnownForTitle(string nameId, string titleId)
        {
            var ctx = new IMDBContext();

            KnownForTitle knownForTitle = new KnownForTitle();
            knownForTitle.TitleId = titleId;
            knownForTitle.NameId = nameId;

            ctx.Add(knownForTitle);
            ctx.SaveChanges();

            return knownForTitle;
        }

        public bool UpdateKnownForTitle(KnownForTitle knownForTitle)
        {
            var ctx = new IMDBContext();
            KnownForTitle temp = ctx.KnownForTitles.Find(knownForTitle.TitleId, knownForTitle.NameId);

            temp.TitleId = knownForTitle.TitleId;
            temp.NameId = knownForTitle.NameId;
            return ctx.SaveChanges() > 0;
        }

        public bool DeleteKnownForTitle(string titleId, string nameId)
        {
            var ctx = new IMDBContext();

            KnownForTitle knownForTitle = new KnownForTitle() { TitleId = titleId, NameId = nameId};
            ctx.KnownForTitles.Attach(knownForTitle);
            ctx.KnownForTitles.Remove(ctx.KnownForTitles.Find(titleId, nameId));

            return ctx.SaveChanges() > 0;
        }



        //---------------------------Profession CRUD-----------------------------------------
        public Profession GetProfession(string nameId, int ordering)
        {
            var ctx = new IMDBContext();
            Profession result = ctx.Professions.FirstOrDefault(x => x.NameId == nameId && x.Ordering.Equals(ordering));
            return result;
        }


        public IList<Profession> GetProfessionsByNameId(string nameId)
        {
            var ctx = new IMDBContext();

            var professions = ctx.Professions
                       .Include(x => x.NameBasic)
                       .Where(p => p.NameId == nameId)
                       .ToList();
            return professions;
        }



        public bool CreateProfession(Profession profession)
        {
            var ctx = new IMDBContext();
            //FIX THIS
            profession.Ordering = ctx.Professions
                .Where(p => p.NameId == profession.NameId && p.Ordering == profession.Ordering)
                .Max(x => x.Ordering) + 1;

            ctx.Add(profession);

            return ctx.SaveChanges() > 0;
        }

        public Profession CreateProfession(string nameId, string professionName)
        {
            var ctx = new IMDBContext();

            Profession profession = new Profession();
            profession.NameId = nameId;
            profession.Ordering = ctx.Professions
                .Where(p => p.NameId == profession.NameId && p.Ordering == profession.Ordering)
                .Max(x => x.Ordering) + 1;

            profession.ProfessionName = professionName;

            ctx.Add(profession);
            ctx.SaveChanges();

            return profession;
        }

        public bool UpdateProfession(Profession profession)
        {
            var ctx = new IMDBContext();
            Profession temp = ctx.Professions.Find(profession.NameId, profession.Ordering);

            temp.ProfessionName = profession.ProfessionName;
            return ctx.SaveChanges() > 0;
        }

        public bool DeleteProfession(string nameId, int ordering)
        {
            var ctx = new IMDBContext();

            Profession profession = new Profession() {NameId = nameId, Ordering = ordering };
            ctx.Professions.Attach(profession);
            ctx.Professions.Remove(ctx.Professions.Find(nameId, ordering));

            return ctx.SaveChanges() > 0;
        }


        //--------------------------Title Genre CRUD-----------------------------------
        public TitleGenre GetTitleGenre(string titleId, string genre)
        {
            var ctx = new IMDBContext();
            TitleGenre result = ctx.TitleGenres.FirstOrDefault(x => x.TitleId == titleId && x.Genre == genre);
            return result;
        }


        public IList<TitleGenre> GetTitleGenresByTitleId(string titleId)
        {
            var ctx = new IMDBContext();

            var titleGenres = ctx.TitleGenres
                       .Include(x => x.TitleBasics)
                       .Where(y => y.TitleId == titleId)
                       .ToList();

            return titleGenres;
        }


        public bool CreateTitleGenre(TitleGenre titleGenre)
        {
            var ctx = new IMDBContext();
            
            ctx.Add(titleGenre);
            return ctx.SaveChanges() > 0;
        }

        public TitleGenre CreateTitleGenre(string titleId, string genre)
        {
            var ctx = new IMDBContext();

            TitleGenre titlegenre = new TitleGenre();
            titlegenre.TitleId = titleId;
            titlegenre.Genre = genre;

            ctx.Add(titlegenre);
            ctx.SaveChanges();

            return titlegenre;
        }

        public bool UpdateTitleGenre(TitleGenre titleGenre)
        {
            var ctx = new IMDBContext();
            TitleGenre temp = ctx.TitleGenres.Find(titleGenre.TitleId, titleGenre.Genre);

            temp.Genre = titleGenre.Genre;
            return ctx.SaveChanges() > 0;
        }

        public bool DeleteTitleGenre(string titleId, string genre)
        {
            var ctx = new IMDBContext();

            TitleGenre titleGenre = new TitleGenre() { TitleId = titleId, Genre = genre };
            ctx.TitleGenres.Attach(titleGenre);
            ctx.TitleGenres.Remove(ctx.TitleGenres.Find(titleId, genre));

            return ctx.SaveChanges() > 0;
        }


        //----------------------------Title Principals CRUD--------------------------
        public TitlePrincipal GetTitlePrincipal(string titleId, int ordering, string nameId)
        {
            var ctx = new IMDBContext();
            TitlePrincipal result = ctx.TitlePrincipals.FirstOrDefault(x => x.TitleId == titleId && x.Ordering == ordering && x.NameId == nameId);
            return result;
        }

        public IList<TitlePrincipal> GetTitlePrincipalsByTitleId(string titleId)
        {
            var ctx = new IMDBContext();

            var titlePrincipals = ctx.TitlePrincipals
                       .Include(x => x.TitleBasic)
                       .Where(p => p.TitleId == titleId)
                       .ToList();

            return titlePrincipals;
        }

        public bool CreateTitlePrincipal(TitlePrincipal titlePrincipal)
        {
            var ctx = new IMDBContext();
      
            titlePrincipal.Ordering = ctx.TitlePrincipals
               .Where(p => p.NameId == titlePrincipal.NameId && p.Ordering == titlePrincipal.Ordering && p.TitleId == titlePrincipal.TitleId)
               .Max(x => x.Ordering) + 1;

            ctx.Add(titlePrincipal);
            return ctx.SaveChanges() > 0;
        }

        public TitlePrincipal CreateTitlePrincipal(string titleId, string nameId, string category, string job)
        {
            var ctx = new IMDBContext();

            TitlePrincipal titlePrincipal = new TitlePrincipal();
            titlePrincipal.TitleId = titleId;

            titlePrincipal.Ordering = ctx.TitlePrincipals
               .Where(p => p.NameId == titlePrincipal.NameId && p.Ordering == titlePrincipal.Ordering && p.TitleId == titlePrincipal.TitleId)
               .Max(x => x.Ordering) + 1;

            titlePrincipal.NameId = nameId;
            titlePrincipal.Category = category;
            titlePrincipal.Job = job;

            ctx.Add(titlePrincipal);
            ctx.SaveChanges();

            return titlePrincipal;
        }

        public bool UpdateTitlePrincipal(TitlePrincipal titlePrincipal)
        {
            var ctx = new IMDBContext();
            TitlePrincipal temp = ctx.TitlePrincipals.Find(titlePrincipal.TitleId, titlePrincipal.Ordering, titlePrincipal.NameId);

            temp.Ordering = titlePrincipal.Ordering;
            temp.NameId = titlePrincipal.NameId;
            temp.Category = titlePrincipal.Category;
            temp.Job = titlePrincipal.Job;

            return ctx.SaveChanges() > 0;
        }

        public bool DeleteTitlePrincipal(string titleId, int ordering, string nameId)
        {
            var ctx = new IMDBContext();

            TitlePrincipal titlePrincipal = new TitlePrincipal() { TitleId = titleId, Ordering = ordering, NameId = nameId };
            ctx.TitlePrincipals.Attach(titlePrincipal);
            ctx.TitlePrincipals.Remove(ctx.TitlePrincipals.Find(titleId, ordering, nameId));

            return ctx.SaveChanges() > 0;
        }

        //-------------------------------Title Episode CRUD--------------------------------
        public TitleEpisode GetTitleEpisode(string titleId)
        {
            var ctx = new IMDBContext();
            TitleEpisode result = ctx.TitleEpisodes.FirstOrDefault(x => x.Id == titleId);
            return result;
        }
        public int NumberOfEpisodes(IList<TitleEpisode> episodes)
        {
            return episodes.Count();
        }


        public IList<TitleEpisode> GetTitleEpisodesByParentTitleId(string parentTitleId, QueryString queryString)
        {
            var ctx = new IMDBContext();
            var result = ctx.TitleEpisodes
            .Where(p => p.ParentTitleId == parentTitleId)
            .Include(x=>x.TitleBasic).AsEnumerable()
            ;
            result = result
                .Skip(queryString.Page * queryString.PageSize)
                .Take(queryString.PageSize);
            return result.ToList();
        }
        public IList<TitleEpisode> GetTitleEpisodesByParentTitleId(string parentTitleId)
        {
            var ctx = new IMDBContext();
            var titleEpisode = ctx.TitleEpisodes
            .Where(p => p.ParentTitleId == parentTitleId)
            .Include(x => x.TitleBasic)
            .ToList()
            ;

            return titleEpisode;   
        }

        public bool CreateTitleEpisode(TitleEpisode titleEpisode)
        {
            var ctx = new IMDBContext();
           
            ctx.Add(titleEpisode);
            return ctx.SaveChanges() > 0;
        }

        public TitleEpisode CreateTitleEpisode(string id, string parentTitleId, int seasonNumber, int episodeNumber)
        {
            var ctx = new IMDBContext();

            TitleEpisode titleEpisode = new TitleEpisode();
            titleEpisode.Id = id;
            titleEpisode.ParentTitleId = parentTitleId;
            titleEpisode.SeasonNumber = seasonNumber;
            titleEpisode.EpisodeNumber = episodeNumber;

            ctx.Add(titleEpisode);
            ctx.SaveChanges();

            return titleEpisode;
        }

        public bool UpdateTitleEpisode(TitleEpisode titleEpisode)
        {
            var ctx = new IMDBContext();
            TitleEpisode temp = ctx.TitleEpisodes.Find(titleEpisode.Id);

            temp.ParentTitleId = titleEpisode.ParentTitleId;
            temp.SeasonNumber = titleEpisode.SeasonNumber;
            temp.EpisodeNumber = titleEpisode.EpisodeNumber;
            return ctx.SaveChanges() > 0;
        }

        public bool DeleteTitleEpisode(string titleId)
        {
            var ctx = new IMDBContext();

            TitleEpisode titleEpisode = new TitleEpisode() { Id = titleId };
            ctx.TitleEpisodes.Attach(titleEpisode);
            ctx.TitleEpisodes.Remove(ctx.TitleEpisodes.Find(titleId));

            return ctx.SaveChanges() > 0;
        }









        //---------------------------Title Basic CRUD----------------------------------
        public TitleBasic GetTitleBasic(string titleId)
        {
            var ctx = new IMDBContext();
            TitleBasic result = ctx.TitleBasics.FirstOrDefault(x => x.Id == titleId);
            return result;
        }

        public bool CreateTitleBasic(TitleBasic titleBasic)
        {
            var ctx = new IMDBContext();
            //FIX THIS
            titleBasic.Id = ctx.TitleBasics.Max(x => x.Id) + 1;
            ctx.Add(titleBasic);
            return ctx.SaveChanges() > 0;
        }

        public TitleBasic CreateTitleBasic(string id, string primarytitle, bool isadult)
        {
            var ctx = new IMDBContext();

            TitleBasic titlebasic = new TitleBasic();
            titlebasic.Id = id;
            titlebasic.PrimaryTitle = primarytitle;
            titlebasic.IsAdult = isadult;

            ctx.Add(titlebasic);
            ctx.SaveChanges();

            return titlebasic;
        }

        public bool UpdateTitleBasic(TitleBasic titleBasic)
        {
            var ctx = new IMDBContext();
            TitleBasic temp = ctx.TitleBasics.Find(titleBasic.Id);

            temp.TitleType = titleBasic.TitleType;
            temp.PrimaryTitle = titleBasic.PrimaryTitle;
            temp.OriginalTitle = titleBasic.OriginalTitle;
            temp.IsAdult = titleBasic.IsAdult;
            temp.StartYear = titleBasic.StartYear;
            temp.EndYear = titleBasic.EndYear;
            temp.Runtime = titleBasic.Runtime;
            temp.Plot = titleBasic.Plot;
            temp.Poster = titleBasic.Poster;
            return ctx.SaveChanges() > 0;
        }

        public bool DeleteTitleBasic(string titleId)
        {
            var ctx = new IMDBContext();
          
            TitleBasic titlebasic = new TitleBasic() { Id = titleId };
            ctx.TitleBasics.Attach(titlebasic);
            ctx.TitleBasics.Remove(ctx.TitleBasics.Find(titleId));
            //ctx.SaveChanges();

            return ctx.SaveChanges() > 0;
        }

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Name CRUD!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public NameBasic GetNameBasic(string nameId)
        {
            var ctx = new IMDBContext();
            NameBasic result = ctx.NameBasics.FirstOrDefault(x => x.Id == nameId);
            return result;
        }

        public bool CreateNameBasic(NameBasic nameBasic)
        {
            var ctx = new IMDBContext();
            //FIX THIS
            nameBasic.Id = ctx.NameBasics.Max(x => x.Id) + 1;
            ctx.Add(nameBasic);
            return ctx.SaveChanges() > 0;
        }

        public NameBasic CreateNameBasic(string nameId, string primaryName)
        {
            var ctx = new IMDBContext();

            NameBasic namebasic = new NameBasic();
            namebasic.Id = nameId;
            namebasic.PrimaryName = primaryName;

            ctx.Add(namebasic);
            ctx.SaveChanges();

            return namebasic;
        }

        public bool UpdateNameBasic(NameBasic nameBasic)
        {
            var ctx = new IMDBContext();
            NameBasic temp = ctx.NameBasics.Find(nameBasic.Id);

            temp.Id = nameBasic.Id;
            temp.PrimaryName = nameBasic.PrimaryName;
            temp.BirthYear = nameBasic.BirthYear;
            temp.DeathYear = nameBasic.DeathYear;
            temp.Rating = nameBasic.Rating;
            return ctx.SaveChanges() > 0;
        }

        public bool DeleteNameBasic(string nameId)
        {
            var ctx = new IMDBContext();
            try
            {
                ctx.NameBasics.Remove(ctx.NameBasics.Find(nameId));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return ctx.SaveChanges() > 0;
        }

    }

}
