using Microsoft.AspNetCore.Mvc;
using RealEstate.Repository.Enums;
using RealEstate.Service.Interfaces;
using RealEstate.Service.Models;
using RealEstateNordic.Dtos;

namespace RealEstateNordic.Controllers
{
    [ApiController]
    [Route("api/municipalities")]
    public class MunicipalityController : ControllerBase
    {
        private readonly ILogger<MunicipalityController> _logger;
        private readonly IMunicipalityHandler _municipalityHandler;

        public MunicipalityController(ILogger<MunicipalityController> logger,
            IMunicipalityHandler municipalityHandler)
        {
            _logger = logger;
            _municipalityHandler = municipalityHandler;
        }

        [HttpPost("tax")]
        public ActionResult InsertMunicipalityTax(string municipality, DateOnly date, TaxType taxType, double tax)
        {
            var municipalityModel = new MunicipalityModel
            {
                Municipality = municipality,
                DateStart = date,
                TaxType = taxType,
                Tax = tax
            };

            var insertResult = _municipalityHandler.InsertMunicipalityTaxRecord(municipalityModel);

            // Case: Failed to insert new record in DB, return 409
            if (insertResult == 0)
                return Conflict(new { message = "Failed to insert new municipality tax record. Please resubmit request." });

            return Created();
        }

        [HttpGet("tax")]
        public ActionResult<MunicipalityTaxResponseDto> GetMunicipalityTax(string municipality, DateOnly date)
        {
            var tax = _municipalityHandler.GetMunicipalityTax(municipality, date);

            // Case: Tax not found for given municipality and date, return 404
            if (tax == 0.0)
                return NotFound(new { message = "No tax record found for provided municipality and date." });

            // Return 200 with Json object containing Tax value
            return new MunicipalityTaxResponseDto
            {
                Tax = tax
            };
        }

        [HttpPost("tax/csv")]
        public ActionResult UploadCsv()
        {
            if (!Request.Form.Files.Any())
                return Ok();

            var file = Request.Form.Files.First();
            if (file.ContentType != "text/csv")
                return BadRequest(new { message = "Provided file is not of type CSV. Please resubmit request using a CSV file." });

            _municipalityHandler.InsertMunicipalityTaxRecords(file);

            return Created();
        }
    }
}
