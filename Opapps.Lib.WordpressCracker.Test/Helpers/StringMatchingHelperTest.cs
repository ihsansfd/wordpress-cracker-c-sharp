using Opapps.Lib.WordpressCracker.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Test.Helpers
{
    public class StringMatchingHelperTest
    {
        [Fact]
        public void CalculateSimilarityWith_WhenTwoStringsCloselyRelated_ReturnDoubleHigherThan0_65()
        {
            var str1 = "Error: The username is not registered on this site. If you are unsure of your username, try your email address instead.";
            var str2 = "Eror: The username is not registered on this site. If you are unsure of your username, try your email address instead.";

            Assert.True(str1.CalculateSimilarityWith(str2) > 0.65);
        }

        [Fact]
        public void CalculateSimilarityWith_WhenTwoStringsNotCloselyRelated_ReturnDsoubleHigherThan0_65()
        {
            var str1 = "Completely different words";
            var str2 = "Eror: The username is not registered on this site. If you are unsure of your username, try your email address instead.";

            Assert.False(str1.CalculateSimilarityWith(str2) > 0.65);
        }
    }
}
