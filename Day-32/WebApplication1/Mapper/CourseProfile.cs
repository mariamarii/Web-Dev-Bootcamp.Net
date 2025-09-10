using AutoMapper;
using WebApplication1.Dtos.CourseDto;
using WebApplication1.Dtos.StudentDto;
using WebApplication1.Models;
using WebApplication1.Features.Courses.Command.Models;

namespace WebApplication1.Mapper;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseReadDto>().ReverseMap();
        CreateMap<Course, CourseWriteDto>().ReverseMap();
        CreateMap<Course, CourseSimpleDto>().ReverseMap();
        CreateMap<CreateCourseDto, Course>().ReverseMap();
        CreateMap<UpdateCourseDto, Course>().ReverseMap();
        CreateMap<Student, StudentSimpleDto>().ReverseMap();
    }
}