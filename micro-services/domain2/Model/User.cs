
using System;
using System.Collections.Generic;

public class User
{
    public string Type { get; } = "User";
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Account> Accounts { get; set; }
}