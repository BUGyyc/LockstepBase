using System;
using System.IO;
using System.Linq;

namespace Entitas.Migration;

public static class MigrationUtils
{
	public static MigrationFile[] GetFiles(string path, string searchPattern = "*.cs")
	{
		return (from p in Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories)
			select new MigrationFile(p, File.ReadAllText(p))).ToArray();
	}

	public static void WriteFiles(MigrationFile[] files)
	{
		foreach (MigrationFile migrationFile in files)
		{
			Console.WriteLine("Migrating: " + migrationFile.fileName);
			File.WriteAllText(migrationFile.fileName, migrationFile.fileContent);
		}
	}
}
