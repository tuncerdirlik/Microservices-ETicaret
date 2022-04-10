using Dapper;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Enums;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourseService.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var deleteStatus = await _dbConnection.ExecuteAsync("delete from discount where id=@id", new { id });
            if (deleteStatus > 0)
            {
                return Response<NoContent>.Success(ResponseStatusCodes.NoContent);
            }
            else
            {
                return Response<NoContent>.Fail("an error accoured while updating the record", ResponseStatusCodes.InternalServerError);
            }
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from discount");

            return Response<List<Models.Discount>>.Success(discounts.ToList(), ResponseStatusCodes.Ok);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("select * from discount where code=@Code and userid=@UserId", new { code, userId })).FirstOrDefault();

            if (discount == null)
            {
                return Response<Models.Discount>.Fail("discount not found", ResponseStatusCodes.NotFound);
            }
            else
            {
                return Response<Models.Discount>.Success(discount, ResponseStatusCodes.Ok);
            }
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("select * from discount where id=@id", new { id })).FirstOrDefault();

            if (discount == null)
            {
                return Response<Models.Discount>.Fail("discount not found", ResponseStatusCodes.NotFound);
            }
            else
            {
                return Response<Models.Discount>.Success(discount, ResponseStatusCodes.Ok);
            }
        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("insert into discount (userid, rate, code) values (@UserId, @Rate, @Code)", discount);
            if (saveStatus > 0)
            {
                return Response<NoContent>.Success(ResponseStatusCodes.Created);
            }
            else
            {
                return Response<NoContent>.Fail("an error accoured while insertintg to database", ResponseStatusCodes.InternalServerError);
            }
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var updateStatus = await _dbConnection.ExecuteAsync("update discount set userid=@UserId, rate=@Rate, code=@Code where Id=@id", discount);
            if (updateStatus > 0)
            {
                return Response<NoContent>.Success(ResponseStatusCodes.NoContent);
            }
            else
            {
                return Response<NoContent>.Fail("an error accoured while updating the record", ResponseStatusCodes.InternalServerError);
            }
        }
    }
}
