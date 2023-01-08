using AutoMapper;
using ContactMicroService.Data;
using MongoDB.Driver;
using ReportService.Entities;
using ReportService.Entities.Dtos;
using ReportService.Repositories.Interfaces;
using ReportService.Settings;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReportModel = ReportService.Entities.Report;

namespace ReportService.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ReportContext _context;
        private readonly IMapper _mapper;
        private readonly IMongoCollection<ReportModel> _reportCollection;

        public ReportRepository(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionStrings);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _reportCollection = database.GetCollection<ReportModel>(databaseSettings.ReportCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<ReportDto>>> GetAllAsync()
        {
            var reports = await _reportCollection.Find(x => true).ToListAsync();
            return Response<List<ReportDto>>.Success(_mapper.Map<List<ReportDto>>(reports), 200);
        }

        public async Task<Response<ReportDto>> GetByIdAsync(string id)
        {
            var report = await _reportCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (report == null)
            {
                return Response<ReportDto>.Fail("Report not found!", 404);
            }
            var reportDto = _mapper.Map<ContactWithCommunicationsDto>(report);

            return Response<ReportDto>.Success(_mapper.Map<ReportDto>(report), 200);
        }

        public async Task<Response<ReportDto>> CreateAsync()
        {
            var report = new Entities.Report { Status = ReportStatusType.WAITING, CreatedDate = DateTime.Now.ToUniversalTime(), FilePath = "NoPath" };
            await _reportCollection.InsertOneAsync(report);
            return Response<ReportDto>.Success(_mapper.Map<ReportDto>(report), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(ReportDto reportDto)
        {
            var updateReport = _mapper.Map<ReportModel>(reportDto);
            var result = await _reportCollection.FindOneAndReplaceAsync(x => x.Id == updateReport.Id, updateReport);
            if (result == null)
            {
                return Response<NoContent>.Fail("Report not found!", 404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _reportCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Fail("Report not found!", 404);
            }
            return Response<NoContent>.Success(204);
        }

        private bool ReportExists(int id)
        {
            return true;
        }
    }
}