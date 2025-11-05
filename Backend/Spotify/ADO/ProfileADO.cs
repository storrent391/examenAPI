using Microsoft.Data.SqlClient;
using Spotify.Services;
using Spotify.Model;
using Spotify.Encryption;
namespace Spotify.Repository;

public class ProfileADO
{


    public static void Insert(DatabaseConnection dbConn, Profile profile)
{
    PasswordEncryption.ConvertPassword(profile);
    dbConn.Open();
    string sql = @"INSERT INTO Profiles (Id, Name, Password, Salt)
                   VALUES (@Id, @Name, @Password, @Salt)";
    using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
    cmd.Parameters.AddWithValue("@Id", profile.Id);
    cmd.Parameters.AddWithValue("@Name", profile.Name);
    cmd.Parameters.AddWithValue("@Password", profile.Password);
    cmd.Parameters.AddWithValue("@Salt", profile.Salt);
    cmd.ExecuteNonQuery();
    
    dbConn.Close();
}


    public static List<Profile> GetAll(DatabaseConnection dbConn)
    {
        List<Profile> list = new();
        dbConn.Open();

        string sql = "SELECT Id, Name, Password, Salt FROM Profiles";
        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new Profile
            {
                Id = reader.GetGuid(0),
                Name = reader.GetString(1),
                Password = reader.GetString(2),
                Salt = reader.GetString(3)
            });
        }

        dbConn.Close();
        return list;
    }

    public static Profile? GetById(DatabaseConnection dbConn, Guid id)
    {
        dbConn.Open();
        string sql = "SELECT Id, Name, Password, Salt FROM Profiles WHERE Id = @Id";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Id", id);

        using SqlDataReader reader = cmd.ExecuteReader();
        Profile? profile = null;

        if (reader.Read())
        {
                profile = new Profile
            {
                Id = reader.GetGuid(0),
                Name = reader.GetString(1),
                Password = reader.GetString(2),
                Salt = reader.GetString(3)
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
                           Password = @Password,
                           Salt = @Salt
                       WHERE Id = @Id";

        using SqlCommand cmd = new SqlCommand(sql, dbConn.sqlConnection);
        cmd.Parameters.AddWithValue("@Id", profile.Id);
        cmd.Parameters.AddWithValue("@Name", profile.Name);
        cmd.Parameters.AddWithValue("@Password", profile.Password);
        cmd.Parameters.AddWithValue("@Salt", profile.Salt);

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
