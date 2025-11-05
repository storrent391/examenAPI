namespace Spotify.Model;

public class Profile
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }    
    
    public string Status { get; set; }

    public Guid User_Id { get; set; }
}
