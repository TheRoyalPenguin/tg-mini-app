using API.DTO.Book;
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
        CreateMap<LongreadCompletionEntity, LongreadCompletion>()
            .ForMember(dest => dest.ModuleAccessId, 
                opt => opt.MapFrom(src => src.ModuleAccessId))
            .ReverseMap()
            .ForMember(dest => dest.ModuleAccess, opt => opt.Ignore())
            .ForMember(dest => dest.Longread, opt => opt.Ignore());
        
        CreateMap<ModuleAccessEntity, ModuleAccess>()
            .ForMember(dest => dest.LongreadCompletions, 
                opt => opt.MapFrom(src => src.LongreadCompletions))
            .ReverseMap()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Module, opt => opt.Ignore());
        CreateMap<ModuleEntity, Module>().ReverseMap();
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
            .ForMember(dest => dest.AudioContentKey, opt => opt.MapFrom(src => src.AudioContentKey))
            .ReverseMap()
            .ForMember(e => e.Images, opt => opt.Ignore())
            .ForMember(e => e.Id, opt => opt.Ignore())
            .ForMember(e => e.Module, opt => opt.Ignore());

        CreateMap<TestEntity, Test>().ReverseMap()
            .ForMember(e => e.Id, opt => opt.Ignore())
            .ForMember(e => e.Module, opt => opt.Ignore());

        CreateMap<BookEntity, Book>()
            .ReverseMap()
            .ForMember(e => e.Id, opt => opt.Ignore())
            .ForMember(e => e.Module, opt => opt.Ignore());

        CreateMap<NewModuleRequest, Module>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<SubmitAnswersDto, SubmitAnswersCommand>().ReverseMap();
        CreateMap<SubmitAnswersResultDto, SubmitAnswersResult>().ReverseMap();
        CreateMap<TestingQuestionDto, TestingQuestion>().ReverseMap();
        CreateMap<AddOrUpdateTestQuestions, TestingQuestion>().ReverseMap();
        CreateMap<LongreadCompletionEntity, LongreadCompletion>().ReverseMap();
        
        CreateMap<TestResultEntity, TestResult>()
            .ReverseMap()
            .ForMember(dest => dest.Test, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());

        CreateMap<Book, ResponseBookDto>()
            .ForMember(dest => dest.ContentUrl, opt => opt.Ignore())
            .ForMember(dest => dest.CoverUrl, opt => opt.Ignore());
    }
}