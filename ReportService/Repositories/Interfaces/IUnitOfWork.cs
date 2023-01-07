using ContactMicroService.Entities;
using System;
using System.Threading.Tasks;

namespace ReportService.Repositories.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {

        Task SaveAsync(Report report);
    }
}
