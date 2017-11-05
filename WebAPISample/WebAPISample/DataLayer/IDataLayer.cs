using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPISample.Models;

namespace WebAPISample.DataLayer
{
    interface IDataLayer
    {
        IEnumerable<SampleData> GetAll();

        bool PostData(SampleData sampleData);
    }
}
