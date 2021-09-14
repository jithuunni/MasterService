using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterService.Api.Data.Entities;
using MasterService.Api.Data.Repositories;
using MasterService.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MasterService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ILogger<CountriesController> logger;
        private readonly IMapper mapper;
        private readonly ICountryRepository countryRepository;

        public CountriesController(ILogger<CountriesController> logger, IMapper mapper, ICountryRepository countryRepository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
        }

        [HttpPost]
        public async Task<IActionResult> AddCountry(CountryModel country)
        {
            try
            {
                Country entity = mapper.Map<Country>(country);
                entity = await countryRepository.AddAsync(entity);
                country = mapper.Map<CountryModel>(entity);
                return Ok(country);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error occured while adding Country: {country.Name}");
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCountry(CountryModel country)
        {
            try
            {
                Country entity = mapper.Map<Country>(country);
                bool updated = await countryRepository.UpdateAsync(entity);
                if (updated)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error occured while updating Country: {country.Name}");
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{cluster}/{id:int}")]
        public async Task<IActionResult> DeleteCountry([FromRoute]string cluster,[FromRoute]int id)
        {
            try
            {
                bool updated = await countryRepository.DeleteAsync(id, cluster);
                if (updated)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error occured while deleteing Country Id: {id}, Cluster:{cluster}");
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            try
            {
                List<Country> entities = await countryRepository.GetAllAsync();
                if (entities == null)
                {
                    return BadRequest();
                }
                List<CountryModel> countries = mapper.Map<List<CountryModel>>(entities);
                return Ok(countries);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error occured while fetching Countries");
                return StatusCode(500, ex);
            }
        }

        [HttpGet("Cluster/{cluster}")]
        public async Task<IActionResult> GetAllCountriesByCluster([FromRoute]string cluster)
        {
            try
            {
                List<Country> entities = await countryRepository.GetAllAsync(cluster);
                if (entities == null)
                {
                    return BadRequest();
                }
                List<CountryModel> countries = mapper.Map<List<CountryModel>>(entities);
                return Ok(countries);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error occured while fetching Countries");
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                Country entity = await countryRepository.GetAsync(id);
                if (entity == null)
                {
                    return NotFound();
                }
                CountryModel country = mapper.Map<CountryModel>(entity);
                return Ok(country);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error occured while fetching Country for Id:{id}");
                return StatusCode(500, ex);
            }
        }
    }// class ends
}
