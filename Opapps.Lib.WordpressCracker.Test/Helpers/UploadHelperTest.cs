using Opapps.Lib.WordpressCracker.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Test.Helpers
{
    public class UploadHelperTest
    {
        [Fact]
        public void CsvFormat_WhenFileNameIsNotInCsvFormat_ReturnTheCsvFormat()
        {
            Assert.True("fileName".CsvFormat() == "fileName.csv");

        }
        
        [Fact]
        public void CsvFormat_WhenFileNameIsInCsvFormat_DontPrependAnything()
        {
            Assert.True("fileName.csv".CsvFormat() == "fileName.csv");

        }
    }
}
