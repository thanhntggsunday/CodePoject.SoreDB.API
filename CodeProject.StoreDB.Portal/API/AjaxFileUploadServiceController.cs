namespace CodeProject.StoreDB.Portal.API
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;

    /// <summary>
    /// Defines the <see cref="AjaxFileUploadServiceController" />
    /// </summary>
    [RoutePrefix("api/AjaxFileUploadService")]
    public class AjaxFileUploadServiceController : BaseApiController
    {
        /// <summary>
        /// The UploadFile
        /// </summary>
        /// <param name="fileType">The fileType<see cref="string"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("UploadFile")]
        [HttpPost]
        public HttpResponseMessage UploadFile(string fileType = "")
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {
                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {
                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {
                            string directory = string.Empty;
                            if (fileType == "avatar")
                            {
                                directory = "/UploadedFiles/Avatars/";
                            }
                            else if (fileType == "product")
                            {
                                directory = "/UploadedFiles/Products/";
                            }
                            else if (fileType == "productCategory")
                            {
                                directory = "/UploadedFiles/productCategory/";
                            }
                            else if (fileType == "news")
                            {
                                directory = "/UploadedFiles/News/";
                            }
                            else if (fileType == "banner")
                            {
                                directory = "/UploadedFiles/Banners/";
                            }
                            else
                            {
                                directory = "/UploadedFiles/";
                            }
                            if (!Directory.Exists(HttpContext.Current.Server.MapPath(directory)))
                            {
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(directory));
                            }

                            string path = Path.Combine(HttpContext.Current.Server.MapPath(directory), postedFile.FileName);
                            //Userimage myfolder name where i want to save my image
                            postedFile.SaveAs(path);
                            return Request.CreateResponse(HttpStatusCode.OK, Path.Combine(directory, postedFile.FileName));
                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex); ;
            }
        }

        /// <summary>
        /// The DeleteFile
        /// </summary>
        /// <param name="filePath">The filePath<see cref="string"/></param>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        [Route("saveImage")]
        [HttpPost]
        public HttpResponseMessage DeleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Created, ex.Message); ;
            }

            var message1 = string.Format("Image Deleted Successfully.");
            return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
        }
    }
}
