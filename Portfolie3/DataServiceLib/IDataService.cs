using System;
using System.Collections.Generic;
using System.Text;
using DataServiceLib.Domain;

namespace DataServiceLib
{
    public interface IDataService
    {

        // Bookmark CRUD
        public IList<Bookmark> GetBookmarks(string username, QueryString queryString);
        public Bookmark GetBookmark(string username, string titleId);
        public bool DeleteBookmark(string username, string titleId);
        public Bookmark CreateBookmark(string username, string titleId);
        public int NumberOfElements(IList<Bookmark> elements);
        public IList<Bookmark> GetBookmarks(string username);
        public bool BookmarkedAlready(string username, string titleId);


        // Comments CRUD
        public Comment GetComment(string username, string titleId, DateTime timeStamp);
        public IList<Comment> GetCommentsByTitleId(string titleId, QueryString queryString);
        public bool CreateComment(Comment comment);
        public Comment CreateComment(string username, string titleId, string content, DateTime timeStamp);
        public bool UpdateComment(Comment comment);
        public bool DeleteComment(string username, string titleId, DateTime timeStamp);
        public bool DeleteComment(Comment comment);
        public int NumberOfElements(IList<Comment> elements);
        public IList<Comment> GetCommentsByTitleId(string titleId);


        //RatingHistory CRUD
        public RatingHistory GetRatingHistory(string username, string titleId);
        public IList<RatingHistory> GetRatingHistoryByUsername(string username, QueryString queryString);
        public bool DeleteRatingHistory(string username, string titleId);
        public bool CreateRatingHistory(RatingHistory history);
        public RatingHistory CreateRatingHistory(string username, string titleId, int rating);
        public bool UpdateRating(RatingHistory rating);


        // SearchHistory CRD
        public SearchHistory GetSearchHistory(string searchInput);
        public IList<SearchHistory> GetSearchHistoryByUsername(string username, QueryString queryString);
        public bool DeleteSearchHistory(string username);
        public bool CreateSearchHistory(SearchHistory searchHistory);

        //Users CR
        public User GetUser(string username);
        public User CreateUser(string username, string password = null, string salt = null);

        //Award  R
        public Award GetAward(string titleId, string award);
        public IList<Award> GetAwardsByTitleId(string titleId);

        //TitleAkas CRUD
        public IList<TitleAka> GetTitleAkasByTitleId(string titleId);
        public TitleAka GetTitleAka(string titleId, int ordering);
        public bool CreateTitleAka(TitleAka titleAka);
        public bool UpdateTitleAka(TitleAka titleAka);
        public bool DeleteTitleAka(string titleId, int ordering);

        //Plays  CRUD
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

        //Profession CRUD
        public Profession GetProfession(string nameId, int ordering);
        public IList<Profession> GetProfessionsByNameId(string nameId);
        public bool CreateProfession(Profession ordering);
        public Profession CreateProfession(string nameId, string professionName);
        public bool UpdateProfession(Profession profession);
        public bool DeleteProfession(string nameId, int ordering);

        //titleGenre CRUD
        public TitleGenre GetTitleGenre(string titleId, string genre);
        public IList<TitleGenre> GetTitleGenresByTitleId(string titleId);
        public IList<string> GetTitleGenresByTitleId1(string titleId);
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
        int NumberOfElements(IList<TitleEpisode> elements);
        int NumberOfElements(IList<TitleBasic> elements);
        public int NumberOfElements(IList<NameBasic> elements);
        public IList<TitleEpisode> GetTitleEpisodesByParentTitleId(string parentTitleId);

        //TitleBasics CRUD
        public TitleBasic GetTitleBasic(string titleId);
        public bool CreateTitleBasic(TitleBasic titleBasic);
        public TitleBasic CreateTitleBasic(string id, string primarytitle, bool isadult);
        public bool UpdateTitleBasic(TitleBasic titleBasic);
        public bool DeleteTitleBasic(string titleId);
        public IList<TitleBasic> GetTitleBasicsBySearch(string searchInput);
        public IList<TitleBasic> GetTitleBasicsBySearch(string searchInput, QueryString queryString);

        //NameBasic CRUD
        public NameBasic GetNameBasic(string nameId);
        public bool CreateNameBasic(NameBasic nameBasic);
        public NameBasic CreateNameBasic(string nameId, string primaryName);
        public bool UpdateNameBasic(NameBasic nameBasic);
        public bool DeleteNameBasic(string nameId);
        public IList<NameBasic> GetNameBasicsBySearch(string searchInput, QueryString queryString);
        public IList<NameBasic> GetNameBasicsBySearch(string searchInput);
       

        //TitleRating R
        public TitleRating GetTitleRating(string titleId);


    }
}
