using API.DTO.Enrollment;
using API.DTO.Testing;
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
        CreateMap<ModuleAccessEntity, ModuleAccess>()
            .ReverseMap()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Module, opt => opt.Ignore());
        CreateMap<ModuleEntity, Module>().ReverseMap();
        CreateMap<ResourceEntity, Resource>()
            .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => Enum.Parse<ResourceType>(src.Type)))
            .ReverseMap()
            .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => src.Type.ToString()));
        CreateMap<RoleEntity, Role>().ReverseMap();
        CreateMap<TestEntity, Test>().ReverseMap();
        CreateMap<UserEntity, User>().ReverseMap();
        CreateMap<CreateEnrollmentDto, Enrollment>();
        CreateMap<UpdateEnrollmentDto, Enrollment>();
        CreateMap<SubmitAnswersDto, SubmitAnswersCommand>().ReverseMap();
        CreateMap<SubmitAnswersResultDto, SubmitAnswersResult>().ReverseMap();
        CreateMap<TestingQuestionDto, TestingQuestion>().ReverseMap();
        CreateMap<AddOrUpdateTestQuestions, TestingQuestion>().ReverseMap();
    }
}