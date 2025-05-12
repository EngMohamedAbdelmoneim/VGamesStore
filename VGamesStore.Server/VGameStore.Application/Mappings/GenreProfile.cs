using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.DTOs;
using VGameStore.Core.Entities;

namespace VGameStore.Application.Mappings
{
    public class GenreProfile : Profile
    {
        public GenreProfile() {
            CreateMap<Genre, GenreDto>().ReverseMap();
        }

    }
}
