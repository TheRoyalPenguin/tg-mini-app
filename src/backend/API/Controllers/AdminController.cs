using API.DTO.User;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController(
    IAdminService adminService) : ControllerBase
{
    [HttpPost("registration/course/{courseId:int}/register/{userId:int}")]
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
        
        if (!serviceResult.IsSuccess)
            return Problem(serviceResult.ErrorMessage);

        var resultModels = serviceResult.Data;
        try
        {
            return Ok(resultModels.Select(
                rm => new UserWithFullStatisticResponse(rm.user, rm.testResults)));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("test-statistic/user/{userId:int}")]
    public async Task<IActionResult> GetTestResultsByUserAsync(int userId)
    {
        var serviceResult = await adminService.GetTestResultsByUser(userId);

        if (!serviceResult.IsSuccess)
            return Problem(serviceResult.ErrorMessage);

        var resultModel = serviceResult.Data;
        try
        {
            return Ok(new UserWithFullStatisticResponse(resultModel.user, resultModel.testResults));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("test-statistic/course/{courseId:int}/user/{userId:int}")]
    public async Task<IActionResult> GetTestResultsForUserByCourseAsync(int userId, int courseId)
    {
        var serviceResult = await adminService.GetTestResultsForUserByCourse(userId, courseId);

        if (!serviceResult.IsSuccess)
            return Problem(serviceResult.ErrorMessage);

        var resultModel = serviceResult.Data;
        try
        {
            return Ok(new UserWithFullStatisticResponse(resultModel.user, resultModel.testResults));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}