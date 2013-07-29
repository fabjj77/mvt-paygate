using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Web
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            int count = 0;

            //Kiểm tra file count_visit.txt nếu không tồn tại thì
            if (System.IO.File.Exists(Server.MapPath("/count.txt")) == false)
            {
                count = 0;
                System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/count.txt"));
                writer.WriteLine(count+1);
                writer.Close();
            }
            // Ngược lại thì
            else
            {
                // Đọc dử liều từ file count_visit.txt
                System.IO.StreamReader read = new System.IO.StreamReader(Server.MapPath("/count.txt"));
                count = int.Parse(read.ReadLine());
                read.Close();
                // Tăng biến count_visit thêm 1
                //count++;
            }
            // khóa website
            Application.Lock();
            // gán biến Application count_visit
            Application["count"] = count;
            // Mở khóa website
            Application.UnLock();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/count.txt"));
            int count = int.Parse(Application["count"]==null?"0":Application["count"].ToString());
            writer.WriteLine(count);
            writer.Close();
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("/count.txt"));
            int count = int.Parse(Application["count"] == null ? "0" : Application["count"].ToString());
            writer.WriteLine(count);
            writer.Close();
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }
    }
}
