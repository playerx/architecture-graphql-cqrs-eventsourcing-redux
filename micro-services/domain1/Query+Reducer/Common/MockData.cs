using System;

public class MockData
{
    public static void AddTestData(MainDbContext db)
    {
        var firstUserId = Guid.NewGuid();
        var secondUserId = Guid.NewGuid();


        db.Users.Add(new User { Id = firstUserId, Name = "Ezeki", CreateDate = DateTime.Now });
        db.Users.Add(new User { Id = secondUserId, Name = "Babt", CreateDate = DateTime.Now });
        db.Users.Add(new User { Id = Guid.NewGuid(), Name = "Playerx", CreateDate = DateTime.Now });
        db.SaveChanges();


        db.Accounts.Add(new Account { Id = Guid.NewGuid(), Name = "Ezeki's Account", UserId = firstUserId, CreateDate = DateTime.Now });
        db.Accounts.Add(new Account { Id = Guid.NewGuid(), Name = "Babt's Primary Account", UserId = secondUserId, CreateDate = DateTime.Now });
        db.Accounts.Add(new Account { Id = Guid.NewGuid(), Name = "Babt's Additional Account", UserId = secondUserId, CreateDate = DateTime.Now });
        db.Accounts.Add(new Account { Id = Guid.NewGuid(), Name = "Babt's Whatever Account", UserId = secondUserId, CreateDate = DateTime.Now });
        db.SaveChanges();
    }
}