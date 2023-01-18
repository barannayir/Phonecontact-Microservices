using AutoMapper;
using ReportService.Entities.Dtos;
using ReportService.Mapping;
using ReportService.Repositories.Interfaces;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportModel = ReportService.Entities.Report;

namespace ReportServiceTests.Services
{
    internal class ReportTestService : IReportRepository
    {
        private readonly List<ReportModel> contextDb;
        private readonly IMapper _mapper;

        public ReportTestService()
        {
            contextDb = new List<ReportModel>() { new ReportModel() { Id = "", FilePath = "Parth1", CreatedDate = DateTime.Now, Status = ReportService.Entities.ReportStatusType.WAITING },
            new ReportModel() { Id = "63c849534431c7d93359246f", FilePath = "Parth2", CreatedDate = DateTime.Now.AddDays(-1), Status = ReportService.Entities.ReportStatusType.INPROGRESS },
            new ReportModel() { Id = "63c8495baf66b9cddd050827", FilePath = "Parth3", CreatedDate = DateTime.Now.AddDays(-2), Status = ReportService.Entities.ReportStatusType.COMPLETED },
            new ReportModel() { Id = "63c8496163258c362070c400", FilePath = "Parth4", CreatedDate = DateTime.Now.AddDays(-3), Status = ReportService.Entities.ReportStatusType.FAILED },};

            var config = new MapperConfiguration(cfg => cfg.AddProfile<GeneralMapper>());
            _mapper = config.CreateMapper();
        }

        public async Task<Response<ReportDto>> CreateAsync()
        {
            ReportModel report = new ReportModel { Status = ReportService.Entities.ReportStatusType.WAITING, CreatedDate = DateTime.Now.ToUniversalTime(), FilePath = "NoPath" };
            contextDb.Add(report);
            return Response<ReportDto>.Success(_mapper.Map<ReportDto>(report), 200);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var report = contextDb.FirstOrDefault(c => c.Id == id);
            var result = contextDb.Remove(report);
            if (!result)
            {
                return Response<NoContent>.Fail("Report not found!", 404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<List<ReportDto>>> GetAllAsync()
        {
            var report = contextDb;
            return Response<List<ReportDto>>.Success(_mapper.Map<List<ReportDto>>(report), 200);
        }

        public async Task<Response<ReportDto>> GetByIdAsync(string id)
        {
            var report = contextDb.FirstOrDefault(x => x.Id == id);
            if (report == null)
            {
                return Response<ReportDto>.Fail("Report not found!", 404);
            }
            return Response<ReportDto>.Success(_mapper.Map<ReportDto>(report), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(ReportDto reportDto)
        {
            var updateReport = _mapper.Map<ReportModel>(reportDto);
            var oldReport = contextDb.FirstOrDefault(x => x.Id == updateReport.Id);

            if (oldReport == null)
            {
                return Response<NoContent>.Fail("Report not found!", 404);
            }

            contextDb.Remove(oldReport);
            contextDb.Add(updateReport);
            return Response<NoContent>.Success(204);
        }
    }
}
