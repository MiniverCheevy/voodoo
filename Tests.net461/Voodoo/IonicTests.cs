using System;
using System.Linq;
using System.IO;
using FluentAssertions;
using Voodoo.Tests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Voodoo.Tests.Voodoo
{
    [TestClass]
    public class IonicTests
    {
        [TestMethod]
        public void WriteFile_FileExists_IsOk()
        {
            var path = IoNic.GetTempFileNameAndPath(".txt");

            IoNic.WriteFile("test1", path);
            IoNic.WriteFile("test2", path);

            IoNic.ReadFile(path).Should().Be("test2");
        }

        [TestMethod]
        public void PathCombineLocal_PathStartsWithSlash_ReturnsCombinedPath()
        {
            var fragment = "\\abc\\efg";
            var path = IoNic.PathCombineLocal(@"C:\", fragment);
            Assert.IsTrue(path.StartsWith("C"));
        }

        [TestMethod]
        public void GetTempFileNameAndPath_NoValue_ReturnsValidPath()
        {
            var fileName = IoNic.GetTempFileNameAndPath(".abc");
            var path = Path.GetDirectoryName(fileName);
            Assert.IsTrue(Directory.Exists(path));
        }

        //[TestMethod]
        public void ShellExecute_textFile_OpensTextFileInAssociatedEditor()
        {
            var fileName = IoNic.GetTempFileNameAndPath();
            var text = "foo";
            IoNic.WriteFile(text, fileName);
            IoNic.ShellExecute(fileName);
        }

        [TestMethod]
        public void ReadFile_ValidFile_ReadsFile()
        {
            var fileName = IoNic.GetTempFileNameAndPath();
            var text = "foo";
            IoNic.WriteFile(text, fileName);
            var readText = IoNic.ReadFile(fileName);
            Assert.AreEqual(text, readText);
        }

        [TestMethod]
        public void AppendToFile_ValidFile_ReadsFile()
        {
            var fileName = IoNic.GetTempFileNameAndPath();
            var text = "foo";
            IoNic.WriteFile(text, fileName);
            IoNic.AppendToFile("bar", fileName);
            var readText = IoNic.ReadFile(fileName);
            Assert.AreEqual("foobar", readText);
        }

        [TestMethod]
        public static void CleanDir_ValidFolder_DeletesFolderAndFiles()
        {
            var fileName1 = IoNic.GetTempFileNameAndPath(".abc");
            var path = Path.GetDirectoryName(fileName1);
            var newPath = Path.Combine(path, "New");
            IoNic.MakeDir(newPath);
            Assert.IsTrue(Directory.Exists(newPath));
            var fileName = Path.Combine(newPath, Guid.NewGuid().ToString());
            IoNic.WriteFile("1", fileName);
            Assert.IsTrue(File.Exists(fileName));
            IoNic.ClearDir(newPath);
            Assert.AreEqual(0, Directory.GetFiles(newPath).Count());
            IoNic.KillDir(newPath);
            Assert.IsFalse(Directory.Exists(newPath));
        }
    }
}