using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
class TidyProjectFiles : AssetPostprocessor
{
    const string KeptPrefix = "Assembly-CSharp";
    static string OnGeneratedSlnSolution(string path, string content)
    {
        return Regex.Replace(content, @"^[ \t]*<Project Path=""(?!" + KeptPrefix + @")[^""]*"" />\r?\n", "", RegexOptions.Multiline);
    }
    static string OnGeneratedCSProject(string path, string content)
    {
        if (!Path.GetFileName(path).StartsWith(KeptPrefix)) return content;
        return Regex.Replace(content, @"^([ \t]*)<ProjectReference Include=""(?!" + KeptPrefix + @")([^""]+)\.csproj"" />(\r?\n)",
            m =>
            {
                string indent = m.Groups[1].Value, name = m.Groups[2].Value, newline = m.Groups[3].Value;
                return indent + "<Reference Include=\"" + name + "\">" + newline
                    + indent + "  <HintPath>Library\\ScriptAssemblies\\" + name + ".dll</HintPath>" + newline
                    + indent + "  <Private>False</Private>" + newline
                    + indent + "</Reference>" + newline;
            }, RegexOptions.Multiline);
    }
    static void OnGeneratedCSProjectFiles()
    {
        foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csproj"))
        {
            if (!Path.GetFileName(file).StartsWith(KeptPrefix)) File.Delete(file);
        }
    }
}
