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
    public class NameBasicProfile : Profile
    {
        public NameBasicProfile()
        {
            CreateMap<NameBasic, NameBasicViewModel>()
                .ForMember(src => src.ListProfessions, dst => dst.MapFrom(x => x.Professions.Select(p => p.ProfessionName)))
                .ForMember(src => src.ListKnownForTitles, dst => dst.MapFrom(x => x.KnownForTitles.Select(p => p.TitleId)));

            CreateMap<NameBasicViewModel, NameBasic>();
        }
    }
}