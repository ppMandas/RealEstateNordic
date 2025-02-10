using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RealEstate.Repository.Entities;
using RealEstate.Repository.Interfaces;
using RealEstate.Service.Interfaces;
using RealEstate.Service.Models;
using System.Globalization;

namespace RealEstate.Service
{
    public class MunicipalityHandler : IMunicipalityHandler
    {
        private readonly ILogger<MunicipalityHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IMunicipalityRepository _municipalityRepository;

        public MunicipalityHandler(ILogger<MunicipalityHandler> logger,
            IMapper mapper, IMunicipalityRepository municipalityRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _municipalityRepository = municipalityRepository;
        }

        public int InsertMunicipalityTaxRecord(MunicipalityModel municipalityModel)
        {
            var municipalityEntity = _mapper.Map<MunicipalityEntity>(municipalityModel);

            try
            {
                return _municipalityRepository.InsertRecord(municipalityEntity);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while inserting record: {municipalityEntity}", municipalityEntity);
            }

            return 0;
        }

        public double GetMunicipalityTax(string municipality, DateOnly dateStart)
        {
            try
            {
                var municipalityEntities = _municipalityRepository.GetRecords(municipality, dateStart);
                var municipalityModels = _mapper.Map<List<MunicipalityModel>>(municipalityEntities);

                // Order by tax type precedence and then by Id.
                // Assumption: Daily > Weekly > Monthly > Yearly
                // Assumption: Bigger ID (latest inserted) takes precedence after tax type
                var municipalityModel = municipalityModels.OrderBy(model => model.TaxType).ThenByDescending(model => model.Id).FirstOrDefault();

                if (municipalityModel != default)
                    return municipalityModel.Tax;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while retrieving records by municipality: {municipality} and date: {dateStart}");
            }

            return 0.0;
        }

        public void InsertMunicipalityTaxRecords(IFormFile file)
        {
            try
            {
                var reader = new StreamReader(file.OpenReadStream());
                var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                csv.Context.RegisterClassMap<MunicipalityModelMap>();
                var municipalityModels = csv.GetRecords<MunicipalityModel>();
                var municipalityEntities = _mapper.Map<List<MunicipalityEntity>>(municipalityModels);

                foreach (var municipalityEntity in municipalityEntities)
                {
                    _municipalityRepository.InsertRecord(municipalityEntity);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while importing provided CSV file.");
            }
        }
    }
}
