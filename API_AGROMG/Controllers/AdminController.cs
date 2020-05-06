using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API_AGROMG.Data;
using API_AGROMG.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AGROMG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DataContext _context;

        public AdminController(DataContext context)
        {
            _context = context;
        }

        private class AdminUser
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Admin(int id)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync();

            if (admin == null)
            {
                return NotFound();
            }

            AdminUser loginedadmin = new AdminUser()
            {
                Id = 1,
                Name = admin.Username
            };
            
            return Ok(admin);
        }


        //checkStatus = 1> gozlemede  , 2 > testiq olunub , 3 > red edilib
        
        [HttpPost]
        public async Task<ActionResult> Default()
        {
            var languages = await _context.Languages.ToListAsync();

            var FertilizerCount = await _context.FertilizerKind.ToListAsync();
            if (FertilizerCount.Count == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    FertilizerKind kind = new FertilizerKind();
                    _context.FertilizerKind.Add(kind);
                    await _context.SaveChangesAsync();

                    if (i == 0 )
                    {
                        foreach (var item in languages)
                        {
                            if (item.code == "az")
                            {
                                FertilizerKindLanguage kindLanguage = new FertilizerKindLanguage()
                                {
                                    FertilizerKind = kind,
                                    Language = item,
                                    Name = "Dərman"
                                };
                                _context.FertilizerKindLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                            if (item.code == "en")
                            {
                                FertilizerKindLanguage kindLanguage = new FertilizerKindLanguage()
                                {
                                    FertilizerKind = kind,
                                    Language = item,
                                    Name = "Dərmanen"
                                };
                                _context.FertilizerKindLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                            if (item.code == "ru")
                            {
                                FertilizerKindLanguage kindLanguage = new FertilizerKindLanguage()
                                {
                                    FertilizerKind = kind,
                                    Language = item,
                                    Name = "Dərmanru"
                                };
                                _context.FertilizerKindLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                    if (i == 1)
                    {
                        foreach (var item in languages)
                        {
                            if (item.code == "az")
                            {
                                FertilizerKindLanguage kindLanguage = new FertilizerKindLanguage()
                                {
                                    FertilizerKind = kind,
                                    Language = item,
                                    Name = "Gübrə"
                                };
                                _context.FertilizerKindLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                            if (item.code == "en")
                            {
                                FertilizerKindLanguage kindLanguage = new FertilizerKindLanguage()
                                {
                                    FertilizerKind = kind,
                                    Language = item,
                                    Name = "GübrəEn"
                                };
                                _context.FertilizerKindLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                            if (item.code == "ru")
                            {
                                FertilizerKindLanguage kindLanguage = new FertilizerKindLanguage()
                                {
                                    FertilizerKind = kind,
                                    Language = item,
                                    Name = "GübrəRU"
                                };
                                _context.FertilizerKindLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }

                }
            }

            var MeasurementUnitCount = await _context.MeasurementUnits.ToListAsync();
            if (MeasurementUnitCount.Count == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    MeasurementUnit data = new MeasurementUnit();
                    _context.MeasurementUnits.Add(data);
                    await _context.SaveChangesAsync();

                    if (i == 0)
                    {
                        foreach (var item in languages)
                        {
                            if (item.code == "az")
                            {
                                MeasurementUnitLanguage kindLanguage = new MeasurementUnitLanguage()
                                {
                                    MeasurementUnit = data,
                                    Language = item,
                                    Name = "KQ"
                                };
                                _context.MeasurementUnitLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                            if (item.code == "en")
                            {
                                MeasurementUnitLanguage kindLanguage = new MeasurementUnitLanguage()
                                {
                                    MeasurementUnit = data,
                                    Language = item,
                                    Name = "KQEn"
                                };
                                _context.MeasurementUnitLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                            if (item.code == "ru")
                            {
                                MeasurementUnitLanguage kindLanguage = new MeasurementUnitLanguage()
                                {
                                    MeasurementUnit = data,
                                    Language = item,
                                    Name = "KQRu"
                                };
                                _context.MeasurementUnitLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                    if (i == 1)
                    {
                        foreach (var item in languages)
                        {
                            if (item.code == "az")
                            {
                                MeasurementUnitLanguage kindLanguage = new MeasurementUnitLanguage()
                                {
                                    MeasurementUnit = data,
                                    Language = item,
                                    Name = "Litr"
                                };
                                _context.MeasurementUnitLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                            if (item.code == "en")
                            {
                                MeasurementUnitLanguage kindLanguage = new MeasurementUnitLanguage()
                                {
                                    MeasurementUnit = data,
                                    Language = item,
                                    Name = "LitrEn"
                                };
                                _context.MeasurementUnitLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                            if (item.code == "ru")
                            {
                                MeasurementUnitLanguage kindLanguage = new MeasurementUnitLanguage()
                                {
                                    MeasurementUnit = data,
                                    Language = item,
                                    Name = "LitrEn"
                                };
                                _context.MeasurementUnitLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                    if (i == 2)
                    {
                        foreach (var item in languages)
                        {
                            if (item.code == "az")
                            {
                                MeasurementUnitLanguage kindLanguage = new MeasurementUnitLanguage()
                                {
                                    MeasurementUnit = data,
                                    Language = item,
                                    Name = "Ədəd"
                                };
                                _context.MeasurementUnitLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                            if (item.code == "en")
                            {
                                MeasurementUnitLanguage kindLanguage = new MeasurementUnitLanguage()
                                {
                                    MeasurementUnit = data,
                                    Language = item,
                                    Name = "ƏdədEn"
                                };
                                _context.MeasurementUnitLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                            if (item.code == "ru")
                            {
                                MeasurementUnitLanguage kindLanguage = new MeasurementUnitLanguage()
                                {
                                    MeasurementUnit = data,
                                    Language = item,
                                    Name = "ƏdədRu"
                                };
                                _context.MeasurementUnitLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }


            var countrycount = await _context.Country.ToListAsync();
            if (countrycount.Count == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Country data = new Country();
                    _context.Country.Add(data);
                    await _context.SaveChangesAsync();

                    if (i == 0)
                    {
                        foreach (var item in languages)
                        {
                            if (item.code == "az")
                            {
                                CountryLanguage kindLanguage = new CountryLanguage()
                                {
                                    Country = data,
                                    Language = item,
                                    Name = "Azərbaycan"
                                };
                                _context.CountryLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                            if (item.code == "en")
                            {
                                CountryLanguage kindLanguage = new CountryLanguage()
                                {
                                    Country = data,
                                    Language = item,
                                    Name = "AzərbaycanEN"
                                };
                                _context.CountryLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                            if (item.code == "ru")
                            {
                                CountryLanguage kindLanguage = new CountryLanguage()
                                {
                                    Country = data,
                                    Language = item,
                                    Name = "AzərbaycanRU"
                                };
                                _context.CountryLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                    if (i == 1)
                    {
                        foreach (var item in languages)
                        {
                            if (item.code == "az")
                            {
                                CountryLanguage kindLanguage = new CountryLanguage()
                                {
                                    Country = data,
                                    Language = item,
                                    Name = "Rusiya"
                                };
                                _context.CountryLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                            if (item.code == "en")
                            {
                                CountryLanguage kindLanguage = new CountryLanguage()
                                {
                                    Country = data,
                                    Language = item,
                                    Name = "RusiyaEn"
                                };
                                _context.CountryLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                            if (item.code == "ru")
                            {
                                CountryLanguage kindLanguage = new CountryLanguage()
                                {
                                    Country = data,
                                    Language = item,
                                    Name = "RusiyaRu"
                                };
                                _context.CountryLanguage.Add(kindLanguage);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }                    
                }
            }

            //ParcelCategory parcel = new ParcelCategory();
            //_context.ParcelCategories.Add(parcel);
            //await _context.SaveChangesAsync();
            return Ok();
        }

    }
}