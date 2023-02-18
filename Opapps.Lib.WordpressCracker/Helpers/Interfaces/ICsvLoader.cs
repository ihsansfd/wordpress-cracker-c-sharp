using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opapps.Lib.WordpressCracker.Helpers.Interfaces
{
    public interface ICsvLoader<RecordType>
    {
        public IEnumerable<RecordType> EnumerateRecords(string fileName);
    }
}
