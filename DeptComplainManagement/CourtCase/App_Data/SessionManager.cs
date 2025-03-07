using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for SessionManager
/// </summary>
public class SessionManager
{
   
   

    public string Username
    {
        get { return HttpContext.Current.Session["UserName"].ToString(); }
        set { HttpContext.Current.Session["Username"] = value; }
    }

    public string Userid
    {
        get { return HttpContext.Current.Session["Userid"].ToString(); }
        set { HttpContext.Current.Session["Userid"] = value; }
    }


 


    public void Logout()
    {

        HttpContext.Current.Session.Clear();
       HttpContext.Current.Session.Abandon();
       
    }

}