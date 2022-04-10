using AutoMapper;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Enums;
using FreeCourseServices.Catalog.Dtos;
using FreeCourseServices.Catalog.Models;
using FreeCourseServices.Catalog.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourseServices.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

            _mapper = mapper;
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find<Course>(c => c.Id == id).FirstOrDefaultAsync();
            if (course == null)
            {
                return Response<CourseDto>.Fail("Course not found", ResponseStatusCodes.NotFound);
            }

            course.Category = await _categoryCollection.Find<Category>(c => c.Id == course.CategoryId).FirstOrDefaultAsync();

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), ResponseStatusCodes.Ok);
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find<Course>(course => true).ToListAsync();
            
            if (courses.Any())
            {
                foreach (var courseItem in courses)
                {
                    courseItem.Category = await _categoryCollection.Find<Category>(c => c.Id == courseItem.CategoryId).FirstOrDefaultAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), ResponseStatusCodes.Ok);
        }

        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find<Course>(course => course.UserId == userId).ToListAsync();
            if (courses.Any())
            {
                foreach (var courseItem in courses)
                {
                    courseItem.Category = await _categoryCollection.Find<Category>(c => c.Id == courseItem.CategoryId).FirstOrDefaultAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), ResponseStatusCodes.Ok);
        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<Course>(courseCreateDto);
            newCourse.Createdtime = DateTime.Now;
            
            await _courseCollection.InsertOneAsync(newCourse);

            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), ResponseStatusCodes.Created);
        }

        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(k => k.Id == updateCourse.Id, updateCourse);

            if (result == null)
            {
                return Response<NoContent>.Fail("Course not found", ResponseStatusCodes.Created);
            }

            return Response<NoContent>.Success(ResponseStatusCodes.NotFound);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(k => k.Id == id);

            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(ResponseStatusCodes.NotFound);
            }

            return Response<NoContent>.Fail("Course not found", ResponseStatusCodes.NotFound);
        }
    }
}
