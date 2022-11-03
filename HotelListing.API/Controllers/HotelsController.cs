using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Contracts;
using AutoMapper;
using HotelListing.API.Models.Hotel;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelsRepository _hotelsRepository;
        private readonly IMapper _mapper;
        public HotelsController(IMapper mapper, IHotelsRepository hotelRepository)
        {
            _hotelsRepository = hotelRepository;
            _mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            List<HotelDao> resultList = await _hotelsRepository.GetAllAsync();
            return Ok(_mapper.Map<List<HotelDto>>(resultList));
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotelDao = await _hotelsRepository.GetAsync(id);

            if (hotelDao == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map <HotelDto>( hotelDao ));
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelDao(int id, HotelDto hotelDto)
        {
            if (id != hotelDto.Id)
            {
                return BadRequest();
            }
         var hotel = _hotelsRepository.GetAsync(id);
            if(hotel == null)
            {
                return BadRequest();
            }
              
           HotelDao hotelDao = _mapper.Map<HotelDao>(hotelDto);
            
          try
            {
                await _hotelsRepository.UpdateAsync(hotelDao);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await HotelDaoExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelDao>> PostHotelDao(HotelDto hotelDto)
        {
            var hotelDao = _mapper.Map<HotelDao>(hotelDto);
            await _hotelsRepository.AddAsync(hotelDao);

            return CreatedAtAction("GetHotelDao", new { id = hotelDao.Id }, hotelDao);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelDao(int id)
        {
            var hotelDao = await _hotelsRepository.GetAsync(id);
            if (hotelDao == null)
            {
                return NotFound();
            }

            await _hotelsRepository.DeleteAsync(id);
          
            return NoContent();
        }

        private async Task<bool> HotelDaoExistsAsync(int id)
        {
            return await _hotelsRepository.Exists(id);
        }
    }
}
