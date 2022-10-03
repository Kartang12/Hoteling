using HotelApi.Contracts.Requests;
using HotelApi.Domain;
using HotelApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RoomController : Controller
    {
        private readonly RoomService _service;

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
        public async Task<IActionResult> Create(CreateRoomRequest request)
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
        public async Task<IActionResult> Update(Room Request)
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
