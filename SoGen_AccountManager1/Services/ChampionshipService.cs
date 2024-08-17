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
                Founded = championshipDTO.Founded,
                UserId = championshipDTO.UserId
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

        public async Task<IEnumerable<Championship>> GetChampionshipsByUserId(int userId)
        {
            return await _championshipRepository.GetChampionshipByUserId(userId);
        } 

        public async Task<Championship> EditChampionship(Championship championship)
        {
            var championshipToUpdate = await _championshipRepository.GetChampionshipById(championship.Id);

            if(championshipToUpdate != null)
            {
                championshipToUpdate.Name = championship.Name;
                championshipToUpdate.Country = championship.Country;

                await _championshipRepository.UpdateChampionshipAsync(championshipToUpdate);
            }

             return championship;
        }

        public async Task<bool> DeleteChampionshipAsync(Championship championship)
        {
            bool championshipDeleted = true;
            var deleteChampionship = await _championshipRepository.GetChampionshipById(championship.Id);

            if(deleteChampionship != null)
            {
                await _championshipRepository.DeleteChampionshio(deleteChampionship);
            }
            else
            {
                championshipDeleted = false;
            }

            return championshipDeleted;
        }

        
    }
    
}