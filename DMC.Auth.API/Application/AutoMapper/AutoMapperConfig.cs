using AutoMapper;
using DMC.Auth.API.Application.Queries.ViewModels;
using DMC.Auth.API.Models;
using DMC.Core.DomainObject.Enum;
using System;

namespace DMC.Auth.API.Application.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserViewModel, User>()
                .ConstructUsing(u =>
                new User(u.Id, u.FirstName, u.LastName, u.Email, u.BirthDate, u.Education));

            CreateMap<User, UserViewModel>()
                .ForMember(destinationMember => destinationMember.EducationName, opt => opt.MapFrom(src => Enum.GetName(typeof(Education), src.Education)))
                .ForMember(destinationMember => destinationMember.Email, opt => opt.MapFrom(src => src.Email.Address))
                .ForMember(destinationMember => destinationMember.BirthDate, opt => opt.MapFrom(src => src.BirthDate.Date));
        }
    }
}