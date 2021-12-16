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
    public class TitlePrincipalProfile : Profile
    {
        public TitlePrincipalProfile()
        {
            CreateMap<TitlePrincipal, TitlePrincipalViewModel>();
        }
    }
}