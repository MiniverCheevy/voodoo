using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Voodoo.Tests.Voodoo.Infrastructure
{
    
    public class IoNicSmokeTests
    {
        [Fact]
        public void GetTempFileNameAndPath_NoValue_ReturnsValidPath()
        {
            var fileName = IoNic.GetTempFileNameAndPath(".abc");
            var path = Path.GetDirectoryName(fileName);
            Assert.True(Directory.Exists(path));
        }

        //[Fact]
        public void ShellExecute_textFile_OpensTextFileInAssociatedEditor()
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
            Assert.Equal(0, Directory.GetFiles(newPath).Count());
            IoNic.KillDir(newPath);
            Assert.False(Directory.Exists(newPath));
        }
    }
}