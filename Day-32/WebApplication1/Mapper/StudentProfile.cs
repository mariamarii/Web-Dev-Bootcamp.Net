using AutoMapper;
using WebApplication1.Dtos.StudentDto;
using WebApplication1.Dtos.CourseDto;
using WebApplication1.Models;
using WebApplication1.Features.Students.Command.Models;

namespace WebApplication1.Mapper;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentReadDto>().ReverseMap();
        CreateMap<Student, StudentWriteDto>().ReverseMap();
        CreateMap<Student, StudentSimpleDto>().ReverseMap();
        CreateMap<CreateStudentDto, Student>().ReverseMap();
        CreateMap<CreateStudentSimpleDto, Student>().ReverseMap();
        CreateMap<UpdateStudentDto, Student>().ReverseMap();
        CreateMap<UpdateStudentRequestDto, UpdateStudentDto>(); // Map request DTO to handler DTO
        CreateMap<Course, CourseSimpleDto>().ReverseMap();
    }
}