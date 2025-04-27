using API.DTO.User;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController(
    IAdminService adminService) : ControllerBase
{
    [HttpGet("course/{courseId:int}")]
    public async Task<IActionResult> GetUsersByCourseIdAsync(int courseId)
    {
        var serviceResult = await adminService.GetUsersInCourse(courseId);

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
    public async Task<IActionResult> GetUserInCourseAsync(int userId, int courseId)
    {
        var serviceResult = await adminService.GetConcreteUserInCourse(userId, courseId);

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
    public async Task<IActionResult> GetUserByIdAsync(int userId)
    {
        var serviceResult = await adminService.GetConcreteUser(userId);

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

    [HttpPost("course/{courseId:int}/register/{userId:int}")]
    public async Task<IActionResult> AddUserToCourse(int courseId, int userId)
    {
        var serviceResult = await adminService.RegisterUserOnCourse(userId, courseId);

        return serviceResult.IsSuccess
            ? Ok(serviceResult)
            : Problem(serviceResult.ErrorMessage);
    }
    
    [HttpGet("test-statistic/course/{courseId:int}")]
    public async Task<IActionResult> GetTestResultsByCourseAsync(int courseId)
    {
        var serviceResult = await adminService.GetTestResultsByCourse(courseId);

        return serviceResult.IsSuccess 
            ? Ok(serviceResult.Data)
            : Problem(serviceResult.ErrorMessage);
    }

    [HttpGet("test-statistic/user/{userId:int}")]
    public async Task<IActionResult> GetTestResultsByUserAsync(int userId)
    {
        var serviceResult = await adminService.GetTestResultsByUser(userId);

        return serviceResult.IsSuccess
            ? Ok(serviceResult.Data)
            : Problem(serviceResult.ErrorMessage);
    }

    [HttpGet("test-statistic/course/{courseId:int}/user/{userId:int}")]
    public async Task<IActionResult> GetTestResultsForUserByCourseAsync(int userId, int courseId)
    {
        var serviceResult = await adminService.GetTestResultsForUserByCourse(userId, courseId);

        return serviceResult.IsSuccess
            ? Ok(serviceResult.Data)
            : Problem(serviceResult.ErrorMessage);
    }
}