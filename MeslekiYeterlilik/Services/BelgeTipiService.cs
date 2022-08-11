
using System.Collections.Generic;
using System.Threading.Tasks;
using MeslekiYeterlilik.Domain.Models;
using MeslekiYeterlilik.Domain.Repositories;
using MeslekiYeterlilik.Domain.Services;

namespace MeslekiYeterlilik.Services
{
    public class BelgeTipiService : IBelgeTipiService
    {
        private readonly IBelgeTipiRepository _belgeTipiRepository;
        public BelgeTipiService(IBelgeTipiRepository belgeTipiRepository)
        {
            this._belgeTipiRepository = belgeTipiRepository;
        }
        
        public async Task<IEnumerable<BelgeTipi>> ListAsync()
        {
            return null;
            //return await _belgeTipiRepository.ListAsync();
        }
        
    }
}
