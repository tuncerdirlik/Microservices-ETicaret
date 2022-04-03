using AutoMapper;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Enums;
using FreeCourseServices.Catalog.Dtos;
using FreeCourseServices.Catalog.Models;
using FreeCourseServices.Catalog.Settings;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourseServices.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

            _mapper = mapper;
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Category>(k => k.Id == id).FirstOrDefaultAsync();
            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not found", (int)ResponseStatusCodes.NotFound);
            }

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), (int)ResponseStatusCodes.Ok);
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find<Category>(category => true).ToListAsync();

            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), (int)ResponseStatusCodes.Ok);
        }

        public async Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.InsertOneAsync(category);

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), (int)ResponseStatusCodes.Created);
        }

        public async Task<Response<NoContent>> UpdateAsync(CategoryDto categoryDto)
        {
            var updateCategory = _mapper.Map<Category>(categoryDto);
            var result = await _categoryCollection.FindOneAndReplaceAsync(c => c.Id == updateCategory.Id, updateCategory);

            if (result == null)
            {
                return Response<NoContent>.Fail("Course not found", (int)ResponseStatusCodes.NotFound);
            }

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _categoryCollection.DeleteOneAsync(c => c.Id == id);

            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success((int)ResponseStatusCodes.NotFound);
            }

            return Response<NoContent>.Fail("Course not found", (int)ResponseStatusCodes.NotFound);

        }
    }
}
