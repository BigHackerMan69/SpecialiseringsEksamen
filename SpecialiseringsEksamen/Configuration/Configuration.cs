using SpecialiseringsEksamen.AppSettings;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpecialiceringsEksamen.Configuration
{
    public static class Configuration
    {
        public static async Task<AppSettings> LoadSettingsAsync()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("appsetting.json");
            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<AppSettings>(json);
        }
    }
}
