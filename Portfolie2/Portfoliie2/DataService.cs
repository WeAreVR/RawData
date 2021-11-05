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
        //TitleBasics
        public TitleBasic GetTitleBasic(string titleId);
        public bool CreateTitleBasic(TitleBasic titleBasic);
        public bool UpdateTitleBasic(TitleBasic titleBasic);
        public bool DeleteTitleBasic(string titleId);


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
            titleBasic.Id = ctx.TitleBasics.Max(x => x.Id) + 1;
            ctx.Add(titleBasic);
            return ctx.SaveChanges() > 0;
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

    }

}
