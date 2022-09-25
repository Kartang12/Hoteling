﻿using Microsoft.AspNetCore.Mvc;
using UserApi.Contracts.Requests;
using UserApi.Domain;
using UserApi.Services;

namespace UserApi.Controllers
{
    public class UserController : Controller
    {
        readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        public async Task<IActionResult> GetByIds(IEnumerable<Guid> ids)
        {
            try
            {
                return Ok(await _service.GetByIdsAsync(ids));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> Create(CreateUserDataRequest request)
        {
            try
            {
                return Ok(await _service.CreateAsync(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> Update(UserData Request)
        {
            try
            {
                return Ok(await _service.UpdateAsync(Request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}