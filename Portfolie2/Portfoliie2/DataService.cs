using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portfolie2.Domain;

namespace Portfolie2
{
    public interface IDataService
    {
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
            try
            {
                ctx.TitleBasics.Remove(ctx.TitleBasics.Find(titleId));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

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
