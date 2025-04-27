using API.DTO.Enrollment;
using API.DTO.ModuleRequests;
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
        //CreateMap<ResourceEntity, Resource>()
        //    .ForMember(dest => dest.Type,
        //        opt => opt.MapFrom(src => Enum.Parse<ResourceType>(src.Type)))
        //    .ReverseMap()
        //    .ForMember(dest => dest.Type,
        //        opt => opt.MapFrom(src => src.Type.ToString()));
        CreateMap<RoleEntity, Role>().ReverseMap();
        CreateMap<TestEntity, Test>().ReverseMap();
        CreateMap<UserEntity, User>()
            .ForMember(dest => dest.ModuleAccesses, opt => opt.MapFrom(src => src.ModuleAccesses))
            .ReverseMap()
            .ForMember(dest => dest.ModuleAccesses, opt => opt.MapFrom(src => src.ModuleAccesses));
        CreateMap<CreateEnrollmentDto, Enrollment>();
        CreateMap<UpdateEnrollmentDto, Enrollment>();

        CreateMap<LongreadEntity, Longread>()
            .ForMember(dest => dest.ImageKeys, opt => opt.MapFrom(src => src.Images.Select(i => i.Key)))
            .ReverseMap()
            .ForMember(e => e.Images, opt => opt.Ignore())
            .ForMember(e => e.Id, opt => opt.Ignore())
            .ForMember(e => e.Module, opt => opt.Ignore());

        CreateMap<TestEntity, Test>().ReverseMap()
            .ForMember(e => e.Id, opt => opt.Ignore())
            .ForMember(e => e.Module, opt => opt.Ignore());

        CreateMap<BookEntity, Book>()
            .ForMember(dest => dest.ModuleIds, opt => opt.MapFrom(src => src.ModuleBooks.Select(mb => mb.ModuleId)))
            .ReverseMap()
            .ForMember(e => e.Id, opt => opt.Ignore())
            .ForMember(e => e.ModuleBooks, opt => opt.Ignore());

        CreateMap<NewModuleRequest, Module>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<SubmitAnswersDto, SubmitAnswersCommand>().ReverseMap();
        CreateMap<SubmitAnswersResultDto, SubmitAnswersResult>().ReverseMap();
        CreateMap<TestingQuestionDto, TestingQuestion>().ReverseMap();
        CreateMap<AddOrUpdateTestQuestions, TestingQuestion>().ReverseMap();
        CreateMap<LongreadCompletionEntity, LongreadCompletion>().ReverseMap();
    }
}