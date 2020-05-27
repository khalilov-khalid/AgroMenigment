using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_AGROMG.Data;
using API_AGROMG.Dtos;
using API_AGROMG.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AGROMG.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IAuthRepository _auth;
        public MapController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpPost]
        public async Task<ActionResult> CreateMapLayers(List<MapLayerCreateDto> layers)
        {
            foreach (var item in layers)
            {
                MapLayer _newlayer = new MapLayer()
                {
                    Type = item.type,
                    Coordinates = item.coordinates,
                    ParcelId = item.parselId != null ? item.parselId : null,
                    CompanyId = 1
                };
                _context.MapLayers.Add(_newlayer);
                await _context.SaveChangesAsync();
            }

            return StatusCode(201);
        }

        [HttpGet]
        public async Task<ActionResult> ListMapLayer()
        {
            List<MapLayerListDto> datalist = await _context.MapLayers
                .Where(s => s.CompanyId == 1)
                .Select(s => new MapLayerListDto()
                {
                    Id = s.Id,
                    coordinates = s.Coordinates,
                    type = s.Type,
                    parselName =s.Parcel.Name
                }).ToListAsync();
            return Ok(datalist);
        }
    }
}