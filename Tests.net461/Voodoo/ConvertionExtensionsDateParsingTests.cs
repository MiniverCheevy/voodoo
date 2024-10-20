using System;
using System.Diagnostics;
using Xunit;

namespace Voodoo.Tests.Voodoo
{
    
    public class ConvertionExtensionsDateParsingTests
    {
        [Fact]
        public void To_ValidDate_CompareToParseAndConvert()
        {
            const string test = "2009/10/10";

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < 1000; i++)
            {
                var result = DateTime.Parse(test);
            }
            stopwatch.Stop();
            Debug.WriteLine("Parse = " + stopwatch.Elapsed);
            stopwatch.Restart();
            for (var i = 0; i < 1000; i++)
            {
                var result = Convert.ToDateTime(test);
            }
            stopwatch.Stop();
            Debug.WriteLine("Convert = " + stopwatch.Elapsed);
        }

        [Fact]
        public void To_DateStringWithSpaces_CompareToParse()
        {
            const string test = "  2009/10/10        ";

            To_DateString_CompareToParse(test);
        }

        [Fact]
        public void To_DateWithnoSpaces_CompareToParse()
        {
            const string test = "2009/10/10";

            To_DateString_CompareToParse(test);
        }

        [Fact]
        public void To_InvalidDateString_CompareToParse()
        {
            const string test = "asdf";

            To_DateString_CompareToParse(test);
        }

        private static void To_DateString_CompareToParse(string test)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < 1000; i++)
            {
                try
                {
                    var result = DateTime.Parse(test);
                }
                catch
                {
                }
            }
            stopwatch.Stop();
            Debug.WriteLine("Parse = " + stopwatch.Elapsed);
            stopwatch.Start();
            for (var i = 0; i < 1000; i++)
            {
                try
                {
                    var outValue = DateTime.MaxValue;
                    var result = DateTime.TryParse(test, out outValue);
                }
                catch
                {
                }
            }
            stopwatch.Stop();
            Debug.WriteLine("TryParse = " + stopwatch.Elapsed);
            stopwatch.Start();
            for (var i = 0; i < 1000; i++)
            {
                try
                {
                    var result = Convert.ToDateTime(test);
                }
                catch
                {
                }
            }
            stopwatch.Stop();
            Debug.WriteLine("Convert = " + stopwatch.Elapsed);
            stopwatch.Restart();
            for (var i = 0; i < 1000; i++)
            {
                var result = test.To<DateTime>();
            }
            stopwatch.Stop();
            Debug.WriteLine("To = " + stopwatch.Elapsed);
        }
    }
}