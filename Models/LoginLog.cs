using System.ComponentModel.DataAnnotations.Schema;
using Projekt_sbd.Models;

[Table("LOGIN_LOGS")]
public class LoginLog
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("USERNAME")]
    public string Username { get; set; }

    [Column("LOGIN_TIME")]
    public DateTime LoginTime { get; set; }

    [Column("IP_ADDRESS")]
    public string IpAddress { get; set; }
}
