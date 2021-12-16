using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataServiceLib;
using DataServiceLib.Domain;

namespace WebService.ViewModels.Profiles
{
    public class TitleBasicProfile : Profile
    {
        public TitleBasicProfile()
        {
            CreateMap<TitleBasic, TitleBasicViewModel>()
                .ForMember(src => src.Genres, dst => dst.MapFrom(x => x.TitleGenres.Select(g => g.Genre)))
                .ForMember(src => src.Akas, dst => dst.MapFrom(x => x.TitleAkas.Select(g => g.Title)))
                .ForMember(src => src.Genres, dst => dst.MapFrom(x => x.TitleGenres.Select(g => g.Genre)));
            
                CreateMap<TitleBasicViewModel, TitleBasic>();
            CreateMap<CreateTitleBasicViewModel, TitleBasic>();
        }
    }
}