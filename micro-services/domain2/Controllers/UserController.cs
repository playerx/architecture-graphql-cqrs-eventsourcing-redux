

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

[Route("[controller]/[action]")]
public class UserController : Controller
{
    const string EndpointUri = "https://cosmosdemox.documents.azure.com:443/";
    const string PrimaryKey = "HHsOV8exbku4UgM45FxpfkE4ePERRWvzEvylJ0O0bm5jtNrnud33jfaCP6DbIWm3IOBe44doJeTFhBYBd25Lug==";
    readonly DocumentClient db;

    public UserController()
    {
        var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        this.db = new DocumentClient(new Uri(EndpointUri), PrimaryKey, jsonSerializerSettings);
    }

    public async Task<dynamic> Create()
    {
        var x = new User
        {
            Id = Guid.NewGuid(),
            Name = "Ezeki",
            Accounts = new List<Account>
            {
                new Account { Id = Guid.NewGuid(), Name = "Primary Account" },
                new Account { Id = Guid.NewGuid(), Name = "Additional Account" },
            }
        };

        var result = await db.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("MainDB", "Items"), x);
        return result;
    }

    public dynamic GetList()
    {
        var query = db.CreateDocumentQuery<User>(UriFactory.CreateDocumentCollectionUri("MainDB", "Items"));
        return query.ToList();
    }
}