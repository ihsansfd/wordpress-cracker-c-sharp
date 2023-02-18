using Opapps.Lib.WordpressCracker.Services;
using Opapps.Lib.WordpressCracker.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Test.Services
{
    public class LocalizationServiceTest
    {
        private readonly LocalizationService service;
        public LocalizationServiceTest()
        {
            service = new();
        }
        [Fact]
        public void GetLocalizedTextFor_WHhenSiteLangIsAvailable_ReturnTheLocalizedText() {
            Assert.NotNull(service.GetLocalizedTextFor(new CultureInfo("id"), LocalizationType.USERNAME));
            Assert.NotNull(service.GetLocalizedTextFor(new CultureInfo("id-ID"), LocalizationType.USERNAME));
            Assert.NotNull(service.GetLocalizedTextFor(new CultureInfo("en"), LocalizationType.USERNAME));
            Assert.NotNull(service.GetLocalizedTextFor(new CultureInfo("en-US"), LocalizationType.USERNAME));
        }

        [Fact]
        public void GetLocalizedTextFor_WhenSiteLangIsNotAvailable_Throw()
        {
            Assert.Throws<SiteLangNotAvailableException>(() => service.GetLocalizedTextFor(new CultureInfo("es"), LocalizationType.USERNAME));
            Assert.Throws<SiteLangNotAvailableException>(() => service.GetLocalizedTextFor(new CultureInfo("da"), LocalizationType.USERNAME));
        }
    
    }
}

