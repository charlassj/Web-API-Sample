using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPISample.DataLayer;
using WebAPISample.Models;

namespace WebAPISample.Controllers
{
    public class SampleAPIController : ApiController
    {
        public IEnumerable<SampleData> GetAll()
        {
            DataLayer1 dl = new DataLayer1();
            var model = dl.GetAll();

            return model;
        }

        [HttpPost]
        //[Route("save")]
        public void PostData([FromBody]SampleData sampleData)
        {
            //if (sampleData == null) { return HttpBadRequest(); }
            DataLayer1 dl = new DataLayer1();
            dl.PostData(sampleData);
        }
    }
}
