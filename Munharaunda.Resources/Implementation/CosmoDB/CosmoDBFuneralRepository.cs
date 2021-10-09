using Microsoft.Azure.Cosmos;
using Muharaunda.Core.Models;
using Munharaunda.Core.Models;
using Munharaunda.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Infrastructure.Implementation.CosmoDB
{
    public class CosmoDBFuneralRepository : IFuneralRepository
    {
        private readonly Database _database;

        public CosmoDBFuneralRepository(Database database)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }
        public Task<ResponseModel<bool>> AuthoriseFuneralAsync(int FuneralId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Funeral>> CreateFuneralAsync(Funeral request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> DeleteFuneralAsync(int FuneralId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Funeral>> GetFuneralDetailsByFuneralIdAsync(int funeralId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Funeral>> GetFuneralDetailsByProfileIdAsync(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Funeral>> GetListOfActiveFuneralsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> UpdateFuneralAsync(Funeral Funeral)
        {
            throw new NotImplementedException();
        }
    }
}
