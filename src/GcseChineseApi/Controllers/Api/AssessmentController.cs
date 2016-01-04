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
    public class AssessmentController: Controller
    {
        private readonly IAssessmentRepository _repository;
        private readonly ILogger<AssessmentController> _logger;

        public AssessmentController(IAssessmentRepository repository, ILogger<AssessmentController> logger )
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            try
            {
                var result = Mapper.Map<IEnumerable<AssessmentViewModel>>(_repository.GetAllAssessments());
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
                var result = Mapper.Map<IEnumerable<AssessmentViewModel>>(_repository.GetAllAssessmentswithExampapers());
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { ex.Message });
            }
        }

        [HttpGet("code/{code}")]
        public JsonResult GetByCode(string code)
        {
            try
            {
                var result = Mapper.Map<AssessmentViewModel>(_repository.GetAssessmentByCode(code));
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { ex.Message });
            }
        }

        [HttpGet("code/{code}/papers")]
        public JsonResult GetPapersbyCode(string code)
        {
            try
            {
                var result = Mapper.Map<IEnumerable<ExampaperViewModel>>(_repository.GetPapersByCode(code));
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
        public JsonResult Create ([FromBody]AssessmentViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newAssessment = Mapper.Map<Assessment>(vm);

                    _repository.AddAssessment(newAssessment);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<AssessmentViewModel>(newAssessment));
                    }  
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new {ex.Message });
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Validation Failed.", ModelState });
        }

        [HttpPut("code/{code}")]
        public JsonResult Update(string code, [FromBody] AssessmentViewModel vm)
        {
            if (vm != null && vm.Code == code)
            {
                var toBeUpdatedItem = _repository.GetAssessmentByCode(code);
                if (toBeUpdatedItem == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Message = "Cannot find the target object", vm });
                }
                try
                {
                    if (ModelState.IsValid)
                    {
                        var updatedAssessment = Mapper.Map<Assessment>(vm);

                        _repository.UpdateAssessment(updatedAssessment);

                        if (_repository.SaveAll())
                        {
                            Response.StatusCode = (int)HttpStatusCode.NoContent;
                            return Json(Mapper.Map<AssessmentViewModel>(updatedAssessment));
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { ex.Message });
                }
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Validation Failed.", ModelState });
        }

        [HttpDelete("code/{code}")]
        public void Delete(string code)
        {
            try
            {
                var assessment = _repository.GetAssessmentByCode(code);
                _repository.DeleteAssessment(assessment);
                if (_repository.SaveAll())
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

