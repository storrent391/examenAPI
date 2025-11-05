using System;
using System.IO;

namespace Spotify.Services;
public class Metadades
{   
    private static async Task LlegirMetadades(List<string> classificacio)
{
    var tasques = new List<Task>();

    for (int i = 1; i <= NumFiles; i++)
    {
       
        tasques.Add(Task.Run(() => CursaCamellIndividual(nom, classificacio)));
    }

    await Task.WhenAll(tasques); 
}
    public MediaService()
    {
        _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

        if (!Directory.Exists(_uploadsFolder))
            Directory.CreateDirectory(_uploadsFolder);
    }

    public static void Main(string[] args, Guid songId, IFormFile file)
    {
        String filePath = await SaveImage(songId, file);
        


        string nombreCompleto = Path.GetFileName(filePath);
        Console.WriteLine($"Nombre completo: {nombreCompleto}");


        string nombreSinExtension = Path.GetFileNameWithoutExtension(filePath);
        Console.WriteLine($"Nombre sin extensión: {nombreSinExtension}");
    }
    private static async Task<string> SaveImage(Guid id, IFormFile image)
    {
        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        string fileName = $"{id}_{Path.GetFileName(image.FileName)}";
        string filePath = Path.Combine(uploadsFolder, fileName);

        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        return filePath;
    }
}
