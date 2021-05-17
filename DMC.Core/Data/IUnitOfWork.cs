using System.Threading.Tasks;

namespace DMC.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}