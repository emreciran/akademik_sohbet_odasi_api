using akademik_sohbet_odasi_api.Dto;
using AutoMapper;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace akademik_sohbet_odasi_api.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Question, QuestionDto>();

            CreateMap<Tag, TagDto>();

            CreateMap<Category, CategoryDto>();
        }
    }
}
