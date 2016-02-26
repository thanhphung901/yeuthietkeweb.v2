<%@ WebHandler Language="C#" Class="KeepAlive" %>

using System.Web;
using System.Web.SessionState;

public class KeepAlive : IRequiresSessionState, IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
    }
 
    public bool IsReusable {
        get {
            return true;
        }
    }

}