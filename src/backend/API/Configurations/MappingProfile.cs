using AutoMapper;
using Core.Models;
using Persistence.Entities;

namespace API.Configurations;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CourseEntity, Course>().ReverseMap();
        CreateMap<EnrollmentEntity, Enrollment>().ReverseMap();
        CreateMap<LessonEntity, Lesson>().ReverseMap();
        CreateMap<LessonProgressEntity, LessonProgress>().ReverseMap();
        CreateMap<ModuleAccessEntity, ModuleAccess>()
            .ReverseMap()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Module, opt => opt.Ignore());
        CreateMap<ModuleEntity, Module>().ReverseMap();
        CreateMap<ResourceEntity, Resource>().ReverseMap();
        CreateMap<RoleEntity, Role>().ReverseMap();
        CreateMap<TestEntity, Test>().ReverseMap();
        CreateMap<UserEntity, User>().ReverseMap();
    }
}