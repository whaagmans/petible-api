﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petible_api.Interfaces;
using Petible_api.Models;
using Petible_api.Models.CustomModels;

namespace Petible_api.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        IUnitOfWork uow;
        IMatchRepository matchesRepository;

        public MatchController(IUnitOfWork uow, IMatchRepository matchesRepository)
		{
            this.uow = uow;
            this.matchesRepository = matchesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await matchesRepository.ListAll());
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Match match = await matchesRepository.FindById(id);
            if (match == null) return BadRequest();
            else return Ok(match);
        }

        [HttpGet("pet/{id}")]
        public async Task<IActionResult> GetMatchesByAnimalId(string id)
        {
            List<MatchForShelter> matches = await matchesRepository.GetMatchesByAnimalId(id);
            if (matches == null) return BadRequest();
            else return Ok(matches);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetMatchesByUserId(string id)
        {
            List<Match> matches = await matchesRepository.GetMatchInfo(id);
            if (matches == null) return BadRequest();
            else return Ok(matches);
        }

        [HttpPut]
        public async Task<IActionResult> Post([FromBody] Match match)
        {
            if (match.id == null) match.id = Guid.NewGuid().ToString();
            try
            {
                await matchesRepository.Save(match);
                await uow.Commit();
                return Created("petible.nl", match);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Match match)
        {
			try
			{
				await matchesRepository.Remove(match);
				await uow.Commit();
				return NoContent();
			}
			catch (Exception)
			{
                return BadRequest();
			}
        }
    }
}
