using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataServiceLib.Domain;
using DataServiceLib;

namespace DataServiceLib
{
    public class DataService : IDataService
    {
        //---------------------------TitleRating---------------------------
        public TitleRating GetTitleRating(string titleId) {
            var ctx = new IMDBContext();
            TitleRating result = ctx.TitleRatings
                .Include(x => x.TitleBasic)
                .FirstOrDefault(x => x.TitleId == titleId);
            return result;
        }

        //---------------------------SearchHistory--------------------------
        public SearchHistory GetSearchHistory(string input)
        {
            var ctx = new IMDBContext();
            SearchHistory result = ctx.SearchHistories.FirstOrDefault(x => x.SearchInput == input);
            return result;
        }

        public IList<SearchHistory> GetSearchHistoryByUsername(string username, QueryString queryString)
        {
            var ctx = new IMDBContext();

            var result = ctx.SearchHistories
                    .Where(p => p.Username == username)
                    .AsEnumerable();

            result = result
                .Skip(queryString.Page * queryString.PageSize)
                .Take(queryString.PageSize);
            return result.ToList();
        }

        public bool DeleteSearchHistory(string username)
        {
            var ctx = new IMDBContext();
            
            ctx.Database
                .ExecuteSqlInterpolated($"CALL clear_search_history({username})");

           // var temp = GetSearchHistory(username);
          //  ctx.SearchHistories.Attach(temp);
           // ctx.SearchHistories.Remove(ctx.SearchHistories.Find(username));

            return ctx.SaveChanges() > 0;
        }
        public bool CreateSearchHistory(SearchHistory searchHistory)
        {
                var ctx = new IMDBContext();
            /*
                searchHistory.TitleId = ctx.Bookmarks.Max(x => x.TitleId) + 1;
                ctx.Add(bookmark);
            */
                return ctx.SaveChanges() > 0;
        }


        //---------------------------Bookmark ----------------------------------\\


        public IList<Bookmark> GetBookmarks(string username, QueryString queryString)
        {
            var ctx = new IMDBContext();

            var result = ctx.Bookmarks
                    .Where(p => p.Username == username)
                    .Include(x => x.TitleBasic)
                    .AsEnumerable();
         
            result = result
                .Skip(queryString.Page * queryString.PageSize)
                .Take(queryString.PageSize);


            return result.ToList();
        }
        public int NumberOfElements(IList<Bookmark> elements)
        {
            return elements.Count();
        }
        public IList<Bookmark> GetBookmarks(string username)
        {
            var ctx = new IMDBContext();

            var result = ctx.Bookmarks
                    .Where(p => p.Username == username)
                    .Include(x => x.TitleBasic)
                    .AsEnumerable();

            return result.ToList();
        }

        public Bookmark GetBookmark(string username, string titleId) {
            var ctx = new IMDBContext();
            Bookmark result = ctx.Bookmarks.FirstOrDefault(x => x.Username == username && x.TitleId == titleId);
            return result;
        }

        public bool DeleteBookmark(string username, string titleId)
        {

            var ctx = new IMDBContext();

            Bookmark bookmark = new Bookmark() { Username = username, TitleId = titleId };
            ctx.Bookmarks.Attach(bookmark);
            ctx.Bookmarks.Remove(ctx.Bookmarks.Find(username, titleId));

            return ctx.SaveChanges() > 0;
        }
        public bool CreateBookmark(Bookmark bookmark)
        {
            var ctx = new IMDBContext();

            ctx.Add(bookmark);
            return ctx.SaveChanges() > 0;
        }
        public bool CreateBookmark(string titleId)
        {
            var ctx = new IMDBContext();

            Bookmark bookmark = new Bookmark
            {
                TitleId = titleId,
                Username = "tobias"
            };

            ctx.Add(bookmark);

            return ctx.SaveChanges() > 0;
        }
        public Bookmark CreateBookmark(string titleId,string username)
        {
            var ctx = new IMDBContext();

            Bookmark bookmark = new Bookmark
            {
                TitleId = titleId,
                Username = username
            };

            ctx.Add(bookmark);
            ctx.SaveChanges();

            return bookmark;
        }

        public Bookmark ToggleBookmark(string titleId)
        {
            var ctx = new IMDBContext();

            Bookmark bookmark = new Bookmark
            {
                TitleId = titleId,
                Username = "tobias"
            };

            var bookmarks = GetBookmarks("tobias");

            if (bookmarks.Contains(bookmark))
            {
                ctx.Remove(bookmark);
            }


            else
            {
                ctx.Add(bookmark);
                ctx.SaveChanges();
            }

            return bookmark;
        }

        //---------------------------Comment ----------------------------------\\

        public Comment GetComment(string username, string titleId, DateTime timeStamp)
        {
            var ctx = new IMDBContext();
            Comment result = ctx.Comments
                .Include(y => y.TitleBasic)
                .FirstOrDefault(x => x.TitleId == titleId && x.Username == username && x.TimeStamp == timeStamp);
            return result;
        }


        public IList<Comment> GetCommentsByTitleId(string titleId, QueryString queryString)
        {
            var ctx = new IMDBContext();

            var result = ctx.Comments
                    .Where(p => p.TitleId == titleId)
                    .Include(x => x.TitleBasic)
                    .AsEnumerable();
            result = result.OrderBy(x => x.TimeStamp);

            result = result
                .Skip(queryString.Page * queryString.PageSize)
                .Take(queryString.PageSize);

            return result.ToList();
        }
        public int NumberOfElements(IList<Comment> elements)
        {
            return elements.Count();
        }
        public IList<Comment> GetCommentsByTitleId(string titleId)
        {
            var ctx = new IMDBContext();

            var result = ctx.Comments
                    .Where(p => p.TitleId == titleId)
                    .Include(x => x.TitleBasic)
                    .AsEnumerable();

           
            return result.ToList();
        }

        public bool CreateComment(Comment comment)
        {
            var ctx = new IMDBContext();
            comment.TimeStamp = DateTime.Now;
            ctx.Add(comment);
            return ctx.SaveChanges() > 0;
        }

        public Comment CreateComment(string username, string titleId, string content, DateTime timestamp)
        {
            var ctx = new IMDBContext();

            Comment comment = new Comment();
            comment.TitleId = titleId;
            comment.Username = username;
            comment.Content = content;
            comment.TimeStamp = timestamp;

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

        public bool DeleteComment(string username, string titleId, DateTime timeStamp)
        {
            var ctx = new IMDBContext();

            Comment comment = new Comment() { TitleId = titleId, Username = username, TimeStamp = timeStamp };
            ctx.Comments.Attach(comment);
            ctx.Comments.Remove(ctx.Comments.Find(username, titleId, timeStamp));

            return ctx.SaveChanges() > 0;
        }

        public bool DeleteComment(Comment comment)
        {
            var ctx = new IMDBContext();

            ctx.Comments.Attach(comment);
            ctx.Comments.Remove(ctx.Comments.Find(comment.Username, comment.TitleId, comment.TimeStamp));

            return ctx.SaveChanges() > 0;
        }

        //---------------------------RatingHistory ----------------------------------\\


        public RatingHistory GetRatingHistory(string username, string titleId)
        {
            var ctx = new IMDBContext();
            RatingHistory result = ctx.RatingHistories.FirstOrDefault(x => x.Username == username && x.TitleId == titleId);
            return result;
        }

        public IList<RatingHistory> GetRatingHistoryByUsername(string username, QueryString queryString)
        {            
            var ctx = new IMDBContext();

            var result = ctx.RatingHistories
                    .Where(p => p.Username == username)
                    .Include(x => x.TitleBasic)
                    .AsEnumerable();

            result = result
                .Skip(queryString.Page * queryString.PageSize)
                .Take(queryString.PageSize);
            return result.ToList();
        }

        public bool DeleteRatingHistory(string username, string titleId)
        {
            var ctx = new IMDBContext();

            RatingHistory ratingHistory = new RatingHistory() { Username = username, TitleId = titleId };
            ctx.RatingHistories.Attach(ratingHistory);
            ctx.RatingHistories.Remove(ctx.RatingHistories.Find(username, titleId));

            return ctx.SaveChanges() > 0;
        }
        public bool CreateRatingHistory(RatingHistory history) {

            var ctx = new IMDBContext();
            history.TimeStamp = DateTime.Now;

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

      
        public User CreateUser( string username, string password = null, string salt = null)
        {
            // Test meget vigtig
            var ctx = new IMDBContext();
            User user = new User
            {
                Username = username,
                Password = password,
                Salt = salt
            };

            ctx.Add(user);
            ctx.SaveChanges();

            return user;
        }

        //---------------------------Award CRUD----------------------------------
        public Award GetAward(string titleId, string awardName) {
            var ctx = new IMDBContext();
            Award result = ctx.Awards.FirstOrDefault(x => x.TitleId == titleId && x.AwardName == awardName);

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
            temp.Type = titleAka.Type;
            temp.Attribute = titleAka.Attribute;
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
                       .Include(x => x.TitleBasic)
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
        public int NumberOfElements(IList<TitleEpisode> elements)
        {
            return elements.Count();
        }


        public IList<TitleEpisode> GetTitleEpisodesByParentTitleId(string parentTitleId, QueryString queryString)
        {
            var ctx = new IMDBContext();
            var result = ctx.TitleEpisodes
            .Where(p => p.ParentTitleId == parentTitleId)
            .Include(x=>x.TitleBasic).AsEnumerable();

            result = result.OrderBy(x => x.SeasonNumber).ThenBy(x => x.EpisodeNumber);

            result = result
                .Skip(queryString.Page * queryString.PageSize)
                .Take(queryString.PageSize);
            return result.ToList();
        }
        public IList<TitleEpisode> GetTitleEpisodesByParentTitleId(string parentTitleId)
        {
            var ctx = new IMDBContext();
            var titleEpisodes = ctx.TitleEpisodes
            .Where(p => p.ParentTitleId == parentTitleId)
            .Include(x => x.TitleBasic)
            .ToList();

            return titleEpisodes;   
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
            var trim = titleId.Trim();

            var ctx = new IMDBContext();
            TitleBasic result = ctx.TitleBasics
                .Include(x => x.TitleRating)
                .Include(x => x.Awards)
                //.Include(x => x.TitleAkas)
                //.Include(x => x.TitleGenres)
                //.Include(x => x.TitlePrincipals)
                .FirstOrDefault(x => x.Id == trim);
            return result;
        }

        public int NumberOfElements(IList<TitleBasic> elements)
        {
            return elements.Count();
        }


        public IList<TitleBasic> GetTitleBasicsBySearch(string searchInput)
        {
            var ctx = new IMDBContext();

            string[] searchWords = searchInput.Split(" ");
            var finalSearch = "'" + string.Join("', '", searchWords) + "'";



            var searchResult = ctx.TitleBasicSearchResults
                .FromSqlRaw("select * from bestmatch(" + finalSearch + ")");

            searchResult = searchResult.OrderBy(x => x.rank);
            IEnumerable<TitleBasic> result = new List<TitleBasic>();


            foreach (var TitleBasicSearchResult in searchResult)
            {
                var temp = GetTitleBasic(TitleBasicSearchResult.Id);
                result = result.Append(temp);
            }
            return result.ToList();
        }

        public IList<TitleBasic> GetTitleBasicsBySearch(string searchInput, QueryString queryString)
        {
            var ctx = new IMDBContext();

            string[] searchWords = searchInput.Split(" ");
            var finalSearch = "'" + string.Join("', '", searchWords) + "'";
     

            var searchResult = ctx.TitleBasicSearchResults
                .FromSqlRaw("select * from bestmatch(" + finalSearch+ ")");

            searchResult = searchResult.OrderByDescending(x => x.rank);
            IEnumerable<TitleBasic> result = new List<TitleBasic>();

            foreach (var TitleBasicSearchResult in searchResult)
            { 
                var temp = GetTitleBasic(TitleBasicSearchResult.Id);
                result = result.Append(temp);
            }


            result = result
                .Skip(queryString.Page * queryString.PageSize)
                .Take(queryString.PageSize);
            return result.ToList();
        }

        public bool CreateTitleBasic(TitleBasic titleBasic)
        {
            var ctx = new IMDBContext();

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

        //-------------------------NameBasic CRUD--------------------------
        public NameBasic GetNameBasic(string nameId)
        {
            var trim = nameId.Trim();

            var ctx = new IMDBContext();
            NameBasic result = ctx.NameBasics.FirstOrDefault(x => x.Id == trim);
            return result;
        }

        public IList<NameBasic> GetNameBasicsBySearch(string searchInput, QueryString queryString)
        {
            var ctx = new IMDBContext();

            var searchResult = ctx.NameBasicSearchResults
                .FromSqlInterpolated($"select * from name_search({searchInput})");

            IEnumerable<NameBasic> result = new List<NameBasic>();

            foreach (var NameBasicSearchResult in searchResult)
            {
                var temp = GetNameBasic(NameBasicSearchResult.Id);
                result = result.Append(temp);
            }


            result = result
                .Skip(queryString.Page * queryString.PageSize)
                .Take(queryString.PageSize);

            result = result.OrderByDescending(x => x.Rating);

            return result.ToList();
        }


        public IList<NameBasic> GetNameBasicsBySearch(string searchInput)
        {
            var ctx = new IMDBContext();

            var searchResult = ctx.NameBasicSearchResults
                .FromSqlInterpolated($"select * from name_search({searchInput})");

            
            IEnumerable<NameBasic> result = new List<NameBasic>();

            foreach (var NameBasicSearchResult in searchResult)
            {
                var temp = GetNameBasic(NameBasicSearchResult.Id);
                result = result.Append(temp);
            }

            return result.ToList();
        }

        public int NumberOfElements(IList<NameBasic> elements)
        {
            return elements.Count();
        }

        public bool CreateNameBasic(NameBasic nameBasic)
        {
            var ctx = new IMDBContext();

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
