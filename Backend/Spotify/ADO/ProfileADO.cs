using Microsoft.Data.SqlClient;
using Spotify.Services;
using Spotify.Model;
using Spotify.Encryption;
namespace Spotify.Repository;

public class ProfileADO
{


    public static void Insert(DatabaseConnection dbConn, Profile profile)
    {
    
    dbConn.Open();
    string sql = @"INSERT INTO Profiles (Id, Name, Description, Status, User_Id)
                   VALUES (@Id, @Name, @Description, @Status, @User_Id)";
    using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
    cmd.Parameters.AddWithValue("@Id", profile.Id);
    cmd.Parameters.AddWithValue("@Name", profile.Name);
    cmd.Parameters.AddWithValue("@Description", profile.Description);
    cmd.Parameters.AddWithValue("@Status", profile.Status);
    cmd.Parameters.AddWithValue("@User_Id", profile.User_Id);
    
    cmd.ExecuteNonQuery();
    
    dbConn.Close();
}


    public static List<Profile> GetAll(DatabaseConnection dbConn)
    {
        List<Profile> list = new();
        dbConn.Open();

        string sql = "SELECT Id, Name, Description, Status FROM Profiles";
        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new Profile
            {
                Id = reader.GetGuid(0),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                Status = reader.GetString(3),
                User_Id = reader.GetGuid(4)
            });
        }

        dbConn.Close();
        return list;
    }

    public static Profile? GetById(DatabaseConnection dbConn, Guid id)
    {
        dbConn.Open();
        string sql = "SELECT Id, Name, Description, Status, User_Id FROM Profiles WHERE User_Id = @User_Id";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@User_Id", id);

        using SqlDataReader reader = cmd.ExecuteReader();
        Profile? profile = null;

        if (reader.Read())
        {
                profile = new Profile
            {
                Id = reader.GetGuid(0),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                Status = reader.GetString(3),
                User_Id = reader.GetGuid(4)

            };
        }

        dbConn.Close();
        return profile;
    }

    public static void Update(DatabaseConnection dbConn, Profile profile)
    {
        dbConn.Open();

        string sql = @"UPDATE Profiles
                       SET Name = @Name,
                           Description = @Description,
                           Status = @Status
                       WHERE Id = @Id";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Id", profile.Id);
        cmd.Parameters.AddWithValue("@Name", profile.Name);
        cmd.Parameters.AddWithValue("@Description", profile.Description);
        cmd.Parameters.AddWithValue("@Status", profile.Status);

        cmd.ExecuteNonQuery();
        dbConn.Close();
    }

    public static bool Delete(DatabaseConnection dbConn, Guid id)
    {
        dbConn.Open();

        string sql = @"DELETE FROM Profiles WHERE Id = @Id";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Id", id);

        int rows = cmd.ExecuteNonQuery();
        dbConn.Close();

        return rows > 0;
    }
}
