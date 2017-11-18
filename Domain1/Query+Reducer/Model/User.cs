
using System;

public class User : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreateDate { get; set; }
}