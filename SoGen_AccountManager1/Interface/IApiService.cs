using System;
using System.Collections;
using System.Threading.Tasks;
using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Interface

{
	public interface IApiService
	{
        Task<IEnumerable> GetCountriesFromExternalApi();

    }
}

