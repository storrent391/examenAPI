using Spotify.Services;
using Spotify.Repository;
using Spotify.Model;
namespace Spotify.Endpoints;

public static class ProfileEndpoints
{
    public static void MapProfileEndpoints(this WebApplication app, DatabaseConnection dbConn)
    {
        

        
        app.MapGet("/profile{id}", (Guid id) =>
        {
            Profile profile = ProfileADO.GetById(dbConn, id);
            return profile is not null
                ? Results.Ok(profile)
                : Results.NotFound(new { message = $"Profile with Id {id} not found." });
        });

        // POST /profile
        app.MapPost("/profile", (ProfileRequest req) =>
        {
            Profile profile = new Profile
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                Description = req.Description,
                Status = req.Status,
                User_Id = req.User_Id,
            };

            ProfileADO.Insert(dbConn, profile);
            return Results.Created($"/profile/{profile.Id}", profile);
        });

        // PUT /profile/{id}
        app.MapPut("/profile/{id}", (Guid id, ProfileRequest req) =>
        {
            var existing = ProfileADO.GetById(dbConn, id);
            if (existing == null)
                return Results.NotFound();

            existing.Name = req.Name;
            existing.Description = req.Description;
            existing.Status = req.Status;

            ProfileADO.Update(dbConn, existing);
            return Results.Ok(existing);
        });

        // DELETE /profile/{id}
        app.MapDelete("/profile/{id}", (Guid Id) => ProfileADO.Delete(dbConn, Id) ? Results.NoContent() : Results.NotFound());
    }
}

public record ProfileRequest(Guid Id, string Name, string Description, int Status, Guid User_Id);