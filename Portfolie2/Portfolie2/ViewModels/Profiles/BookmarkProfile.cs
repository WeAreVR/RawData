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
    public class BookmarkProfile : Profile
    {
        public BookmarkProfile()
        {
            CreateMap<Bookmark, BookmarkViewModel>();
        }
    }
}