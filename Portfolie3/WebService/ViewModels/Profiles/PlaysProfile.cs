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
    public class PlaysProfile : Profile
    {
        public PlaysProfile()
        {
            CreateMap<Plays, PlaysViewModel>();
             /*   .ForMember(src => src.TitleId, dst => dst.MapFrom(x => x.Plays.Select(p => p.ProfessionName)))
                .ForMember(src => src.ListKnownForTitles, dst => dst.MapFrom(x => x.KnownForTitles.Select(p => p.TitleId)));*/

           // CreateMap<NameBasicViewModel, NameBasic>();
        }
    }
}