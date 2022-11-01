using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Models.Country;
using AutoMapper;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;

        public CountriesController(HotelListingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryVo>>> GetCounties()
        {
            
            List<CountryDao> countries = await _context.Counties.ToListAsync();
            return Ok(_mapper.Map<List<CountryVo>>(countries));
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDetailsVo>> GetCountry(int id)
        {


            var country = await _context.Counties
                .Include(q => q.Hotels)
                .FirstOrDefaultAsync(q=> q.Id == id);
            var contriesDetailsVo = _mapper.Map<CountryDetailsVo>(country);
            if (country == null)
            {
                return NotFound();
            }

            return contriesDetailsVo;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, CountryDetailsVo countryDto)
        {
            if (id != countryDto.Id)
            {
                return BadRequest();
            }

            var country = await _context.Counties.FindAsync(id);

            if(country == null)
            {
                return NotFound();
            }
            _mapper.Map(countryDto, country);
            //    _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CountryDao>> PostCountry(CountryVo countryVo)
        {
            var country = _mapper.Map<CountryDao>(countryVo);
            
            _context.Counties.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Counties.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Counties.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return _context.Counties.Any(e => e.Id == id);
        }
    }
}
