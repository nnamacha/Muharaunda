using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Infrastructure.Implementation.CosmoDB
{
    public interface ICosmosUtilities
    {
        Task<Container> GetContainer(string containerId);
    }

    public class CosmosUtilities : ICosmosUtilities
    {
        private readonly Database _dataBase;
        private readonly IConfiguration _configuration;
        

        public CosmosUtilities(Database dataBase, IConfiguration configuration)
        {
            _dataBase = dataBase;
            _configuration = configuration;
        }

        public async Task<Container> GetContainer(string containerId)
        {

            var containerProperties = new ContainerProperties()
            {
                Id = containerId,
                PartitionKeyPath = _configuration["CosmosDB:PartitionKeyPath"],

            };

            return await _dataBase.CreateContainerIfNotExistsAsync(containerProperties);
        }
    }
}
