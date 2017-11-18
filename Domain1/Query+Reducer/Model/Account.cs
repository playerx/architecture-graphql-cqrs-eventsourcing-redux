
using System;

public class Account : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreateDate { get; set; }
    public Guid UserId { get; set; }
}