using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;

namespace Nebula.MvcAddons.Controllers {
    public abstract class BaseAdminController : BaseController {

        protected string _AbandonSession() {

            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

            SessionIDManager sessionManager = new SessionIDManager();
            string sID = sessionManager.CreateSessionID(System.Web.HttpContext.Current);
            bool redirected = false;
            bool cookieAdded = false;

            sessionManager.SaveSessionID(System.Web.HttpContext.Current, sID, out redirected, out cookieAdded);

            return sID;
        }

        protected bool _HasUploadedFile(string propertyName) {

            propertyName += propertyName.EndsWith("_FileUpload") ? "" : "_FileUpload";

            return Request.Files.AllKeys.Contains(propertyName) && Request.Files[propertyName].ContentLength > 0;
        }

        protected string _ProcessUploadedFile(int objectId, string propertyName, string basePath) {

            string newFileName;
            HttpPostedFileBase currentFile;
            Business.File image;

            propertyName += propertyName.EndsWith("_FileUpload") ? "" : "_FileUpload";

            if (Request.Files.AllKeys.Contains(propertyName)) {
                currentFile = Request.Files[propertyName];

                if (currentFile.ContentLength > 0) {
                    image = new Business.File(objectId, basePath, new string[] { "", "", "" });
                    newFileName = image.GetFilePath(Path.GetFileName(currentFile.FileName));

                    currentFile.SaveAs(Server.MapPath(newFileName));

                    return newFileName;
                }
            }

            return string.Empty;
        }
    }
}
