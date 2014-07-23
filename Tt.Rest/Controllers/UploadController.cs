using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Ninject;
using Tt.Framework.Service;

namespace Tt.Rest.Controllers
{
    public class UploadController : ApiController
    {
        const string AppDataPath = "~/App_Data";
        private readonly ICollector _collector;

        [Inject]
        public UploadController(ICollector collector)
        {
            _collector = collector;
        }

        public async Task<HttpResponseMessage> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                // Read the form data.
                var provider = new MultipartFormDataStreamProvider(HttpContext.Current.Server.MapPath(AppDataPath));
                await Request.Content.ReadAsMultipartAsync(provider);

                string customer;
                try
                {
                    customer = provider.FormData.GetValues("customer").First();
                }
                catch (Exception)
                {
                    customer = "Unknown Cutomer";
                }

                string username;
                try
                {
                    username = provider.FormData.GetValues("username").First();
                }
                catch (Exception)
                {
                    username = "Anonymous";
                }

                var results = string.Empty;
                foreach (var file in provider.FileData)
                {
                    var fileName = file.Headers.ContentDisposition.FileName;
                    fileName = fileName.Substring(1, fileName.Length - 2);
                    if (_collector.AddCollectorFile(customer, fileName, username, file.LocalFileName))
                        results += string.Format("{0} uploaded successfully. ", fileName);
                    else
                        results += string.Format("Ignored previously processed file {0}. ", fileName);
                }

                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
