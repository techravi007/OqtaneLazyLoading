using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using LLM.Module.LazyLoadingTest.Repository;
using Oqtane.Controllers;
using System.Net;

namespace LLM.Module.LazyLoadingTest.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class LazyLoadingTestController : ModuleControllerBase
    {
        private readonly ILazyLoadingTestRepository _LazyLoadingTestRepository;

        public LazyLoadingTestController(ILazyLoadingTestRepository LazyLoadingTestRepository, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _LazyLoadingTestRepository = LazyLoadingTestRepository;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.LazyLoadingTest> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return _LazyLoadingTestRepository.GetLazyLoadingTests(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized LazyLoadingTest Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.LazyLoadingTest Get(int id)
        {
            Models.LazyLoadingTest LazyLoadingTest = _LazyLoadingTestRepository.GetLazyLoadingTest(id);
            if (LazyLoadingTest != null && IsAuthorizedEntityId(EntityNames.Module, LazyLoadingTest.ModuleId))
            {
                return LazyLoadingTest;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized LazyLoadingTest Get Attempt {LazyLoadingTestId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.LazyLoadingTest Post([FromBody] Models.LazyLoadingTest LazyLoadingTest)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, LazyLoadingTest.ModuleId))
            {
                LazyLoadingTest = _LazyLoadingTestRepository.AddLazyLoadingTest(LazyLoadingTest);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "LazyLoadingTest Added {LazyLoadingTest}", LazyLoadingTest);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized LazyLoadingTest Post Attempt {LazyLoadingTest}", LazyLoadingTest);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                LazyLoadingTest = null;
            }
            return LazyLoadingTest;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.LazyLoadingTest Put(int id, [FromBody] Models.LazyLoadingTest LazyLoadingTest)
        {
            if (ModelState.IsValid && LazyLoadingTest.LazyLoadingTestId == id && IsAuthorizedEntityId(EntityNames.Module, LazyLoadingTest.ModuleId) && _LazyLoadingTestRepository.GetLazyLoadingTest(LazyLoadingTest.LazyLoadingTestId, false) != null)
            {
                LazyLoadingTest = _LazyLoadingTestRepository.UpdateLazyLoadingTest(LazyLoadingTest);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "LazyLoadingTest Updated {LazyLoadingTest}", LazyLoadingTest);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized LazyLoadingTest Put Attempt {LazyLoadingTest}", LazyLoadingTest);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                LazyLoadingTest = null;
            }
            return LazyLoadingTest;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.LazyLoadingTest LazyLoadingTest = _LazyLoadingTestRepository.GetLazyLoadingTest(id);
            if (LazyLoadingTest != null && IsAuthorizedEntityId(EntityNames.Module, LazyLoadingTest.ModuleId))
            {
                _LazyLoadingTestRepository.DeleteLazyLoadingTest(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "LazyLoadingTest Deleted {LazyLoadingTestId}", id);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized LazyLoadingTest Delete Attempt {LazyLoadingTestId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
