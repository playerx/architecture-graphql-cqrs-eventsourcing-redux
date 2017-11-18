
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public class UserController : CRUDController<User>
{
    public UserController(MainDbContext db) : base(db) { }
}