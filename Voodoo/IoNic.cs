using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Voodoo
{
    public static class IoNic
    {
        public static string GetApplicationRootDirectory()
        {

            return System.Web.HttpContext.Current == null ? 
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) : 
                System.Web.HttpContext.Current.Server.MapPath(".");
        }

        public static string ResolveRelativePath(string path, string rootFolder = null)
        {
            rootFolder = rootFolder ?? GetApplicationRootDirectory();
            var newPath = Path.Combine(rootFolder, path);
            return Path.GetFullPath(newPath);
        }

        public static string GetTempFileNameAndPath(string fileExtensionNoDot = "txt")
        {
            var path = Environment.GetEnvironmentVariable("TEMP") ?? "c:\temp";
            path = Path.Combine(path, string.Format("{0}.{1}", Guid.NewGuid(), fileExtensionNoDot));
            return path;
        }


        public static string ExecuteAndReturnOutput(string path, string arguments)
        {
            var p = new Process
            {
                StartInfo =
                    new ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = false,
                        RedirectStandardOutput =true,
                        CreateNoWindow = true,
                        Arguments = arguments
                    }
            };
            p.Start();
            var output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return output;
        }

        public static void ShellExecute(string path)
        {
            var p = new Process
            {
                StartInfo =
                    new ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = true,
                        RedirectStandardOutput = false,
                        CreateNoWindow = false
                    }
            };
            p.Start();
        }

        public static string ReadFile(string fileName)
        {
            
            string buffer;
            using (var streamReader = File.OpenText(fileName))
            {
                buffer = streamReader.ReadToEnd();
            }

            return buffer;
        }

        public static void AppendToFile(string contents, string fileName)
        {
            if (!File.Exists(fileName))
                WriteFile(string.Empty, fileName);
            using (var sw = File.AppendText(fileName))
            {
                sw.Write(contents);
                sw.Flush();
                sw.Close();
            }
        }

        public static void WriteFile(string fileContents, string fileName)
        {
            var directory = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directory))
                MakeDir(directory);

            if (!File.Exists(fileName))
                KillFile(fileName);
            using (var sw = File.CreateText(fileName))
            {
                sw.Write(fileContents);
                sw.Flush();
                sw.Close();
            }
        }

        public static void MakeDir(string path)
        {
            if (Directory.Exists(path)) return;

            Directory.CreateDirectory(path);
        }

        public static void ClearDir(string path)
        {
            if (!Directory.Exists(path)) return;

            foreach (var f in Directory.GetFiles(path))
            {
                KillFile(f);
            }
            foreach (var d in Directory.GetDirectories(path))
            {
                ClearDir(d);
            }
        }

        public static void KillDir(string path)
        {
            if (!Directory.Exists(path)) return;

            foreach (var f in Directory.GetFiles(path))
            {
                KillFile(f);
            }
            foreach (var d in Directory.GetDirectories(path))
            {
                KillDir(d);
            }
            Directory.Delete(path);
        }

        public static void KillFile(string path)
        {
            if (File.Exists(path))
            {
                File.SetAttributes(path, FileAttributes.Archive);
                File.Delete(path);
            }
        }
    }
}