using Domain.Data.Context;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IInstallationService
    {
        Task InstallData(string collation);
    }
}
