
using System.Collections.Generic;
using System.Threading.Tasks;
using MeslekiYeterlilik.Domain.Models;
    
namespace MeslekiYeterlilik.Domain.Services
{
    public interface IBelgeTipiService
    {
        Task<IEnumerable<BelgeTipi>> ListAsync();
    }
}