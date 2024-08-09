using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Repositories.Interface.IRepository;
using SoGen_AccountManager1.Repositories.Interface.IService;

namespace SoGen_AccountManager1.Services.ChampionshipService
{
    public class ChampionshipService : IChampionshipService {

        private readonly IChampionshipRepository _championshipRepository;
        public ChampionshipService(IChampionshipRepository championshipRepository )
        {
            _championshipRepository = championshipRepository;
        }

        public async Task<Championship> AddChampionshipAsync(ChampionshipDTO championshipDTO)
        {
            var Championship = new Championship
            {
                Name = championshipDTO.Name,
                Country = championshipDTO.Country,
                Founded = championshipDTO.Founded
            };

            return await _championshipRepository.AddChampionship(Championship);
        }

        public async Task<IEnumerable<Championship>> GetAllChampionships()
        {
            return await _championshipRepository.GetAllChampionshipsAsync();
        }

        public async Task<Championship> GetChampionshipById(int championshipId)
        {
            return await _championshipRepository.GetChampionshipById(championshipId);
        }
    }
    
}