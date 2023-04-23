using Microsoft.AspNetCore.Http;

namespace EduHome.Business.Extensions;
public static class FileExtension
{
    public static bool CheckSize(this IFormFile file,int kb)
        =>file.Length < kb*1024;
    public static bool CheckType(this IFormFile file, string type)
        => file.ContentType.Contains(type);


    public static async Task<string> SaveFile(this IFormFile file,string path)
    {
        string name = Guid.NewGuid() + file.FileName.Substring(file.FileName.LastIndexOf("."));
        path = Path.Combine(path,name);
        using(FileStream fs = new(path, FileMode.Create))
        {
            await file.CopyToAsync(fs);
        }
        return name;
    }
}


