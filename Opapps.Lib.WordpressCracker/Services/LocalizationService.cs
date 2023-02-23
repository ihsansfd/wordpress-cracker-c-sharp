using Opapps.Lib.WordpressCracker.Entities;
using Opapps.Lib.WordpressCracker.Helpers;
using Opapps.Lib.WordpressCracker.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Services
{

    public static class LocalizationType {
        public static readonly string USERNAME = "Username";
        public static readonly string PASSWORD = "Password";
    }
    public class LocalizationService
    {
        private readonly LocalizationCsvFileLoader localizationCsvFileLoader = new LocalizationCsvFileLoader();
        private readonly static string LOCALIZATION_SUFFIX = "Localization";

        public string GetLocalizedTextFor(CultureInfo siteLang, string localizationType)
        {
            IEnumerable<LocalizationRecord> records = localizationCsvFileLoader.EnumerateRecords(localizationType + LOCALIZATION_SUFFIX);

            var intendedRecord =  records.FirstOrDefault((record) => record.SiteLang == siteLang.Name);
            if(intendedRecord == null) throw new SiteLangNotAvailableException();

            return intendedRecord.Text;
        }
    }
}
