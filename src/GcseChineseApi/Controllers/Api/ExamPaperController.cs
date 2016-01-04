using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using GcseChineseApi.Entities;
using GcseChineseApi.Repositories;
using Microsoft.AspNet.Mvc;
using GcseChineseApi.ViewModels;
using Microsoft.Extensions.Logging;

namespace GcseChineseApi.Controllers.Api
{
    [Route("api/[controller]")]
    public class ExampaperController: Controller
    {
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IExampaperRepository _exampaperRepository;
        private readonly ILogger<ExampaperController> _logger;

        public ExampaperController(IAssessmentRepository assessmentRepository, IExampaperRepository exampaperRepository, ILogger<ExampaperController> logger )
        {
            _assessmentRepository = assessmentRepository;
            _exampaperRepository = exampaperRepository;
            _logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            try
            {
                var result = _exampaperRepository.GetAllPapers();

                if (result == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<IEnumerable<ExampaperViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(false);
            }
        }

        [HttpGet("code/{code}")]
        public JsonResult GetPaperByCode(string code)
        {
            try
            {
                var result = _exampaperRepository.GetPaperByCode(code);

                if (result == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<IEnumerable<ExampaperViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(false);
            }
        }

        [HttpGet("year/{year}")]
        public JsonResult GetPaperByYear(int year)
        {
            try
            {
                var result = _exampaperRepository.GetPaperByYear(year);

                if (result == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<IEnumerable<ExampaperViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(false);
            }
        }

        [HttpGet("year/{year}/code/{code}")]
        public JsonResult GetPaperByYearandCode(int year, string code)
        {
            try
            {
                var result = _exampaperRepository.GetPaperByYearAndCode(year,code);

                if (result == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<IEnumerable<ExampaperViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(false);
            }
        }

        [HttpGet("{code}/")]
        public JsonResult Get(string code)
        {
            try
            {
                var result = _assessmentRepository.GetAssessmentByCode(code);

                if (result == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<IEnumerable<ExampaperViewModel>>(result.Exampapers));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(false);
            }
        }

        [HttpPost("code/{code}")]
        public JsonResult Create (string code, [FromBody] ExampaperViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newPaper = Mapper.Map<Exampaper>(vm);
                    _assessmentRepository.AddExampaper(code, newPaper);

                    if (_assessmentRepository.SaveAll())
                    {
                        Response.StatusCode = (int) HttpStatusCode.Created;
                        return Json(Mapper.Map<ExampaperViewModel>(newPaper));
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

       
        [HttpDelete("id/{id}")]
        public void Delete(int id)
        {
            try
            {
                var paper = _exampaperRepository.GetPaperById(id);
                _exampaperRepository.Delete(paper);
                if (_exampaperRepository.SaveAll())
                {
                    Response.StatusCode = (int)HttpStatusCode.NoContent;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }

    }
}
