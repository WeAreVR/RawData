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
    public class RatingHistoryProfile : Profile
    {
        public RatingHistoryProfile()
        {
            CreateMap<RatingHistory, RatingHistoryViewModel>();
            CreateMap<CreateRatingHistoryViewModel, RatingHistory>();
        }
    }
}