using AutoMapper;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Utils;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Repositories;

public class TestResultRepository(
    AppDbContext appDbContext,
    IMapper mapper) : ITestResultRepository
{
    public async Task<Result<TestResult>> AddAsync(TestResult model)
    {
        try
        {
            var testResultEntity = mapper.Map<TestResultEntity>(model);
            await appDbContext.TestResults.AddAsync(testResultEntity);
            
            var result = mapper.Map<TestResult>(testResultEntity);
            return Result<TestResult>.Success(result);
        }
        catch (Exception e)
        {
            return Result<TestResult>.Failure(e.Message)!;
        }
    }

    public async Task<Result<TestResult>> UpdateAsync(TestResult model)
    {
        try
        {
            var entity = await appDbContext.TestResults
                .FirstOrDefaultAsync(e => e.Id == model.Id);
            if (entity == null)
            {
                return Result<TestResult>.Failure("Test result entity not found")!;
            }

            mapper.Map(model, entity);
            appDbContext.TestResults.Update(entity);
            
            return Result<TestResult>.Success(mapper.Map<TestResult>(entity));
        }
        catch (Exception e)
        {
            return Result<TestResult>.Failure(e.Message)!;
        }
    }

    public async Task<Result> DeleteAsync(TestResult model)
    {
        try
        {
            var entity = await appDbContext.TestResults
                .FirstOrDefaultAsync(e => e.Id == model.Id);
            if (entity == null)
            {
                return Result.Failure("Test result entity not found");
            }
            
            appDbContext.TestResults.Remove(entity);
            
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure(e.Message);
        }
    }

    public async Task<Result<TestResult?>> GetByIdAsync(int id)
    {
        try
        {
            var testResultEntity = await appDbContext.TestResults
                .FirstOrDefaultAsync(x => x.Id == id);
            if (testResultEntity == null)
            {
                return Result<TestResult?>.Success(null);
            }
            
            var result = mapper.Map<TestResult>(testResultEntity);
            return Result<TestResult?>.Success(result);
        }
        catch (Exception e)
        {
            return Result<TestResult?>.Failure(e.Message);
        }
    }

    public async Task<Result<ICollection<TestResult>>> GetAllAsync()
    {
        try
        {
            var testResultEntities = await appDbContext.TestResults.ToListAsync();
            var results = mapper.Map<ICollection<TestResult>>(testResultEntities);
            return Result<ICollection<TestResult>>.Success(results);
        }
        catch (Exception e)
        {
            return Result<ICollection<TestResult>>.Failure(e.Message)!;
        }
    }

    public async Task<Result<ICollection<TestResult>>> GetAllByUser(int userId)
    {
        try
        {
            var testResultEntities = await appDbContext.TestResults
                .Where(x => x.UserId == userId)
                .ToListAsync();
                
            return Result<ICollection<TestResult>>.Success(mapper.Map<ICollection<TestResult>>(testResultEntities));
        }
        catch (Exception e)
        {
            return Result<ICollection<TestResult>>.Failure(e.Message)!;
        }
    }

    public async Task<Result<ICollection<TestResult>>> GetAllForUserByCourse(int userId, int courseId)
    {
        try
        {
            var testResultEntities = await appDbContext.TestResults
                .Include(x => x.Test)
                    .ThenInclude(t => t.Module)
                .Where(x => x.UserId == userId && x.Test.Module.CourseId == courseId)
                .ToListAsync();
                
            return Result<ICollection<TestResult>>.Success(mapper.Map<ICollection<TestResult>>(testResultEntities));
        }
        catch (Exception e)
        {
            return Result<ICollection<TestResult>>.Failure(e.Message)!;
        }
    }

    public async Task<Result<ICollection<TestResult>>> GetAllByCourse(int courseId)
    {
        try
        {
            var testResultEntities = await appDbContext.TestResults
                .Include(x => x.Test)
                    .ThenInclude(t => t.Module)
                .Where(x => x.Test.Module.CourseId == courseId)
                .ToListAsync();
                
            return Result<ICollection<TestResult>>.Success(mapper.Map<ICollection<TestResult>>(testResultEntities));
        }
        catch (Exception e)
        {
            return Result<ICollection<TestResult>>.Failure(e.Message)!;
        }
    }
}
