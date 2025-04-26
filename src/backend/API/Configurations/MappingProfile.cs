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
        CreateMap<LongreadCompletionEntity, LongreadCompletion>()
            .ForMember(dest => dest.ModuleAccessId, 
                opt => opt.MapFrom(src => src.ModuleAccessId))
            .ReverseMap()
            .ForMember(dest => dest.ModuleAccess, opt => opt.Ignore())
            .ForMember(dest => dest.Resource, opt => opt.Ignore());
        
        CreateMap<ModuleAccessEntity, ModuleAccess>()
            .ForMember(dest => dest.LongreadCompletions, 
                opt => opt.MapFrom(src => src.LongreadCompletions))
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
        CreateMap<UserEntity, User>()
            .ForMember(dest => dest.ModuleAccesses, opt => opt.MapFrom(src => src.ModuleAccesses))
            .ReverseMap()
            .ForMember(dest => dest.ModuleAccesses, opt => opt.MapFrom(src => src.ModuleAccesses));
        CreateMap<CreateEnrollmentDto, Enrollment>();
        CreateMap<UpdateEnrollmentDto, Enrollment>();
    }
}