using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

[Owned]
public class ComplexTimestamp
{
    [Timestamp]
    public byte[] Timestamp { get; set; }
    
    [Timestamp]
    public uint Xmin { get; set; }
}