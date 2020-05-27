using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API_AGROMG.Data;
using API_AGROMG.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using API_AGROMG.Model;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace API_AGROMG.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HRController : ControllerBase
    {

        private readonly DataContext _context;

        private readonly IAuthRepository _auth;

        public HRController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }


        [HttpGet]
        public async Task<ActionResult> GelAllUsers()
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);

            if (await _auth.VerifyUser(id) == null)
            {
                return Unauthorized();
            }

            var users = await _context.Workers.Where(s => s.Status == true && s.Company == logineduser.Company).ToListAsync();


            List<WorkerDataForHR> userlist = users.Select(s => new WorkerDataForHR()
            {
                Id = s.Id,
                Name = s.Name,
                Adress = s.Adress,
                Birthday = s.Birthday,
                Email = s.Email,
                Tel = s.Tel,
                Gender = s.Gender,
                StartDate = s.WorkStartDate,
                WorkStatus = s.WorkStatus.ToString(),
                Professions = _context.WorkerProfessions.Where(w => w.Workers.Id == s.Id).Select(w => w.Profession.Id).ToList(),
                Fin = s.Fin,
                SerialNumber = s.SerialNumber,
                SSN = s.SSN,
                GrossSalary = (_context.WorkerSalaries.FirstOrDefault(w => w.Workers.Id == s.Id)==null) ? "": _context.WorkerSalaries.FirstOrDefault(w => w.Workers.Id == s.Id).GrossSalary.ToString(),
                NetSalary = (_context.WorkerSalaries.FirstOrDefault(w => w.Workers.Id == s.Id)==null) ? "": _context.WorkerSalaries.FirstOrDefault(w => w.Workers.Id == s.Id).NetSalary.ToString()
            }).ToList();
            return Ok(userlist);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(int id) 
        {
            var worker = await _context.Workers.FirstOrDefaultAsync(s => s.Id == id); 

            if (worker == null)
            {
                return NotFound();
            }
            WorkerForEditDtos Worker = new WorkerForEditDtos()
            {
                ID = worker.Id,
                Name = worker.Name,
                Adress = worker.Adress,
                Birthday = worker.Birthday,
                Gender = worker.Gender,
                Email = worker.Email,
                Phone = worker.Tel,
                WorkStartDate = worker.WorkStartDate,
                WorkStatus = worker.WorkStatus,
                Fin = worker.Fin,
                SerialNumber = worker.SerialNumber,
                SSN = worker.SSN
            };
            return Ok(Worker);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditUser(int id , [FromBody]WorkerForEditDtos worker)
        {
            if (id != worker.ID)
            {
                return BadRequest("Datada Sehvlik var");
            }
            var editeduser = await _context.Workers.FirstOrDefaultAsync(s => s.Id == worker.ID);

            editeduser.Name = worker.Name;
            editeduser.Birthday = worker.Birthday;
            editeduser.Email = worker.Email;
            editeduser.Tel = worker.Phone;
            editeduser.Adress = worker.Adress;
            editeduser.Gender = worker.Gender;
            editeduser.WorkStartDate = worker.WorkStartDate;
            editeduser.WorkStatus = worker.WorkStatus;
            editeduser.Fin = worker.Fin;
            editeduser.SerialNumber = worker.SerialNumber;
            editeduser.SSN = worker.SSN;
            try
            {
                _context.Entry(editeduser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Emeliyyat ugursuz");
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWorker(int id)
        {
            var deletedUser = await _context.Workers.Include(s => s.Company).FirstOrDefaultAsync(s => s.Id == id);

            deletedUser.Status = false;
            try
            {
                _context.Entry(deletedUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return BadRequest();
            }
            int HumanCount = await _context.Workers.Where(s => s.Company == deletedUser.Company && s.Status == true).CountAsync();

            var Company = await _context.Companies.FirstOrDefaultAsync(s => s.Id == deletedUser.Company.Id);
            Company.HumanCount = HumanCount;
            _context.Entry(Company).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateWorker([FromBody]WorkerForAddDtos worker)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (!CheckHumanCount(id))
            {
                return BadRequest("Paketinizin istifadə limitini aşmisiz");
            }

            var logineduser = await _auth.VerifyUser(id);

            Workers new_user = new Workers()
            {
                Name = worker.Name,
                Status = true,
                Birthday = worker.Birthday,
                Email = worker.Email,
                Tel = worker.Phone,
                Adress = worker.Adress,
                Gender = worker.Gender,
                WorkStartDate = worker.WorkStartDate,
                WorkStatus = worker.WorkStatus,
                Company = logineduser.Company,
                Fin = worker.Fin,
                SerialNumber = worker.SerialNumber,
                SSN = worker.SSN
            };
            try
            {
                await _context.Workers.AddAsync(new_user);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            WorkerSalary workerSalary = new WorkerSalary()
            {
                Workers= new_user,
                GrossSalary = worker.GrossSalary,
                NetSalary = worker.NetSalary,
                Status = true,
                StartSalary = worker.WorkStartDate
            };
            await _context.WorkerSalaries.AddAsync(workerSalary);
            await _context.SaveChangesAsync();

            foreach (var item in worker.ProfessionId)
            {
                WorkerProfessions WorkerProfession = new WorkerProfessions();
                WorkerProfession.Workers = new_user;
                WorkerProfession.Profession = await _context.Professions.Where(s => s.Id == item).FirstOrDefaultAsync();
                WorkerProfession.Startdate = worker.WorkStartDate;
                WorkerProfession.Status = true;
                await _context.WorkerProfessions.AddAsync(WorkerProfession);
                await _context.SaveChangesAsync();
            }

            int HumanCount = await _context.Workers.Where(s => s.Company == logineduser.Company && s.Status == true).CountAsync();

            var Company = await _context.Companies.FirstOrDefaultAsync(s => s.Id == logineduser.Company.Id);
            Company.HumanCount = HumanCount;
            _context.Entry(Company).State = EntityState.Modified;


            return StatusCode(201);
        }       


        [HttpPost("Salary")]
        public async Task<ActionResult> EditSalary([FromBody]WorkerSalaryDto salary)
        {
            var Worker = await _context.Workers.FirstOrDefaultAsync(s => s.Id == salary.WorkerId && s.Status ==true);

            if (Worker==null)
            {
                return BadRequest("Sistemde bele bir isci yoxdur");
            }

            var oldSalary = await _context.WorkerSalaries.FirstOrDefaultAsync(s =>s.Workers == Worker && s.Status == true && s.EndSalary == null);

            if (oldSalary == null)
            {
                oldSalary.EndSalary = salary.StartDate;
                oldSalary.Status = false;
                _context.Entry(oldSalary).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            

            WorkerSalary newSalary = new WorkerSalary()
            {
                StartSalary = salary.StartDate,
                GrossSalary = salary.GrossSalary,
                NetSalary = salary.NetSalary,
                Status = true,
                Workers = Worker
            };
            await _context.WorkerSalaries.AddAsync(newSalary);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpGet("SalaryHistory/{Id}")]
        public async Task<ActionResult> SalaryHistory(int id)
        {
            var worker = await _context.Workers.FirstOrDefaultAsync(s => s.Id == id);
            if (worker == null)
            {
                return BadRequest("Sistemde bele bir isci yoxdur");
            }

            List<WorkerSalaryHistoryDto> data = await _context.WorkerSalaries.Where(s => s.Workers == worker).OrderByDescending(s=>s.Id).Select(s => new WorkerSalaryHistoryDto()
            {
                Id = s.Id,
                StartDate = s.StartSalary,
                GrossSalary = s.GrossSalary,
                NetSalary = s.NetSalary,
                EndDate =s.EndSalary
            }).ToListAsync();
            return Ok(data);
        }


        [HttpGet("Profession/{Id}")]
        public async Task<ActionResult> GetProfession(int id)
        {
            var worker = await _context.Workers.FirstOrDefaultAsync(s => s.Id == id);

            if (worker == null)
            {
                return BadRequest("Sistemde Bele bir istifadeci yoxdur");
            };

            List<WorkerProfessionGetDto> datalist = await _context.WorkerProfessions.Where(s => s.Workers == worker && s.Status == true).Select(s => new WorkerProfessionGetDto() {
                Id = s.Id,
                StartDate = s.Startdate,
                ProfessionId = s.Profession.Id
            }).ToListAsync();

            return Ok(datalist);
        }

        [HttpPost("Profession")]
        public async Task<ActionResult> Profession([FromBody] WorkerProfessionDto professions)
        {
            var worker = await _context.Workers.FirstOrDefaultAsync(s => s.Id == professions.WorkerId);
            if (worker == null)
            {
                return BadRequest("Melumat duzgun deyil");
            }

            foreach (var item in professions.WorkerProfessions)
            {
                if (item.Id==0)
                {
                    WorkerProfessions newProfession = new WorkerProfessions()
                    {
                        Startdate = item.StartDate,
                        Profession = await _context.Professions.FirstOrDefaultAsync(s => s.Id == item.ProfessionId),
                        Status = true,
                        Workers = worker
                    };
                    await _context.WorkerProfessions.AddAsync(newProfession);
                    
                }
                else
                {
                    if (item.Status == true)
                    {
                        var editedprofession = await _context.WorkerProfessions.FirstOrDefaultAsync(s => s.Id == item.Id);
                        if (editedprofession==null)
                        {
                            return BadRequest("Xeta bas verdi.");
                        }

                        editedprofession.Startdate = item.StartDate;
                        editedprofession.Profession = await _context.Professions.FirstOrDefaultAsync(s => s.Id == item.Id);
                        _context.Entry(editedprofession).State = EntityState.Modified;
                    }
                    else
                    {
                        var deletedprofession = await _context.WorkerProfessions.FirstOrDefaultAsync(s => s.Id == item.Id);
                        if (deletedprofession == null)
                        {
                            return BadRequest("Xeta bas verdi.");
                        }

                        deletedprofession.Status = false;
                        deletedprofession.EndDate = item.EndDate;
                        deletedprofession.EndReason = item.EndReason;
                        _context.Entry(deletedprofession).State = EntityState.Modified;
                    }
                }
            }
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("ProfessionHistory/{lang}/{Id}")]
        public async Task<ActionResult> ProfessionHistory(string lang , int id)
        {
            var worker = await _context.Workers.FirstOrDefaultAsync(s => s.Id == id);
            if (worker == null)
            {
                return BadRequest("Uygun melumat tapilmadi");
            }

            List<WorkerProfessionHistoryDto> data = await _context.WorkerProfessions.Where(s => s.Workers == worker).OrderByDescending(s=>s.Id).Select(s => new WorkerProfessionHistoryDto()
            {
                StartDate =s.Startdate,
                ProfessionName = _context.ProfessionLanguanges.FirstOrDefault(w=>w.Language.code == lang && w.Profession == s.Profession).Name,
                EndDate = s.EndDate,
                EndReason = s.EndReason
            }).ToListAsync();

            return Ok(data);
        }

        private bool CheckHumanCount(int id)
        {
            var user =  _context.Workers.Include(c => c.Company).Include(p => p.Company.Packet).FirstOrDefault(s => s.Id == id);

            if (user.Company.HumanCount >= user.Company.Packet.HumanCount)
            {
                return false;
            }
            return true;

        }
    }
}