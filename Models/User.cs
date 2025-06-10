using System.ComponentModel.DataAnnotations.Schema;
using Projekt_sbd.Models;

public class User
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("USERNAME")]
    public string Username { get; set; }

    [Column("PASSWORD_HASH")]
    public string PasswordHash { get; set; }

    [Column("ROLE")]
    public string Role { get; set; }


}



