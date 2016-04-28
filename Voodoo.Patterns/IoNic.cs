using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
#if DNXCORE50
//using Microsoft.AspNet.Hosting;
//using Microsoft.AspNet.Http;
#endif

#if (!PCL)
namespace Voodoo
{
    public static class IoNic
    {
        public static bool IsWebHosted
        {
            get
            {
#if DNX40 || DNX45 || DNX451 || DNX452 || DNX46
                return System.Web.HttpContext.Current == null;

#elif DNXCORE50
                //return string.IsNullOrWhiteSpace(HostingEnvironment.WebRootPath)
                //    ? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                //    : HostingEnvironment.Virtual;
                throw new NotImplementedException();
#endif                
            }
        }
            public static string GetApplicationRootDirectory()
        {
#if DNX40 || DNX45 || DNX451 || DNX452 || DNX46
                return System.Web.HttpContext.Current == null
                ? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                : System.Web.HttpContext.Current.Server.MapPath(".");

#elif DNXCORE50
            throw new NotImplementedException();
            //return string.IsNullOrWhiteSpace(HostingEnvironment.WebRootPath)
            //    ? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            //    : HostingEnvironment.Virtual;
#endif
            

        }

		/// <summary>
		/// Strips leading slashes from the every path except the first so that combining "C:\" and "\abc\def" yeilds "C:\abc\def", System.IO.Path.Combine would yeild "\abc\def".  
		/// </summary>
		public static string PathCombineLocal(params string[] paths)
        {
			if (paths.Length <= 1)
				return string.Empty;
			var cleanPaths = new List<string> { paths[0] };
			var rest = paths.Skip(1).ToArray();

			foreach (var p in rest)
			{
				var path = p;
				path = path.TrimStart(Path.DirectorySeparatorChar);
				path = path.TrimStart(Path.AltDirectorySeparatorChar);
				cleanPaths.Add(path);
			}
			return Path.Combine(cleanPaths.ToArray());
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
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        Arguments = arguments
                    }
            };
            p.Start();
            var output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return output;
        }

        public static void ShellExecute(string path, bool asAdmin = false)
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
            }
        }
		
		public static void WriteFile(string fileContents, string fileName)
		{
			StreamWriter sw = null;

			verifyDirectory(fileName);
			
			var exists = File.Exists(fileName);
#if DNXCORE50
			exists = false;
#endif
#if !DNXCORE50
			if (exists)
			{
				File.SetAttributes(fileName, FileAttributes.Archive);
				sw = new StreamWriter(fileName);
			}
#endif
			if (!exists)
			{
				KillFile(fileName);
				sw = File.CreateText(fileName);
			}

			using (sw)
            {
                sw.Write(fileContents);
                sw.Flush();
            }


		}

	    private static void verifyDirectory(string fileName)
	    {
		    var directory = Path.GetDirectoryName(fileName);
		    if (!Directory.Exists(directory))
			    MakeDir(directory);
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
#endif