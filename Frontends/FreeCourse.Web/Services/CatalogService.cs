using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Helpers;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalogs;
using FreeCourse.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("categories");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var viewModelResponse = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return viewModelResponse.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            var response = await _httpClient.GetAsync("courses");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var viewModelResponse = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
            viewModelResponse.Data.ForEach(k => k.StockPictureUrl = _photoHelper.GetPhotoStockUrl(k.Picture));

            return viewModelResponse.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var viewModelResponse = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
            viewModelResponse.Data.ForEach(k => k.StockPictureUrl = _photoHelper.GetPhotoStockUrl(k.Picture));

            return viewModelResponse.Data;
        }

        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            var response = await _httpClient.GetAsync($"courses/{courseId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var viewModelResponse = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
            viewModelResponse.Data.StockPictureUrl = _photoHelper.GetPhotoStockUrl(viewModelResponse.Data.Picture);

            return viewModelResponse.Data;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput input)
        {
            if (input.PhotoFormFile != null)
            {
                var photoViewModel = await _photoStockService.UploadPhoto(input.PhotoFormFile);
                if (photoViewModel != null)
                {
                    input.Picture = photoViewModel.Url;
                }
            }

            var response = await _httpClient.PostAsJsonAsync<CourseCreateInput>("courses", input);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput input)
        {
            if (input.PhotoFormFile != null)
            {
                await _photoStockService.DeletePhoto(input.Picture);

                var photoViewModel = await _photoStockService.UploadPhoto(input.PhotoFormFile);
                if (photoViewModel != null)
                {
                    input.Picture = photoViewModel.Url;
                }
            }

            var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("courses", input);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var courseItem = await _httpClient.GetAsync($"courses/{courseId}");
            var viewModelResponse = await courseItem.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
            if (!string.IsNullOrEmpty(viewModelResponse.Data.Picture))
            {
                await _photoStockService.DeletePhoto(viewModelResponse.Data.Picture);
            }

            var response = await _httpClient.DeleteAsync($"courses/{courseId}");
            return response.IsSuccessStatusCode;
        }

    }
}
