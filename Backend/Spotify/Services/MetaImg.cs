using System;
using System.IO;

public class Ejemplo
{   
    public MediaService()
    {
        _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

        if (!Directory.Exists(_uploadsFolder))
            Directory.CreateDirectory(_uploadsFolder);
    }

    public static void Main(string[] args, )
    {
        String filePath = await SaveImage(songId, file);
        string rutaDelArchivo = @"C:\Users\Usuario\Documentos\mi_archivo.txt";


        string nombreCompleto = Path.GetFileName(rutaDelArchivo);
        Console.WriteLine($"Nombre completo: {nombreCompleto}");


        string nombreSinExtension = Path.GetFileNameWithoutExtension(rutaDelArchivo);
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
