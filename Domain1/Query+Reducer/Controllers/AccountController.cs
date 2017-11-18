
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

[Route("[controller]")]
public class AccountController : CRUDController<Account>
{
    public AccountController(MainDbContext db) : base(db) { }


    protected override Account GetFilters(IQueryCollection query)
    {
        var userIdString = query["userId"].FirstOrDefault();
        var userId = default(Guid);

        if (userIdString != null)
            Guid.TryParse(userIdString, out userId);

        return new Account
        {
            UserId = userId
        };
    }

    protected override bool FilterEntityBeforeSelect(Account filter, Account entity)
    {
        return filter.UserId == Guid.Empty || filter.UserId == entity.UserId;
    }
}