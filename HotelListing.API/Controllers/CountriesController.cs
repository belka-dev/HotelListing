using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Models.Country;
using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data.Configurations;
using HotelListing.API.Data;
using Microsoft.AspNetCore.Authorization;
using HotelListing.API.Exceptions;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesRepository _countriesRepository;
        private readonly IMapper _mapper;

        public CountriesController(ICountriesRepository countriesRepository, IMapper mapper)
        {
            _countriesRepository = countriesRepository;
            _mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
   
        public async Task<ActionResult<IEnumerable<CountryVo>>> GetCounties()
        {
            
            List<CountryDao> countries = await _countriesRepository.GetAllAsync();
            return Ok(_mapper.Map<List<CountryVo>>(countries));
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        
        public async Task<ActionResult<CountryDetailsVo>> GetCountry(int id)
        {


            var country = await _countriesRepository.GetDetails(id);
            var contriesDetailsVo = _mapper.Map<CountryDetailsVo>(country);
            if (country == null)
            {
                throw new NotFoundException(nameof(GetCountry), id);
            }

            return contriesDetailsVo;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
   
        public async Task<IActionResult> PutCountry(int id, CountryDetailsVo countryVo)
        {
            if (id != countryVo.Id)
            {
                return BadRequest();
            }

            var country = await _countriesRepository.GetAsync(id);

            if(country == null)
            {
                return NotFound();
            }
            _mapper.Map(countryVo, country);
            //    _countriesRepository.Entry(country).State = EntityState.Modified;

            try
            {
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await CountryExists(id))
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
        [Authorize]
        public async Task<ActionResult<CountryDao>> PostCountry(CountryVo countryVo)
        {
            var country = _mapper.Map<CountryDao>(countryVo);

            await _countriesRepository.AddAsync(country);
            

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

          
            await _countriesRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return  await _countriesRepository.Exists(id);
        }
    }
}
