using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IAuthRepository _auth;
        public CustomerController(DataContext context, IAuthRepository auth)
        {
            _context = context;

            _auth = auth;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromBody] CustomerDto customer)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var logineduser = await _auth.VerifyUser(id);

            var country = await _context.Country.FirstOrDefaultAsync(s => s.Id == customer.CountryId);
            if (country == null) return BadRequest("Sistemde bele bir olke yoxdur");


            var city = await _context.Cities.Where(s => s.Country.Id == country.Id).FirstOrDefaultAsync(s => s.Id == customer.CityId);
            if (city == null) return BadRequest("Sistemde bele bir Seher yoxdur");

            var paymentterm = await _context.PaymentTerms.FirstOrDefaultAsync(s => s.Id == customer.PaymentTermId);
            if (paymentterm == null) return BadRequest("Sistemde bele bir payment Term yoxdur");

            var advancepaymentterm = await _context.PaymentTerms.FirstOrDefaultAsync(s => s.Id == customer.AdvancePaymentTermId);
            if (advancepaymentterm == null) return BadRequest("Sistemde bele bir payment Term yoxdur");

            var paymentkind = await _context.PaymentKinds.FirstOrDefaultAsync(s => s.Id == customer.PaymentKindId);
            if (paymentkind == null) return BadRequest("Sistemde bele bir payment kind yoxdur");

            var advancepaymentkind = await _context.PaymentKinds.FirstOrDefaultAsync(s => s.Id == customer.AdvancePaymentTermId);
            if (advancepaymentkind == null) return BadRequest("Sistemde bele bir payment kind yoxdur");

            var deliveryterm = await _context.DeliveryTerms.FirstOrDefaultAsync(s => s.Id == customer.DeliveryTermId);
            if (deliveryterm == null) return BadRequest("Sistemde bele bir delivery term yoxdur");

            Customer _newCustomer = new Customer() {
                Name = customer.Name,
                LegalName = customer.LegalName,
                Industry = customer.Industry,
                Country = country,
                City = city,
                Address = customer.Address,
                ContactPerson = customer.ContactPerson,
                Email = customer.Email,
                Phone = customer.Phone,
                AgreementNumber = customer.AgreementNumber,
                AgreementDate = customer.AgreementDate,
                PaymentTerm = paymentterm,
                PaymentAmount = customer.PaymentAmount,
                PaymentKind = paymentkind,
                PaymentPeriod = customer.PaymentPeriod,
                AdvancePaymentTerm = advancepaymentterm,
                AdvancePaymentAmount = customer.AdvancePaymentAmount,
                AdvancePaymentKind = advancepaymentkind,
                AdvancePaymentPeriod = customer.AdvancePaymentPeriod,
                DeliveryTerm = deliveryterm,
                DeliveryPeriod = customer.DeliveryPeriod,
                Company = logineduser.Company,
                Status = true
            };

            _context.Customers.Add(_newCustomer);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpGet("{lang}")]
        public async Task<ActionResult> GetCustomer(string lang)
        {
            int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var logineduser = await _auth.VerifyUser(id);
            List<CustomerReadDto> datalist = await _context.Customers.Where(s => s.Company == logineduser.Company && s.Status == true).Select(s => new CustomerReadDto()
            {
                Id = s.Id,
                Name = s.Name,
                LegalName = s.LegalName,
                Industry = s.Industry,
                Country = s.Country.CountryLanguages.FirstOrDefault(w => w.Language.code == lang).Name,
                City = s.City.CityLangs.FirstOrDefault(w => w.Language.code == lang).Name,
                Address = s.Address,
                ContactPerson = s.ContactPerson,
                Email = s.Email,
                Phone = s.Phone,
                AgreementNumber = s.AgreementNumber,
                AgreementDate = s.AgreementDate,
                PaymentTerm = s.PaymentTerm.PaymentTermLangs.FirstOrDefault(w => w.Language.code == lang).Name,
                PaymentAmount = s.PaymentAmount,
                PaymentKind = s.PaymentKind.PaymentKindLanguages.FirstOrDefault(w => w.Language.code == lang).Name,
                PaymentPeriod = s.PaymentPeriod,
                AdvancePaymentTerm = s.AdvancePaymentTerm.PaymentTermLangs.FirstOrDefault(w => w.Language.code == lang).Name,
                AdvancePaymentAmount = s.AdvancePaymentAmount,
                AdvancePaymentKind = s.AdvancePaymentKind.PaymentKindLanguages.FirstOrDefault(w => w.Language.code == lang).Name,
                AdvancePaymentPeriod = s.AdvancePaymentPeriod,
                DeliveryTerm = s.DeliveryTerm.DeliveryTermLangs.FirstOrDefault(w => w.Language.code == lang).Name,
                DeliveryPeriod = s.DeliveryPeriod
            }).ToListAsync();

            return Ok(datalist);
        }

        [HttpGet("{lang}/{id}")]
        public async Task<ActionResult> Customer(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(s => s.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            CustomerDto data = await _context.Customers.Select(s => new CustomerDto()
            {
                Id = s.Id,
                Name = s.Name,
                LegalName = s.LegalName,
                Industry = s.Industry,
                CountryId = s.Country.Id,
                CityId = s.City.Id,
                Address = s.Address,
                ContactPerson = s.ContactPerson,
                Email = s.Email,
                Phone = s.Phone,
                AgreementNumber = s.AgreementNumber,
                AgreementDate = s.AgreementDate,
                PaymentTermId = s.PaymentTerm.Id,
                PaymentAmount = s.PaymentAmount,
                PaymentKindId = s.PaymentKind.Id,
                PaymentPeriod = s.PaymentPeriod,
                AdvancePaymentTermId = s.AdvancePaymentTerm.Id,
                AdvancePaymentAmount = s.AdvancePaymentAmount,
                AdvancePaymentKindId = s.AdvancePaymentKind.Id,
                AdvancePaymentPeriod = s.AdvancePaymentPeriod,
                DeliveryTermId = s.DeliveryTerm.Id,
                DeliveryPeriod = s.DeliveryPeriod
            }).FirstOrDefaultAsync(s => s.Id == id);


            return Ok(data);
        }


        [HttpPut("{Id}")]
        public async Task<ActionResult> EditCustomer(int id, [FromBody] CustomerDto customer)
        {

            if (id != customer.Id)
            {
                return BadRequest("Idler duz deyil");
            }

            var country = await _context.Country.FirstOrDefaultAsync(s => s.Id == customer.CountryId);
            if (country == null) return BadRequest("Sistemde bele bir olke yoxdur");


            var city = await _context.Cities.Where(s => s.Country.Id == country.Id).FirstOrDefaultAsync(s => s.Id == customer.CityId);
            if (city == null) return BadRequest("Sistemde bele bir Seher yoxdur");

            var paymentterm = await _context.PaymentTerms.FirstOrDefaultAsync(s => s.Id == customer.PaymentTermId);
            if (paymentterm == null) return BadRequest("Sistemde bele bir payment Term yoxdur");

            var advancepaymentterm = await _context.PaymentTerms.FirstOrDefaultAsync(s => s.Id == customer.AdvancePaymentTermId);
            if (advancepaymentterm == null) return BadRequest("Sistemde bele bir payment Term yoxdur");

            var paymentkind = await _context.PaymentKinds.FirstOrDefaultAsync(s => s.Id == customer.PaymentKindId);
            if (paymentkind == null) return BadRequest("Sistemde bele bir payment kind yoxdur");

            var advancepaymentkind = await _context.PaymentKinds.FirstOrDefaultAsync(s => s.Id == customer.AdvancePaymentTermId);
            if (advancepaymentkind == null) return BadRequest("Sistemde bele bir payment kind yoxdur");

            var deliveryterm = await _context.DeliveryTerms.FirstOrDefaultAsync(s => s.Id == customer.DeliveryTermId);
            if (deliveryterm == null) return BadRequest("Sistemde bele bir delivery term yoxdur");

            var cust = await _context.Customers.FirstOrDefaultAsync(s => s.Id == customer.Id);

            cust.Name = customer.Name;
            cust.LegalName = customer.LegalName;
            cust.Industry = customer.Industry;
            cust.Country = country;
            cust.City = city;
            cust.Address = customer.Address;
            cust.ContactPerson = customer.ContactPerson;
            cust.Email = customer.Email;
            cust.Phone = customer.Phone;
            cust.AgreementNumber = customer.AgreementNumber;
            cust.AgreementDate = customer.AgreementDate;
            cust.PaymentTerm = paymentterm;
            cust.PaymentAmount = customer.PaymentAmount;
            cust.PaymentKind = paymentkind;
            cust.PaymentPeriod = customer.PaymentPeriod;
            cust.AdvancePaymentTerm = advancepaymentterm;
            cust.AdvancePaymentAmount = customer.AdvancePaymentAmount;
            cust.AdvancePaymentKind = advancepaymentkind;
            cust.AdvancePaymentPeriod = customer.AdvancePaymentPeriod;
            cust.DeliveryTerm = deliveryterm;
            cust.DeliveryPeriod = customer.DeliveryPeriod;

            _context.Entry(cust).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var cust = await _context.Customers.FirstOrDefaultAsync(s => s.Id == id);
            if (cust == null)
            {
                return NotFound();
            }
            cust.Status = false;
            _context.Entry(cust).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}