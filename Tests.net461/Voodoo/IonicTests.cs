using System;
using System.Linq;
using System.IO;
using FluentAssertions;
using Voodoo.Tests.TestClasses;
using Xunit;

namespace Voodoo.Tests.Voodoo
{
    
    public class IonicTests
    {
        [Fact]
        public void WriteFile_FileExists_IsOk()
        {
            var path = IoNic.GetTempFileNameAndPath(".txt");

            IoNic.WriteFile("test1", path);
            IoNic.WriteFile("test2", path);

            IoNic.ReadFile(path).Should().Be("test2");
        }

        [Fact]
        public void PathCombineLocal_PathStartsWithSlash_ReturnsCombinedPath()
        {
            var fragment = "\\abc\\efg";
            var path = IoNic.PathCombineLocal(@"C:\", fragment);
            Assert.StartsWith("C", path);
        }

        [Fact]
        public void GetTempFileNameAndPath_NoValue_ReturnsValidPath()
        {
            var fileName = IoNic.GetTempFileNameAndPath(".abc");
            var path = Path.GetDirectoryName(fileName);
            Assert.True(Directory.Exists(path));
        }

        //[Fact]
        private void ShellExecute_textFile_OpensTextFileInAssociatedEditor()
        {
            var fileName = IoNic.GetTempFileNameAndPath();
            var text = "foo";
            IoNic.WriteFile(text, fileName);
            IoNic.ShellExecute(fileName);
        }

        [Fact]
        public void ReadFile_ValidFile_ReadsFile()
        {
            var fileName = IoNic.GetTempFileNameAndPath();
            var text = "foo";
            IoNic.WriteFile(text, fileName);
            var readText = IoNic.ReadFile(fileName);
            Assert.Equal(text, readText);
        }

        [Fact]
        public void AppendToFile_ValidFile_ReadsFile()
        {
            var fileName = IoNic.GetTempFileNameAndPath();
            var text = "foo";
            IoNic.WriteFile(text, fileName);
            IoNic.AppendToFile("bar", fileName);
            var readText = IoNic.ReadFile(fileName);
            Assert.Equal("foobar", readText);
        }

        [Fact]
        public static void CleanDir_ValidFolder_DeletesFolderAndFiles()
        {
            var fileName1 = IoNic.GetTempFileNameAndPath(".abc");
            var path = Path.GetDirectoryName(fileName1);
            var newPath = Path.Combine(path, "New");
            IoNic.MakeDir(newPath);
            Assert.True(Directory.Exists(newPath));
            var fileName = Path.Combine(newPath, Guid.NewGuid().ToString());
            IoNic.WriteFile("1", fileName);
            Assert.True(File.Exists(fileName));
            IoNic.ClearDir(newPath);
            Assert.Empty(Directory.GetFiles(newPath));
            IoNic.KillDir(newPath);
            Assert.False(Directory.Exists(newPath));
        }
    }
}