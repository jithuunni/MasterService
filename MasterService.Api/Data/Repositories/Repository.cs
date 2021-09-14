using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using MasterService.Api.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace MasterService.Api.Data.Repositories
{
    public class Repository<T>
    {
        private readonly Container container;

        public Repository(IConfiguration configuration, string code)
        {
            var cosmosDbSettings = configuration.GetSection("CosmosDbSettings").Get<CosmosDbSettings>();
            if (cosmosDbSettings.Containers?.Count() > 0)
            {
                code = code ?? "";
                DbContainer dbContainer = cosmosDbSettings.Containers.FirstOrDefault(ct => ct.Code.Equals(code, StringComparison.CurrentCultureIgnoreCase));

                if (dbContainer != null)
                {
                    var cosmosClient = new CosmosClient(cosmosDbSettings.ConnectionString);
                    Database database = cosmosClient.GetDatabase(cosmosDbSettings.DatabaseName);
                    container = database.GetContainer(dbContainer.Name);
                }
                else
                {
                    throw new Exception($"Container not found for the Code: {code}");
                }
            }
            else
            {
                throw new Exception($"Containers not defined in CosmosDbSettings");
            }
        }

        protected virtual async Task<T> CreateAsync(T item)
        {
            return await container.CreateItemAsync(item);
        }

        protected virtual async Task<bool> ReplaceAsync(T item, string id)
        {
            var itemResponse = await container.ReplaceItemAsync(item, id);
            return itemResponse.StatusCode == HttpStatusCode.OK;
        }

        protected virtual async Task<List<T>> ReadAsysnc(string query)
        {
            var entities = new List<T>();
            var queryDefinition = new QueryDefinition(query);
            FeedIterator<T> feedIterator = container.GetItemQueryIterator<T>(queryDefinition);
            
            while (feedIterator.HasMoreResults)
            {
                FeedResponse<T> feedResponse = await feedIterator.ReadNextAsync();
                foreach (T item in feedResponse)
                {
                    entities.Add(item);
                }
            }
            return entities;
        }

        protected virtual async Task<bool> RemoveAsync(string id, string partitionKey)
        {
            var itemResponse = await container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
            return itemResponse.StatusCode == HttpStatusCode.NoContent;
        }
    }// class ends
}
