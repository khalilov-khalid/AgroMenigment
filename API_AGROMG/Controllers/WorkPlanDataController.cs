﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API_AGROMG.Data;
using API_AGROMG.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AGROMG.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkPlanDataController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuthRepository _auth;
        public WorkPlanDataController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpGet("professions/{lang}")]// where the responder was true
        public async Task<ActionResult> GetPessions(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);

            List<StandartDto> professionList = await _context.ProfessionLanguanges.Where(s => s.Profession.Status == true && s.Profession.Company == logineduser.Company && s.Language.code == lang).Select(s => new StandartDto()
            {
                Id = s.Profession.Id,
                Name = s.Name
            }).ToListAsync();

            return Ok(professionList);
        }

        [HttpGet("Worker/{id}")]
        public async Task<ActionResult> GetWorker(int id)
        {
            int userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(userid);

            List<StandartDto> datalist = await _context.WorkerProfessions.Where(s => s.Profession.Id == id && s.Workers.Company == logineduser.Company).Select(s => new StandartDto()
            {
                Id = s.Workers.Id,
                Name = s.Workers.Name
            }).ToListAsync();

            return Ok(datalist);
        }

        [HttpGet("Feltilizer")]
        public async Task<ActionResult> GetProduct()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
            List<ProductForFeltilizerDto> data = await _context.Products.Where(s => s.FertilizerKind != null && s.Status == true && s.Company == logineduser.Company).Select(s => new ProductForFeltilizerDto()
            {
                Id = s.Id,
                Name = s.Name,
                FertilizerKindId = s.FertilizerKind.Id,
                MainIngredientId = s.MainIngredient.Id
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("MainIngredients")]
        public async Task<ActionResult> GetMainIngredients()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
            List<StandartDto> data = await _context.MainIngredients.Where(s => s.Status == true && s.Company == logineduser.Company).Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("FertilizerKind/{lang}")]
        public async Task<ActionResult> GetFertilizerKind(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
            List<StandartDto> data = await _context.FertilizerKindLanguage.Where(s => s.Language.code == lang).Select(s => new StandartDto()
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("ParcelCategory/{lang}")]
        public async Task<ActionResult> GetParcelCategory(string lang)
        {
            List<StandartDto> data = await _context.ParcelCategoryLanguages.Where(s => s.Language.code == lang).Select(s => new StandartDto()
            {
                Id = s.ParcelCategory.Id,
                Name = s.Name
            }).ToListAsync();

            return Ok(data);
        }

        [HttpGet("Parcel")]
        public async Task<ActionResult> GetParcel()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);

            List<StandartByCategoryDto> data = await _context.Parcels.Where(s => s.Status == true && s.ParcelCategory != null && s.Company == logineduser.Company).Select(s => new StandartByCategoryDto()
            {
                Id = s.Id,
                Name = s.Name,
                CategoryId = s.ParcelCategory.Id
            }).ToListAsync();

            return Ok(data);
        }
    }
}