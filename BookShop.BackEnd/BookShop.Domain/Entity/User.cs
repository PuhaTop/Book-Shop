namespace BookShop.Domain.Entity;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = default!;
    public byte[] PasswordSalt { get; set; } = default!;
}