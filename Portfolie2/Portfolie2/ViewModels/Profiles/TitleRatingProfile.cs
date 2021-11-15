using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Portfolie2;
using Portfolie2.Domain;

namespace WebService.ViewModels.Profiles
{
    public class TitleRatingProfile : Profile
    {
        public TitleRatingProfile()
        {
            CreateMap<TitleRating, TitleRatingViewModel>();
        }
    }
}