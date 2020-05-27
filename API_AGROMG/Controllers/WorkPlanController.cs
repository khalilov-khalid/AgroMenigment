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
using Microsoft.EntityFrameworkCore;
using API_AGROMG.Model;

namespace API_AGROMG.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkPlanController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAuthRepository _auth;
        public WorkPlanController(DataContext context, IAuthRepository auth)
        {
            _context = context;
            _auth = auth;
        }

        [HttpPost]
        public async Task<ActionResult> AddWorkPlan([FromBody] WorkPlanDto workPlanDto)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
            if (DateTime.Now > workPlanDto.StartDate)
            {
                return BadRequest("Baslama zamani indiki zamandan kecmisde ola bilmez");
            }
            if (DateTime.Now > workPlanDto.EndDate)
            {
                return BadRequest("Bitme vaxt zamani indiki zamandan kecmisde ola bilmez");
            }

            WorkPlan newPlan = new WorkPlan()
            {
                Name = workPlanDto.Name,
                StartDate = workPlanDto.StartDate,
                EndDate = workPlanDto.EndDate,
                Action = await _context.Actions.FirstOrDefaultAsync(s => s.Id == 4),                
                Created = logineduser,
                Company = logineduser.Company,
                Status = true,
                Parcel = _context.Parcels.FirstOrDefault(s => s.Id == workPlanDto.ParcelId)
            };
            try
            {
                await _context.WorkPlans.AddAsync(newPlan);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest("Emeliyyatda xeta bas verdi");
            }

            foreach (var item in workPlanDto.WorkPlanTasks)
            {
                WorkPlanTask newtask = new WorkPlanTask()
                {
                    Name = item.Name,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    WorkPlan = newPlan,
                    Status = true,
                    Respondent = _context.Workers.FirstOrDefault(s=>s.Id == item.RespondentId)
                };
                await _context.WorkPlanTasks.AddAsync(newtask);
                await _context.SaveChangesAsync();

                if (item.WorkPlanTaskFertilizers != null)
                {
                    foreach (var itemFeltilizer in item.WorkPlanTaskFertilizers)
                    {
                        WorkPlanTaskFertilizer _newTaskFertilizer = new WorkPlanTaskFertilizer()
                        {
                            Product = _context.Products.FirstOrDefault(s => s.Id == itemFeltilizer.ProductId),
                            Quantity = itemFeltilizer.Quantity,
                            WorkPlanTask = newtask
                        };
                        await _context.WorkPlanTaskFertilizers.AddAsync(_newTaskFertilizer);
                        await _context.SaveChangesAsync();
                    }
                }
            }        

            return StatusCode(201);
        }


        [HttpGet("{lang}")]
        public async Task<ActionResult> GetAllWorkPlans(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);
            List<WorkPlanReadDto> data = await _context.WorkPlans.Where(s => s.Company == logineduser.Company).Select(s => new WorkPlanReadDto()
            {
                Id = s.Id,
                Name = s.Name,
                StartDate = s.StartDate,
                Enddate = s.EndDate,
                FinishDate = s.FinisDate,
                WorkStatus = _context.ActionLanguanges.FirstOrDefault(w => w.Language.code == lang && w.Action == s.Action).Name,
                WorkPlanTask = _context.WorkPlanTasks.Where(w => w.WorkPlan.Id == s.Id).Select(w => new WorkPlanTaskReadDto()
                {

                    Id = w.Id,
                    Name = w.Name,
                    StartDate = w.StartDate,
                    EndDate = w.EndDate,
                    Respondent = _context.Workers.FirstOrDefault(a => a.Id == w.Respondent.Id).Name,
                    Status = w.Status,
                    WorkPlanTaskFertilizer = _context.WorkPlanTaskFertilizers.Where(a=>a.WorkPlanTask.Id == w.Id).Select(a=> new WorkPlanTaskFertilizerReadDto() 
                    {
                        Id = a.Id,
                        FertilizerKind = a.Product.FertilizerKind.FertilizerKindLanguage.FirstOrDefault(r=>r.Language.code== lang).Name,
                        MainIngredient = a.Product.MainIngredient.Name,
                        Name = a.Product.Name,
                        Quantity = a.Quantity,
                        MeasurementUnit = a.Product.MeasurementUnit.MeasurementUnitLanguage.FirstOrDefault(r=>r.Language.code == lang).Name
                    }).ToList()
                }).ToList()
            }).ToListAsync();
            return Ok(data);
        }

        //[HttpGet("{lang}/{id}")]
        //public async Task<ActionResult> GetWorkPlan(int id)
        //{
        //    WorkPlanUpdateDto data = await _context.WorkPlans.Select(s => new WorkPlanUpdateDto()
        //    {
        //        Id = s.Id,
        //        Name = s.Name,
        //        StartDate = s.StartDate,
        //        EndDate = s.EndDate,                
        //        WorkPlanTask = _context.WorkPlanTasks.Where(w => w.WorkPlan.Id == s.Id).Select(w => new WorkPlanTaskDto()
        //        {
        //            Id = w.Id,
        //            Name = w.Name,
        //            StartDate = w.StartDate,
        //            EndDate = w.EndDate,
        //            Responder =w.Respondent.Name
        //        }).ToList()
        //    }).FirstOrDefaultAsync(s=>s.Id == id);
        //    return Ok(data);
        //}


        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdateWorkPlan(int id, [FromBody] WorkPlanUpdateDto workPlan)
        //{
        //    int userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //    var logineduser = await _auth.VerifyUser(userid);
        //    if (id != workPlan.Id)
        //    {
        //        return BadRequest("Idler uygun gelmir");
        //    }

        //    var editedWorkPlan = await _context.WorkPlans.FirstOrDefaultAsync(s => s.Id == workPlan.Id);
        //    editedWorkPlan.Name = workPlan.Name;
        //    editedWorkPlan.StartDate = workPlan.StartDate;
        //    editedWorkPlan.EndDate = workPlan.EndDate;
        //    editedWorkPlan.Respondent = await _context.Workers.FirstOrDefaultAsync(s => s.Id == workPlan.RespondentId);
        //    _context.Entry(editedWorkPlan).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();


        //    WorkPlanActionLog log = new WorkPlanActionLog()
        //    {
        //        Name = workPlan.Name,
        //        StartDate = workPlan.StartDate,
        //        EndDate = workPlan.EndDate,
        //        Action = await _context.Actions.FirstOrDefaultAsync(s => s.Id == 2),
        //        Responder = await _context.Workers.FirstOrDefaultAsync(s => s.Id == workPlan.RespondentId),
        //        PerformingUser = logineduser,
        //        ActionTime = DateTime.Now,
        //        WorkPlan = editedWorkPlan,
        //    };
        //    await _context.WorkPlanActionLogs.AddAsync(log);
        //    await _context.SaveChangesAsync();


        //    foreach (var item in workPlan.WorkPlanTask)
        //    {
        //        if (item.Id == null || item.Id == 0 )
        //        {
        //            WorkPlanTask newtask = new WorkPlanTask()
        //            {
        //                Name = item.Name,
        //                StartDate = item.StartDate,
        //                EndDate = item.EndDate,
        //                WorkPlan = editedWorkPlan,
        //                Status = true
        //            };
        //            await _context.WorkPlanTasks.AddAsync(newtask);
        //            await _context.SaveChangesAsync();


        //            WorkPlanTaskActionLog newtasklog = new WorkPlanTaskActionLog()
        //            {
        //                name = item.Name,
        //                Startdate = item.StartDate,
        //                EndDate = item.EndDate,
        //                WorkPlanActionLog = log,
        //                Action = await _context.Actions.FirstOrDefaultAsync(s => s.Id == 1),
        //                WorkPlanTask = newtask
        //            };
        //            await _context.WorkPlanTaskActionLogs.AddAsync(newtasklog);
        //            await _context.SaveChangesAsync();
        //        }
        //        else {
        //            var editedTask = await _context.WorkPlanTasks.FirstOrDefaultAsync(s => s.Id == item.Id);
        //            editedTask.Name = item.Name;
        //            editedTask.StartDate = item.StartDate;
        //            editedTask.EndDate = item.EndDate;
        //            _context.Entry(editedTask).State = EntityState.Modified;
        //            await _context.SaveChangesAsync();

        //            WorkPlanTaskActionLog newtasklog = new WorkPlanTaskActionLog()
        //            {
        //                name = item.Name,
        //                Startdate = item.StartDate,
        //                EndDate = item.EndDate,
        //                WorkPlanActionLog = log,
        //                Action = await _context.Actions.FirstOrDefaultAsync(s => s.Id == 2),
        //                WorkPlanTask = editedTask
        //            };
        //            await _context.WorkPlanTaskActionLogs.AddAsync(newtasklog);
        //            await _context.SaveChangesAsync();
        //        }
        //    }

        //    return Ok();
        //}


    }
}