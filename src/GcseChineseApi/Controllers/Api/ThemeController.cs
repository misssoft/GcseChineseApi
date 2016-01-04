using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using GcseChineseApi.Entities;
using GcseChineseApi.ViewModels;
using Microsoft.AspNet.Mvc;
using GcseChineseApi.Repositories;
using Microsoft.Extensions.Logging;

namespace GcseChineseApi.Controllers.Api
{
    [Route("api/[controller]")]
    public class ThemeController:  Controller
    { 
     private readonly IThemeRepository _repository;
    private readonly ILogger<ThemeController> _logger;

    public ThemeController(IThemeRepository repository, ILogger<ThemeController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet("")]
    public JsonResult Get()
    {
        try
        {
            var result = Mapper.Map<IEnumerable<ThemeViewModel>>(_repository.GetAllThemes());
            return new JsonResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { ex.Message });
        }
    }

        [HttpGet("all/")]
        public JsonResult GetAll()
        {
            try
            {
                var result = Mapper.Map<IEnumerable<ThemeViewModel>>(_repository.GetAllThemeswithTopics());
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { ex.Message });
            }
        }

        [HttpGet("{id}/topics/")]
        public JsonResult GetTopicsByThemeId(int id)
        {
            try
            {
                var result = Mapper.Map<IEnumerable<TopicViewModel>>(_repository.GetTopicsbyThemeId(id));
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { ex.Message });
            }
        }

    [HttpPost("")]
    public JsonResult Post([FromBody]ThemeViewModel vm)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var newTheme = Mapper.Map<Theme>(vm);

                _repository.AddTheme(newTheme);

                if (_repository.SaveAll())
                {
                    Response.StatusCode = (int)HttpStatusCode.Created;
                    return Json(Mapper.Map<ThemeViewModel>(newTheme));
                }
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { ex.Message });
        }
        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return Json(new { Message = "Validation Failed.", ModelState });
    }
}
}
