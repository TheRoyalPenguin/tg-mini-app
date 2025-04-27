using API.DTO.User;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class StatisticController(IStatisticService service) : ControllerBase
{
    [HttpGet("course/{courseId:int}")]
    public async Task<IActionResult> GetUsersInCourseStatistic(int courseId)
    {
        var serviceResult = await service.GetUsersInCourseStatisticAsync(courseId);

        var userModels = serviceResult.Data;

        try
        {
            return Ok(userModels.Select(um => new UserWithStatisticResponse(um)).ToList());
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("course/{courseId:int}/user/{userId:int}")]
    public async Task<IActionResult> GetConcreteUserInCourseStatisticAsync(int userId, int courseId)
    {
        var serviceResult = await service.GetConcreteUserInCourseStatisticAsync(userId, courseId);

        var userModel = serviceResult.Data;

        try
        {
            return Ok(new UserWithStatisticResponse(userModel));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetUserStatisticByIdAsync(int userId)
    {
        var serviceResult = await service.GetConcreteUserStatisticAsync(userId);

        if (!serviceResult.IsSuccess)
            return Problem(serviceResult.ErrorMessage);

        var userModel = serviceResult.Data;

        try
        {
            return Ok(new UserWithStatisticResponse(userModel));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
    
    [HttpPost("test-results")]
    public async Task<IActionResult> AddStatisticForUserAsync([FromBody] TestResult testResult)
    {
        testResult.Timestamp = DateTime.Now;
        var serviceAddResult = await service.AddStatisticAsync(testResult);

        return serviceAddResult.IsSuccess 
            ? Created("", serviceAddResult.Data) 
            : Problem(serviceAddResult.ErrorMessage);
    }
}