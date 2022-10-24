using System.IO;

namespace Entitas
{

    public static class EntitasResources
    {
        public static string GetVersion()
        {
            using StreamReader streamReader = new StreamReader(typeof(Entity).Assembly.GetManifestResourceStream("version.txt"));
            return streamReader.ReadToEnd();
        }
    }
}