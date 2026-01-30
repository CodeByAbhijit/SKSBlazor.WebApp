#nullable enable
using System;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string? Status { get; set; }
}