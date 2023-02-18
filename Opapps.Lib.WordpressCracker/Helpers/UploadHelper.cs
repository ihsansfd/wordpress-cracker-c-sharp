using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Helpers
{
    public static class UploadHelper
    {
        internal readonly static string UPLOAD_PATH = "Uploads";
        internal readonly static string LOCALIZATION_UPLOAD_PATH = UPLOAD_PATH + "/" + "WordpressLocalizations";

        public static string CsvFormat(this string fileName)
        {
            return fileName.EndsWith(".csv") ? fileName : fileName + ".csv";
        }
    }
}
