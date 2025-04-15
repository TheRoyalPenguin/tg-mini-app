using API.DTO.Enrollment;
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
        CreateMap<ModuleAccessEntity, ModuleAccess>().ReverseMap();
        CreateMap<ModuleEntity, Module>().ReverseMap();
        CreateMap<ResourceEntity, Resource>().ReverseMap();
        CreateMap<RoleEntity, Role>().ReverseMap();
        CreateMap<TestEntity, Test>().ReverseMap();
        CreateMap<UserEntity, User>().ReverseMap();
        CreateMap<CreateEnrollmentDto, Enrollment>();
        CreateMap<UpdateEnrollmentDto, Enrollment>();
    }
}