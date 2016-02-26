using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using yeuthietkeweb.cms;

namespace Testtinymice.Filemanager
{
    public partial class Default : System.Web.UI.Page
    {
        string id;
        string path;
        string win;
        string input;
        int count = 0;
        int count2 = 0;
        dbShopDataContext DB = new dbShopDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["sessionid"];
            win = Request.QueryString["win"];
            path = Session["FileManager"].ToString();
            input = Request.QueryString["field_name"];
            if (!IsPostBack)
            {
                Loadlist_img();
            }
        }
        public void Loadlist_img()
        {
            if (id != null)
            {
               
                string FullPath = Server.MapPath(path);

                string[] Files = Directory.GetFiles(FullPath);
                string[] Directorys = Directory.GetDirectories(FullPath);
                List<Object_filename> list = new List<Object_filename>();
                foreach (string s in Files)
                {
                    Object_filename item = new Object_filename();
                    item.filename = System.IO.Path.GetFileName(s);
                    list.Add(item);
                   
                }
                Rplistimg.DataSource = list;
                Rplistimg.DataBind();
                
            }
        }
        public string getOnlickdata(object filename)
        {
            string [] imgtype = { ".png", ".jpg", ".jpeg",".bmp",".gif",".ico" };
            string[] videotype = { ".flv", ".flash", ".wmv", "wmv",".mp4" };
            string[] htmltype = {".htm"};
            string data = string.Empty;
            string[] gettype_file = filename.ToString().Split('.');
            string filenew = "."+gettype_file[gettype_file.Length - 1].ToLower();
            if (imgtype.Contains(filenew))
            {
                data = "<img src='" + path + filename + "'/>";
            }
            else if (videotype.Contains(filenew))
            {
                data = "<img src='/Cpanel/Images/ic_video.jpg'/>";
            }
            else if (htmltype.Contains(filenew))
            {
                data = "<img src='/Cpanel/Images/ic_htm.png'/>";
            }
            else data = "<img src='/Cpanel/Images/ic_notype.png'/>";
            string chuoi = "InsertFile(\'"+input+"\',\'"+path+filename+"\')";
            return "<a href='javascript:void(0)' onclick=\""+chuoi+"\">"+data+"</a>";
        }
        public string getLinkbtn(object filename)
        {
            string chuoiinsert = "InsertFile(\'" + input + "\',\'" + path + filename + "\')";
            string chuoidelete = "DeleteFile('"+ filename + "\',"+count2+")";
            count2++;
            return "<a href='#' class='btn btn-primary' role='button' onclick=\"" + chuoiinsert + "\">Insert</a> <a href='#' class='btn btn-default' role='button' onclick=\"" + chuoidelete + "\">Delete</a>";     
        }
        public int getCount()
        {
            return count++;
        }
        private class Object_filename
        {
            public string filename { get; set; }
        }
        [WebMethod]
        public static string deleteFile(string filename)
        {
            string FullPath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Session["FileManager"].ToString());
            if (Directory.Exists(FullPath))
            {
                System.IO.File.Delete(FullPath + "/" + filename);
                return "success";
            }
            return "errors";
        }
        protected void Btupmulti_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {

                    HttpFileCollection hfc = Request.Files;
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        HttpPostedFile hpf = hfc[i];
                        if (hpf.ContentLength > 0)
                        {

                            string pathfile = Server.MapPath(Session["FileManager"].ToString());
                            string fullpathfile = pathfile + "/" + Path.GetFileName(hpf.FileName);
                            if (!Directory.Exists(pathfile))
                            {
                                Directory.CreateDirectory(pathfile);
                            }
                            hpf.SaveAs(fullpathfile);

                        }
                    }
                    Loadlist_img();
                }



            }
            catch (Exception ex)
            {
                
            }
        }
    }
}