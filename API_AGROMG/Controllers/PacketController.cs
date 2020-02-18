using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_AGROMG.Data;
using API_AGROMG.Dtos;
using API_AGROMG.Model;
using API_AGROMG.SimpleforDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API_AGROMG.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PacketController : ControllerBase
    {
        private readonly DataContext _context;

        public PacketController(DataContext context)
        {
            _context = context;
        }
        //xreate paket
        [HttpPost]
        public async Task<ActionResult> CreatePacket([FromBody]PacketDto _packetDto)
        {
            string keystring = DateTime.Now.ToString().GetHashCode().ToString("x");

            Packet newpacket = new Packet()
            {
                NameKey= DateTime.Now.ToString() + keystring,
                Price = _packetDto.Price,
                HumanCount = _packetDto.HumanContent,
                Content = JsonConvert.SerializeObject(_packetDto.ModulId)
            };

            try
            {
                _context.Add(newpacket);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            var createdPaket = _context.Packets.FirstOrDefault(s => s.Id == newpacket.Id);

            if (createdPaket == null)
            {
                return BadRequest();
            }


            foreach (var item in _packetDto.Content)
            {
                LanguageContext newContent = new LanguageContext()
                {
                    Key = newpacket.NameKey,
                    LangUnicode = item.Languagename,
                    Context = item.Content
                };
                _context.LanguageContexts.Add(newContent);
                await _context.SaveChangesAsync();
            }

            return StatusCode(201);
        }


        //get paket list
        [HttpGet]
        public async Task<ActionResult> GetPaketList()
        {
            var paket = await _context.Packets.Select(s => new Packet()
            {
                Id=s.Id,
                HumanCount=s.HumanCount,
                Price=s.Price,
                NameKey= _context.LanguageContexts.Where(w => w.LangUnicode == "az" && w.Key == s.NameKey).FirstOrDefault().Context,
            }).ToListAsync();
            return Ok(paket);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetPaket(int id)
        {
            var paket = await _context.Packets.FirstOrDefaultAsync(s => s.Id == id);

            if (paket == null)
            {
                return StatusCode(404);
            }

            List<LangcontentDto> langcontent = await _context.LanguageContexts.Where(s => s.Key == paket.NameKey).Select(s => new LangcontentDto
            {
                Languagename = s.LangUnicode,
                Content = s.Context

            }).ToListAsync();

            PacketDto selectedpacket = new PacketDto()
            {
                Id = paket.Id,
                HumanContent = paket.HumanCount,
                Price = paket.Price,
                Content = langcontent,
                ModulId = JsonConvert.DeserializeObject<List<int>>(paket.Content)
            };

            return Ok(selectedpacket);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditPacket(int id,[FromBody]PacketDto _packetDto)
        {
            if (id != _packetDto.Id)
            {
                return BadRequest();
            }

            var editedpacket = await _context.Packets.FirstOrDefaultAsync(s => s.Id == id);

            if (editedpacket==null)
            {
                return NotFound();
            }

            editedpacket.Price = _packetDto.Price;
            editedpacket.HumanCount = _packetDto.HumanContent;
            editedpacket.Content = JsonConvert.SerializeObject(_packetDto.ModulId);
            _context.Entry(editedpacket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

            var deletetcontent = await _context.LanguageContexts.Where(s => s.Key == editedpacket.NameKey).ToListAsync();

            foreach (var item in deletetcontent)
            {
                _context.LanguageContexts.Remove(item);
                await _context.SaveChangesAsync();
            }

            for (int i = 0; i < _packetDto.Content.Count; i++)
            {
                LanguageContext newContent = new LanguageContext()
                {
                    Key = editedpacket.NameKey,
                    LangUnicode = _packetDto.Content[i].Languagename,
                    Context = _packetDto.Content[i].Content
                };
                _context.LanguageContexts.Add(newContent);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }        
    }
}