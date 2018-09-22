using Eleveight.Models.Domain.User;
using Eleveight.Models.Requests.App;
using Eleveight.Models.Requests.User;
using Eleveight.Models.Responses;
using Eleveight.Services;
using Eleveight.Services.App;
using Eleveight.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Eleveight.Web.Controllers.Api.User
{
    
        [RoutePrefix("api/user/AwardType")]
        public class AwardTypeController : ApiController
        {
            IAwardTypeService _awardTypeService;
            IAppLogService _appLogService;
            IUserService _userService;
            public AwardTypeController(IAwardTypeService awardTypeService, IAppLogService appLogService, IUserService userService)
            {
                _awardTypeService = awardTypeService;
                _appLogService = appLogService;
                _userService = userService;
            }

            [Route(), HttpGet]
            public IHttpActionResult SelectAll()
            {
                try
                {
                    ItemsResponse<AwardType> response = new ItemsResponse<AwardType>();
                    response.Items = _awardTypeService.SelectAll();
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    int currentUser = _userService.GetCurrentUserId();
                    _appLogService.Insert(new AppLogAddRequest
                    {
                        AppLogTypeId = 1,
                        Message = ex.Message,
                        StackTrace = ex.StackTrace,
                        Title = "Error in " + GetType().Name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                        UserBaseId = currentUser
                    });

                    return BadRequest(ex.Message);
                }
            }

            [Route("{id:int}"), HttpGet]
            public IHttpActionResult SelectById(int id)
            {
                try
                {
                    ItemResponse<AwardType> response = new ItemResponse<AwardType>
                    {
                        Item = _awardTypeService.SelectById(id)
                    };
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    int currentUser = _userService.GetCurrentUserId();
                    _appLogService.Insert(new AppLogAddRequest
                    {
                        AppLogTypeId = 1,
                        Message = ex.Message,
                        StackTrace = ex.StackTrace,
                        Title = "Error in " + GetType().Name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                        UserBaseId = currentUser
                    });

                    return BadRequest(ex.Message);
                }
            }

            [Route(), HttpPost]
            public IHttpActionResult Post(AwardTypeAddRequest model)
            {
                try
                {
                    if (!ModelState.IsValid) return BadRequest(ModelState);
                    ItemResponse<int> response = new ItemResponse<int>
                    {
                        Item = _awardTypeService.Insert(model)
                    };
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    int currentUser = _userService.GetCurrentUserId();
                    _appLogService.Insert(new AppLogAddRequest
                    {
                        AppLogTypeId = 1,
                        Message = ex.Message,
                        StackTrace = ex.StackTrace,
                        Title = "Error in " + GetType().Name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                        UserBaseId = currentUser
                    });

                    return BadRequest(ex.Message);
                }
            }

            [Route("{id:int}"), HttpPut]
            public IHttpActionResult Put(AwardTypeUpdateRequest model)
            {
                try
                {
                    _awardTypeService.Update(model);
                    return Ok(new SuccessResponse());
                }
                catch (Exception ex)
                {
                    int currentUser = _userService.GetCurrentUserId();
                    _appLogService.Insert(new AppLogAddRequest
                    {
                        AppLogTypeId = 1,
                        Message = ex.Message,
                        StackTrace = ex.StackTrace,
                        Title = "Error in " + GetType().Name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                        UserBaseId = currentUser
                    });

                    return BadRequest(ex.Message);
                }
            }

            [Route("{id:int}"), HttpDelete]
            public IHttpActionResult Delete(int id)
            {
                try
                {
                    _awardTypeService.Delete(id);
                    return Ok(new SuccessResponse());
                }
                catch (Exception ex)
                {
                    int currentUser = _userService.GetCurrentUserId();
                    _appLogService.Insert(new AppLogAddRequest
                    {
                        AppLogTypeId = 1,
                        Message = ex.Message,
                        StackTrace = ex.StackTrace,
                        Title = "Error in " + GetType().Name + " " + System.Reflection.MethodBase.GetCurrentMethod().Name,
                        UserBaseId = currentUser
                    });

                    return BadRequest(ex.Message);
                }
            }

        }
}