using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using GcseChineseApi.Entities;
using GcseChineseApi.Repositories;
using GcseChineseApi.ViewModels;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;

namespace GcseChineseApi.Controllers.Api
{
    [Route("api/[controller]")]
    public class TopicController : Controller
    {
        private readonly ITopicRepository _topicRepositoryrepository;
        private readonly IThemeRepository _themeRepository;
        private readonly ILogger<TopicController> _logger;

        public TopicController(IThemeRepository themeRepository, ITopicRepository topicRepository, ILogger<TopicController> logger)
        {
            _themeRepository = themeRepository;
            _topicRepositoryrepository = topicRepository;
            _logger = logger;
        }

        [HttpGet("all/")]
        public JsonResult GetAll()
        {
            try
            {
                var result = _topicRepositoryrepository.GetAllTopics();

                if (result == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<IEnumerable<TopicViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(false);
            }
        }

        [HttpGet("id/{id}")]
        public JsonResult GetTopicById(int id)
        {
            try
            {
                var result = _topicRepositoryrepository.GetTopicById(id);

                if (result == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<TopicViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(false);
            }
        }

        [HttpGet("name/{name}")]
        public JsonResult GetTopicByName(string name)
        {
            try
            {
                var result = _topicRepositoryrepository.GetTopicByName(name);

                if (result == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<TopicViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(false);
            }
        }

        [HttpPost("")]
        public JsonResult Post(int id, [FromBody] TopicViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTopic = Mapper.Map<Topic>(vm);
                    _themeRepository.AddTopic(id, newTopic);

                    if (_themeRepository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<TopicViewModel>(newTopic));
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
