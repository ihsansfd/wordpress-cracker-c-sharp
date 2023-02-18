using CsvHelper;
using CsvHelper.Configuration;
using Opapps.Lib.WordpressCracker.Entities;
using Opapps.Lib.WordpressCracker.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Helpers
{
    internal class CsvLoader<RecordType> : ICsvLoader<RecordType>
    {
        private readonly RecordType _rec;
        private readonly CsvConfiguration _config;

        public CsvLoader(RecordType rec)
        {
            _rec = rec;
            _config = new(CultureInfo.InvariantCulture)
            {
                Delimiter = ","
            };
        }

        public IEnumerable<RecordType> EnumerateRecords(string path)
        {
            using var reader = new StreamReader(path);

            using var csv = new CsvReader(reader, _config);

            foreach (var rec in csv.EnumerateRecords<RecordType>(_rec)) yield return rec;
        }
    }

    public class LocalizationCsvFileLoader
    {
        private readonly ICsvLoader<LocalizationRecord> csvLoader = new CsvLoader<LocalizationRecord>(new LocalizationRecord());

        public IEnumerable<LocalizationRecord> EnumerateRecords(string fileName)
        {
            string fullPath = UploadHelper.LOCALIZATION_UPLOAD_PATH + "/" + fileName.CsvFormat();
            foreach (var rec in csvLoader.EnumerateRecords(fullPath)) {
                yield return rec;
            }

        }
    }

}
