public class User
{
    public int id;
    public string name;
    public string email;
    public string email_verified_at;

    public User(int id, string name, string email, string email_verified_at)
    {
        this.id = id;
        this.name = name;
        this.email = email;
        this.email_verified_at = email_verified_at;
    }
}