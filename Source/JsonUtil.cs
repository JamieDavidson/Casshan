using System.IO;
using Newtonsoft.Json;

namespace Casshan
{
    internal static class JsonUtil
    {
        public static T LoadJsonFromFile<T>(string path) where T : class
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File at path {path} was not found");
            }

            var fileText = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(fileText);
        }

        public static void SaveJsonToFile(string path, object jsonBinding)
        {
            var serializer = new JsonSerializer();

            var file = File.Create(path);
            using (var sw = new StreamWriter(file))
            using (var jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;
                serializer.Serialize(jw, jsonBinding);
            }
        }
    }
}