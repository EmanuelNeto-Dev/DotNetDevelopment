using Xunit;
using System.IO.Abstractions.TestingHelpers;
using ConsoleApp.DataProcessor;

namespace DataProcessor.Tests
{
    public class TextFileProcessorShould
    {
        [Fact]
        public void MakeTheSecondLineUpperCase()
        {
            //Create a mock input file
            var mockInputFile = new MockFileData("Line 1\nLine 2\nLine 3");

            //Setup mock file system starting state
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\rootTest\in\myFile.txt", mockInputFile);
            mockFileSystem.AddDirectory(@"c:\rootTest\out");

            //Create TextFileProcessor with mock file system
            var sut = new TextFileProcessor(@"c:\rootTest\in\myFile.txt"
                                            , @"c:\rootTest\out\myFile.txt" 
                                            , mockFileSystem);

            //Process Test File
            sut.Process();

            //Check mock file system for output file
            Assert.True(mockFileSystem.FileExists(@"c:\rootTest\out\myFile.txt"));

            //Check content of output file in mock file system
            MockFileData processedFile = mockFileSystem.GetFile(@"c:\rootTest\out\myFile.txt");
            string[] lines = processedFile.TextContents.SplitLines();
            Assert.Equal("Line 1", lines[0]);
            Assert.Equal("LINE 2", lines[1]);
            Assert.Equal("Line 3", lines[2]);
        }
    }
}
