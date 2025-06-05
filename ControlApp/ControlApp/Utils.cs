using System.Text;
using System.Configuration;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Media;
using System.Windows.Input;
using System.IO;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Forms;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Drawing.Imaging;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Microsoft.VisualBasic.Devices;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Emit;



namespace ControlApp
{
    internal class Utils
    {
        public void writetolog(string shortvr, string longvr)
        {
            if (ConfigurationManager.AppSettings["Logging"] == "On")
            {
                if (ConfigurationManager.AppSettings["Logging"] == "On")
                {
                    File.AppendAllText("log.txt", shortvr);
                }
                else { File.AppendAllText("log.txt", longvr); }
            }
        }
        public string[] GetLatestItem()
        {
            try
            {
                string[] ret = { "", "" };
                string senderid = "";
                string user = ConfigurationManager.AppSettings["UserName"].ToString();
                string pwd = ConfigurationManager.AppSettings["Password"].ToString();
                string vrs = "012";
                string result = getcmd(user, pwd, vrs, "Content");

                ret = seperate_string(result);
                string[] runcode = ret[1].Split("|||");
                try
                {
                    run_process(runcode, ret[0]);
                }
                catch (Exception ex)
                {
                    writetolog("Error running process \n\r", ex.Message);
                }

                return ret;
            }
            catch { return new string[] { "" }; }
        }
        public string[] GetRelations()
        {
            string[] ret = { "", "" };
            string senderid = "";
            string user = ConfigurationManager.AppSettings["UserName"].ToString();
            string pwd = ConfigurationManager.AppSettings["Password"].ToString();
            string vrs = "012";
            string result = getcmd(user, pwd, vrs, "Relations");
            ret = seperate_string(result);
            return ret;
        }
        public void AcceptInvite(string dom)
        {
            string user = ConfigurationManager.AppSettings["UserName"].ToString();
            string pwd = ConfigurationManager.AppSettings["Password"].ToString();
            string vrs = "012";
            string result = getcmd(user, pwd, vrs, "Accept" + dom);
        }
        public void ThumbsUp(string dom)
        {
            string user = ConfigurationManager.AppSettings["UserName"].ToString();
            string pwd = ConfigurationManager.AppSettings["Password"].ToString();
            string vrs = "012";
            string result = getcmd(user, pwd, vrs, "Thumbs" + dom);
        }
        public void RejectInvite(string dom)
        {
            string user = ConfigurationManager.AppSettings["UserName"].ToString();
            string pwd = ConfigurationManager.AppSettings["Password"].ToString();
            string vrs = "012";
            string result = getcmd(user, pwd, vrs, "Reject" + dom);
        }
        public string[] GetInvites()
        {
            string[] ret = { "", "" };
            string senderid = "";
            string user = ConfigurationManager.AppSettings["UserName"].ToString();
            string pwd = ConfigurationManager.AppSettings["Password"].ToString();
            string vrs = "012";
            string result = getcmd(user, pwd, vrs, "Invite");

            int start = result.IndexOf("\"result\">");
            if (start > 0)
            {
                int end = result.IndexOf("</span>", start);
                if (end - (start + 9) >= 0)
                {
                    string fullres = result.Substring(start + 9, end - (start + 9));
                    if (fullres.Length > 3)
                    {
                        ret = seperate_string(fullres);
                    }
                }
            }
            return ret;
        }
        public string[] seperate_string(string seperate)
        {
            if (seperate != "")
            {
                string[] strings = new string[] { "" };
                if ((seperate[0] == '[') && (seperate[seperate.Length - 1] == ']'))
                {
                    strings = seperate.Split("],[");
                    if (strings.Length > 1)
                    {
                        int count = 0;
                        foreach (string s in strings)
                        {
                            if (count == 0)
                            {
                                strings[count] = s.Substring(1, s.Length - 1);
                            }
                            else
                                if (count == strings.Length - 1)
                            {
                                strings[count] = s.Substring(0, s.Length - 1);
                            }
                            count++;
                        }
                    }
                    else
                        strings = new string[] { seperate.Substring(1, seperate.Length - 2) };
                    return strings;

                }
                else return strings;
            }
            else return new string[] { "" };
        }
        public string getcmd(string UserNm, string Pwd, string version, string what)
        {

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            string downloadString = "";
            try
            {
                string geturl = "https://www.thecontrolapp.co.uk/AppCommand.aspx?UserNm=" + UserNm + "&Pwd=" + Ecrypt(Pwd) + "&vrs=" + version + "&cmd=" + what;
                using (ComForm wf = new ComForm(geturl, "Normal"))
                {
                    wf.ShowDialog();
                    string[] res = wf.ReturnedData;
                    downloadString = res[0];
                }
                /*
                using (HttpClient client = new HttpClient())
                {
                    string geturl = "https://www.thecontrolapp.co.uk/AppCommand.aspx?UserNm=" + UserNm + "&Pwd=" + Ecrypt(Pwd) + "&vrs=" + version + "&cmd=" + what;
                    downloadString = client.GetStringAsync(geturl).GetAwaiter().GetResult(); 
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(downloadString);
                    var labelNode = doc.GetElementbyId("result");
                    if (labelNode != null)
                    {
                        // Display the label's text in a Windows Forms label control
                        downloadString = labelNode.InnerText;
                    }
                }
                */
            }
            catch (Exception ex)
            {
                writetolog("Error getting command", ex.Message.ToString());
            }
            return downloadString;
        }
        public bool sendftpfile(string FileName)
        {
            bool ret = true;
            try
            {
                CustomMessage cm = new CustomMessage("Sending file to server", "", 4, false);
                cm.Show();

                string justfilename = Path.GetFileName(FileName);
                string ftpUsername = "acc929431981";
                string ftpPassword = "6scM67YJ+Ezzz0RKCeIxbT9TAfSbRE++1T";

                using (WebClient client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(ftpUsername, Decrypt(ftpPassword));
                    Uri myuri = new System.Uri("ftp://home240474283.1and1-data.host/" + justfilename);
                    client.UploadFile(myuri, WebRequestMethods.Ftp.UploadFile, FileName);
                }

            }
            catch (Exception e)
            {
                ret = false;
                CustomMessage cm = new CustomMessage("Sending file to server failed", "", 4, false);
                cm.ShowDialog();
                writetolog("Error sending ftp process \n\r", e.Message);
            }
            return ret;
        }

        public void deleteoutstanding(string UserNm, string Pwd, string version)
        {
            try
            {
                string user = ConfigurationManager.AppSettings["UserName"].ToString();
                string pwd = ConfigurationManager.AppSettings["Password"].ToString();
                string vrs = "012";
                string result = getcmd(user, pwd, vrs, "Delete");
                /*
                using (HttpClient client = new HttpClient())
                {
                    string urlstr = "https://www.thecontrolapp.co.uk/DeleteOut.aspx?UserNm=" + UserNm + "&Pwd=" + Ecrypt(Pwd) + "&vrs=" + version;
                    string res = client.GetStringAsync(urlstr).GetAwaiter().GetResult();
                }*/
            }
            catch
            { }
        }
        public string[] getoutstanding(string UserNm, string Pwd, string version)
        {
            string[] returned = { "", "", "" };
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            string downloadString = "";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string geturl = "https://www.thecontrolapp.co.uk/GetCount.aspx?UserNm=" + UserNm + "&Pwd=" + Ecrypt(Pwd) + "&vrs=" + version;
                    downloadString = client.GetStringAsync(geturl).Result;
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(downloadString);
                    var labelNode = doc.GetElementbyId("result");
                    if (labelNode != null)
                    {
                        // Display the label's text in a Windows Forms label control
                        returned[0] = labelNode.InnerText;
                    }
                    labelNode = doc.GetElementbyId("next");
                    if (labelNode != null)
                    {
                        // Display the label's text in a Windows Forms label control
                        returned[1] = labelNode.InnerText;
                    }
                    labelNode = doc.GetElementbyId("vari");
                    if (labelNode != null)
                    {
                        // Display the label's text in a Windows Forms label control
                        returned[2] = labelNode.InnerText;
                    }
                }
            }
            catch (Exception ex)
            {
                writetolog("Error getting outstanding \n\r", ex.Message);
            }
            return returned;
        }
        public void sendcmd(string UserNm, string comm, bool all)
        {
            string user = ConfigurationManager.AppSettings["UserName"].ToString();
            string pwd = ConfigurationManager.AppSettings["Password"].ToString();
            if (user == null || pwd == null)
            {
                user = "";
                pwd = "";
            }
            int allint = 0;
            if (all) { allint = 1; }
            string what = "https://www.thecontrolapp.co.uk/AppSendContent.aspx?UserNm=" + UserNm + "&comm=" + comm.Replace("G0", "PPP") + "&all=" + allint + "&fromuser=" + user + "&frompword=" + Ecrypt(pwd).Replace("G0", "PPP");
            using (sendcmd sendc = new sendcmd(what))
            {
                sendc.ShowDialog();
            }
        }
        public void SendBlockReport(string senderid, string command, string report)
        {
            string user = ConfigurationManager.AppSettings["UserName"].ToString();
            string pwd = Ecrypt(ConfigurationManager.AppSettings["Password"].ToString());
            string what = "https://www.thecontrolapp.co.uk/BlockReport.aspx?usernm=" + user + "&pwd=" + pwd + "&vrs=012&sender=" + senderid + "&report=" + report + "&content=" + command;
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = what,
                UseShellExecute = true
            });
        }
        public string Ecrypt(string Line)
        {
            string ToReturn = "";
            try
            {
                string textToEncrypt = Line;

                string publickey = "santhosh";
                string secretkey = "engineer";
                byte[] secretkeyByte;
                secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte;
                publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms;
                CryptoStream cs;
                byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    ToReturn = Convert.ToBase64String(ms.ToArray());
                }
                ToReturn = ToReturn.Replace("\\", "xxx");
                ToReturn = ToReturn.Replace("&", "yyy");
                ToReturn = ToReturn.Replace("/", "zzz");
                ToReturn = ToReturn.Replace("]", "aaa");
                ToReturn = ToReturn.Replace("G0", "ppp");
                ToReturn = ToReturn.Replace("0x", "lll");


            }
            catch (Exception ex)
            {
                writetolog("Error enrypt " + Line + "\n\r", ex.Message);
            }
            return ToReturn;
        }
        public string Decrypt(string Line)
        {
            string ToReturn = "";
            try
            {

                Line = Line.Replace("xxx", "\\");
                Line = Line.Replace("yyy", "&");
                Line = Line.Replace("zzz", "/");
                Line = Line.Replace("aaa", "]");
                Line = Line.Replace("ppp","G0");
                Line = Line.Replace("lll","0x");

                string publickey = "santhosh";
                string privatekey = "engineer";
                byte[] privatekeyByte = System.Text.Encoding.UTF8.GetBytes(privatekey);
                byte[] publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = new byte[Line.Replace(" ", "+").Length];
                inputbyteArray = Convert.FromBase64String(Line.Replace(" ", "+"));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    ToReturn = encoding.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                writetolog("Decrypt error for : " + Line + "\n\r", ex.Message);
            }
            return ToReturn;
        }
        public string Get_File(string url)
        {
            string filename = "";
            string directory = url.Substring(0, url.LastIndexOf('/'));
            Uri trueuri = new Uri(url);
            filename = url.Substring(url.LastIndexOf("/") + 1, (url.Length - url.LastIndexOf("/")) - 1);
            filename = ConfigurationManager.AppSettings["LocalDrive"] + filename;
            filename.Replace(".webp", "");
            try
            {
                using (CustomMessage cm = new CustomMessage("Downloading Image please wait", "", 0, false))
                {
                    cm.Show();
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/109.0");

                        using (Task<Stream> s = client.GetStreamAsync(trueuri))
                        {
                            s.Wait(); // Wait for the task to complete

                            using (FileStream fs = new FileStream(filename, FileMode.Create))
                            {
                                s.Result.CopyTo(fs); // Use the result of the completed task
                            }

                            s.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                filename = "FAILED";
                writetolog("Error getting file for : " + url + "\n\r", ex.Message);
            }
            return filename;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;
        string[] bannedlist = new string[] { "booru.allthefallen.moe", "media.mstdn.jp", "mega.nz", "thecontrolapp.co.uk/Pages/ControlPC", "paradroid-gamma.vercel", "imagekit.io/tools/asset-public-link", "paradroid-gamma.web.app" };
        string[] bannedref = new string[] { "money", "pay" };
        public string[] blist = new string[] { "" };
        public void Change_Wallpaper(string filename)
        {
            string style = ConfigurationManager.AppSettings["PaperStyle"];
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            if (key != null)
            {
                if (style == "Stretch")
                {
                    key.SetValue(@"WallpaperStyle", 2.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                }

                if (style == "Fit")
                {
                    key.SetValue(@"WallpaperStyle", 1.ToString());
                    key.SetValue(@"TileWallpaper", 0.ToString());
                }

                SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, filename, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
            }
        }
        public string[] remove_notneeded(string[] lines)
        {
            List<string> ret = new List<string>();
            foreach (string line in lines)
            {
                if (line == "")
                    break;

                string decrip = Decrypt(line);
                string which = decrip.Substring(0, 2);
                string what = decrip.Substring(2, decrip.Length - 2).Trim();
                if ((which == "D=") && (ConfigurationManager.AppSettings["Downloads"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["Downloads"].ToString())))
                {
                    ret.Add(line);
                }
                if ((which == "P=") && (ConfigurationManager.AppSettings["Wallpapers"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["Wallpapers"].ToString())))
                {
                    ret.Add(line);
                }
                if ((which == "R=") && (ConfigurationManager.AppSettings["Runables"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["Runables"].ToString())))
                {
                    ret.Add(line);
                }
                if ((which == "W=") && (ConfigurationManager.AppSettings["OpenWeb"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["OpenWeb"].ToString())))
                {
                    ret.Add(line);
                }
                if ((which == "U=") || (which == "O=" && what.IndexOf("static") > 0))
                {
                    if (ConfigurationManager.AppSettings["PopUps"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["PopUps"].ToString()))
                    {
                        ret.Add(line);
                    }
                }
                if (which == "O=" && what.IndexOf("static") == -1)
                {
                    if (ConfigurationManager.AppSettings["PopUps"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["PopUps"].ToString()))
                    {
                        ret.Add(line);
                    }
                }
                if ((which == "M=") && (ConfigurationManager.AppSettings["Messages"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["Messages"].ToString())))
                {
                    bool found = false;
                    foreach (string bref in bannedref)
                    {
                        if (what.Contains(bref))
                            found = true;
                    }
                    foreach (string bref in blist)
                    {
                        if ((what.Contains(bref)) && (bref != ""))
                            found = true;
                    }
                    if (!found)
                    {
                        ret.Add(line);
                    }
                }
                if ((which == "S=") && (ConfigurationManager.AppSettings["Subliminals"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["Subliminals"].ToString())))
                {
                    ret.Add(line);
                }
                if ((which == "V=") && (ConfigurationManager.AppSettings["Subliminals"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["Subliminals"].ToString())))
                {
                    ret.Add(line);
                }
                if ((which == "A=") && (ConfigurationManager.AppSettings["Audios"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["Audios"].ToString())))
                {
                    ret.Add(line);
                }
                if ((which == "F=") && (ConfigurationManager.AppSettings["Writeformes"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["Writeformes"].ToString())))
                {
                    ret.Add(line);
                }
                if ((which == "1=") && (ConfigurationManager.AppSettings["ScreenShots"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["ScreenShots"].ToString())))
                {
                    ret.Add(line);
                }
                if ((which == "2=") && (ConfigurationManager.AppSettings["Watch4me"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["Watch4me"].ToString())))
                {
                    ret.Add(line);
                }
                if ((which == "3=") && (ConfigurationManager.AppSettings["twitter"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["twitter"].ToString())))
                {
                    bool found = false;
                    foreach (string bref in bannedref)
                    {
                        if (what.Contains(bref))
                            found = true;
                    }
                    foreach (string bref in blist)
                    {
                        if ((what.Contains(bref))&&(bref!=""))
                            found = true;
                    }
                    if (!found)
                    {
                        ret.Add(line);
                    }
                }
                if ((which == "4=") && (ConfigurationManager.AppSettings["SendDelete"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["SendDelete"].ToString())))
                {
                    ret.Add(line);
                }
                if ((which == "5=") && (ConfigurationManager.AppSettings["TTS"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["TTS"].ToString())))
                {
                    bool found = false;
                    foreach (string bref in bannedref)
                    {
                        if (what.Contains(bref))
                            found = true;
                    }
                    foreach (string bref in blist)
                    {
                        if ((what.Contains(bref)) && (bref != ""))
                            found = true;
                    }
                    if (!found)
                    {
                        ret.Add(line);
                    }
                }
                if ((which == "6=") && (ConfigurationManager.AppSettings["Webcam"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["Webcam"].ToString())))
                {
                    ret.Add(line);
                }
                if ((which == "7=") && (ConfigurationManager.AppSettings["DisM"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["DisM"].ToString())))
                {
                    ret.Add(line);
                }
                if ((which == "8=") && (ConfigurationManager.AppSettings["DisM"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["DisM"].ToString())))
                {
                    ret.Add(line);
                }
                if (which == "L=")
                {
                    ret.Add(line);
                }
                if (which == "9=")
                {
                    ret.Add(line);
                }
            }
            return ret.ToArray();
        }
        public bool IsWebPage(string input)
        {
            if (Uri.TryCreate(input, UriKind.Absolute, out Uri uriResult))
            {
                return uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
            }
            return false;
        }
        public void run_process(string[] lines, string from)
        {
            lines = remove_notneeded(lines);
            if (lines.Length > 0)
                SystemSounds.Beep.Play();
            foreach (string line in lines)
            {
                if (line == "")
                    break;
                string decrip = Decrypt(line);

                writetolog(line + "\n", line + "\n");
                if (decrip.Length > 2)
                {
                    try
                    {
                        string which = decrip.Substring(0, 2);
                        string what = decrip.Substring(2, decrip.Length - 2).Trim();
                        bool inbanned = false;
                        string blist = ConfigurationManager.AppSettings["BlackList"];
                        if (blist != null && blist.Length > 0)
                        {
                            string[] blists = seperate_string(blist);
                            bannedlist.Concat(blists);
                        }
                        foreach (string banned in bannedlist)
                        {
                            if (what.Contains(banned))
                                inbanned = true;
                        }
                        if (inbanned)
                        {
                            CustomMessage cm = new CustomMessage("Message contains potential bad stuff closing", "", 4, false);
                            cm.ShowDialog();
                            break;
                        }
                        if (what.Substring(0, 3) == "FTP")
                        {
                            what = what = "https://www.thecontrolapp.co.uk/storage/" + (what.Substring(3, what.Length - 3));
                        }
                        string type = "";
                        try
                        {
                            type = Path.GetExtension(what);
                        }
                        catch { }
                        bool isfile = false;
                        if (type != "")
                        {
                            isfile = true;
                        }
                        if (which == "D=")
                        {
                            string thefile = "";
                            if (!isfile)
                            {
                                System.Diagnostics.Process.Start(new ProcessStartInfo
                                {
                                    FileName = what,
                                    UseShellExecute = true
                                });
                            }
                            else
                            {
                                thefile = Get_File(what);
                            }
                            if (thefile != "FAILED")
                            {
                                CustomMessage cm = new CustomMessage("File downloaded! Find it here : " + thefile, "", 4, false);
                                cm.ShowDialog();
                            }
                        }
                        else if (which == "P=")
                        {

                            if (!isfile)
                            {
                                System.Diagnostics.Process.Start(new ProcessStartInfo
                                {
                                    FileName = what,
                                    UseShellExecute = true
                                });
                            }
                            else
                            {
                                string thefile = "";

                                thefile = Get_File(what);
                                if (thefile != "FAILED")
                                {
                                    Change_Wallpaper(thefile);
                                }
                            }

                        }
                        else if (which == "L=")
                        {
                            bool done = false;
                            foreach (Form fm in System.Windows.Forms.Application.OpenForms)
                            {
                                if (fm.GetType() == typeof(SubLoop))
                                {
                                    SubLoop pop = (SubLoop)fm;
                                    pop.additem(what);
                                    done = true;
                                }
                            }
                            if (!done)
                            {
                                string appDirectory = AppDomain.CurrentDomain.BaseDirectory + @"\ConstantSubList.txt";

                                string filetype = Path.GetExtension(what);
                                string[] types = new string[] { ".jpg", ".jpeg", ".gif", ".mov", ".mpg", ".mpeg", ".avi", ".png", ".mp4" };
                                if (types.Contains(filetype))
                                {
                                    //if message add to list
                                    what = Get_File(what);
                                    if (what != "FAILED")
                                        using (StreamWriter writer = File.AppendText(appDirectory))
                                        {
                                            writer.WriteLine("[m],[" + what + "]");
                                        }
                                }else
                                //add data to text file
                                if ((!IsWebPage(what))&& (what != "FAILED"))
                                {
                                    using (StreamWriter writer = File.AppendText(appDirectory))
                                    {
                                        writer.WriteLine("[t],[" + what + "]");
                                    }
                                }
                                else
                                {

                                    //if message add to list
                                    what = Get_File(what);
                                    if (what != "FAILED")
                                        using (StreamWriter writer = File.AppendText(appDirectory))
                                        {
                                            writer.WriteLine("[m],[" + what + "]");
                                        }
                                }
                            }
                        }
                        else if (which == "R=")
                        {
                            if (isfile)
                            {
                                string thefile = "";
                                thefile = Get_File(what);
                                if (thefile != "FAILED")
                                {
                                    if (ConfigurationManager.AppSettings["AutoRun"] == "True")
                                        Process.Start(thefile);
                                    else
                                    {
                                        CustomMessage cm = new CustomMessage("Exe downloaded! Find it here : " + thefile, "", 4, false);
                                        cm.ShowDialog();
                                    }
                                }
                            }
                        }
                        else if (which == "W=")
                        {
                            System.Diagnostics.Process.Start(new ProcessStartInfo
                            {
                                FileName = what,
                                UseShellExecute = true
                            });
                        }
                        else if ((which == "U=") || (which == "O=" && what.IndexOf("static") > 0))
                        {
                            if (!isfile)
                            {
                                System.Diagnostics.Process.Start(new ProcessStartInfo
                                {
                                    FileName = what,
                                    UseShellExecute = true
                                });
                            }
                            else
                            {
                                string thefile = "";
                                thefile = Get_File(what);
                                if (thefile != "FAILED")
                                {
                                    string popstyle = "Serial";
                                    if (ConfigurationManager.AppSettings["PopStyle"] != null)
                                        popstyle = ConfigurationManager.AppSettings["PopStyle"].ToString();
                                    if (popstyle == "Serial")
                                    {
                                        bool done = false;
                                        foreach (Form fm in System.Windows.Forms.Application.OpenForms)
                                        {
                                            if (fm.GetType() == typeof(PopUp))
                                            {
                                                PopUp pop = (PopUp)fm;
                                                pop.add_url(thefile);
                                                done = true;
                                            }
                                        }
                                        if (!done)
                                        {
                                            PopUp pop = new PopUp(thefile);
                                            pop.Show();
                                        }
                                    }
                                    else
                                    {
                                        PopUp pop = new PopUp(thefile);
                                        pop.Show();
                                    }
                                }
                            }
                        }
                        else if (which == "O=" && what.IndexOf("static") == -1)
                        {
                            if (!isfile)
                            {
                                System.Diagnostics.Process.Start(new ProcessStartInfo
                                {
                                    FileName = what,
                                    UseShellExecute = true
                                });
                            }
                            else
                            {
                                string popstyle = "Serial";
                                if (ConfigurationManager.AppSettings["PopStyle"] != null)
                                    popstyle = ConfigurationManager.AppSettings["PopStyle"].ToString();
                                if (popstyle == "Serial")
                                {
                                    bool done = false;
                                    foreach (Form fm in System.Windows.Forms.Application.OpenForms)
                                    {
                                        if (fm.GetType() == typeof(PopUp))
                                        {
                                            PopUp pop = (PopUp)fm;
                                            pop.add_url(what);
                                            done = true;
                                        }
                                    }
                                    if (!done)
                                    {
                                        PopUp pop = new PopUp(what);
                                        pop.Show();
                                    }
                                }
                                else
                                {
                                    PopUp pop = new PopUp(what);
                                    pop.Show();
                                }
                            }

                        }
                        else if (which == "M=")
                        {
                            string[] split = what.Split("&&&");
                            CustomMessage cm;
                            if (split.Length > 1)
                            {
                                cm = new CustomMessage(split[0], split[1], 0, false);
                            }
                            else { cm = new CustomMessage(split[0], "", 0, false); }
                            cm.ShowDialog();
                        }
                        else if (which == "S=")
                        {
                            if (!isfile)
                            {
                                System.Diagnostics.Process.Start(new ProcessStartInfo
                                {
                                    FileName = what,
                                    UseShellExecute = true
                                });
                            }
                            else
                            {
                                string thefile = "";

                                thefile = Get_File(what);
                                if (thefile != "FAILED")
                                {
                                    Subliminal subliminal = new Subliminal(thefile, false);
                                }
                            }

                        }
                        else if (which == "V=")
                        {
                            Subliminal subliminal = new Subliminal(what, true);
                        }
                        else if (which == "A=")
                        {
                            if (!isfile)
                            {
                                System.Diagnostics.Process.Start(new ProcessStartInfo
                                {
                                    FileName = what,
                                    UseShellExecute = true
                                });
                            }
                            else
                            {
                                string thefile = "";
                                thefile = Get_File(what);
                                if (thefile != "FAILED")
                                {
                                    Audiopop aud = new Audiopop(thefile);
                                    aud.ShowDialog();
                                }
                            }
                        }
                        else if (which == "F=")
                        {
                            bool found = false;
                            foreach (string bref in bannedref)
                            {
                                if (what.Contains(bref))
                                    found = true;
                            }
                            if (!found)
                            {
                                string[] split = what.Split("&&&");
                                WriteForMe wfm;
                                wfm = new WriteForMe(split[0], split[1], from);
                                wfm.ShowDialog();
                            }
                        }
                        else if ((which == "1=") && (what == "Yes"))
                        {
                            string filename = "scr" + ConfigurationManager.AppSettings["UserName"].ToString() + from + DateTime.Now.Date.ToString().Replace("/", "").Replace(" ", "").Replace(":", "") + ".jpg";
                            string filePath = ConfigurationManager.AppSettings["LocalDrive"] + filename;
                            using (Bitmap bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                        Screen.PrimaryScreen.Bounds.Height))
                            {
                                using (Graphics g = Graphics.FromImage(bmpScreenCapture))
                                {
                                    g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                                     Screen.PrimaryScreen.Bounds.Y,
                                                     0, 0,
                                                     bmpScreenCapture.Size,
                                                     CopyPixelOperation.SourceCopy);
                                }
                                Bitmap resized = new Bitmap(bmpScreenCapture, new Size(bmpScreenCapture.Width / 2, bmpScreenCapture.Height / 2));
                                resized.Save(filePath, ImageFormat.Jpeg);
                            }
                            if (sendftpfile(filePath))
                            {
                                sendcmd(from, Ecrypt("U=FTP" + filename) + "|||", false);
                                MessageBox.Show("Screen shot taken :D");
                            }
                        }
                        else if (which == "2=")
                        {
                            WatchForMe wfm = new WatchForMe(what, from);
                            wfm.Show();
                        }
                        else if (which == "3=")
                        {
                            what = what.Replace(" ", "%20");
                            System.Diagnostics.Process.Start(new ProcessStartInfo
                            {
                                FileName = "https://x.com/intent/tweet?text=" + what + " [Posted by :]&url=www.thecontrolapp.co.uk",
                                UseShellExecute = true
                            });
                        }
                        else if ((which == "4=") && (what == "Yes"))
                        {
                            SendOrDelete sod = new SendOrDelete(from);
                            sod.Show();
                        }
                        else if (which == "5=")
                        {
                            CustomMessage cm = new CustomMessage(what, "", 4, true);
                            cm.Show();
                        }
                        else if (which == "6=")
                        {
                            try
                            {
                                string filename = "";
                                string filePath = "";
                                if ((ConfigurationManager.AppSettings["WebCnt"] != null) && (ConfigurationManager.AppSettings["WebCnt"].ToString() == "True"))
                                {
                                    CustomMessage cm = new CustomMessage("Webcam picture in 5...4...3...2...1...", "", 5, false);
                                    cm.Show();
                                }
                                using (VideoCapture capture = new VideoCapture()) //create a camera capture
                                {
                                    using (Bitmap image = Emgu.CV.BitmapExtension.ToBitmap(capture.QueryFrame()))
                                    {
                                        filename = "web" + ConfigurationManager.AppSettings["UserName"].ToString() + from + DateTime.Now.Date.ToString().Replace("/", "").Replace(" ", "").Replace(":", "") + ".jpg";
                                        filePath = ConfigurationManager.AppSettings["LocalDrive"] + filename;
                                        Bitmap resized = new Bitmap(image, new Size(Convert.ToInt16(image.Width / 1.5), Convert.ToInt16(image.Height / 1.5)));
                                        resized.Save(filePath);
                                    }
                                }
                                if (sendftpfile(filePath))
                                    sendcmd(from, Ecrypt("U=FTP" + filename) + "|||", false);
                            }
                            catch (Exception ex)
                            {
                                writetolog("Error getting webcam \n\r", ex.Message);
                            }
                        }
                        else if (which == "7=")
                        {
                            bool found = false;
                            foreach (Form fm in System.Windows.Forms.Application.OpenForms)
                            {
                                if (fm.GetType() == typeof(Blank))
                                {
                                    Blank blank = (Blank)fm;
                                    blank.add_time(1);
                                    found = true;
                                }
                            }
                            if (!found)
                            {
                                Blank blank = new Blank(1);
                                blank.Show();
                                Cursor.Show();
                            }
                        }
                        else if (which == "8=")
                        {
                            bool found = false;
                            foreach (Form fm in System.Windows.Forms.Application.OpenForms)
                            {
                                if (fm.GetType() == typeof(Blank))
                                {
                                    Blank blank = (Blank)fm;
                                    blank.add_time(1);
                                    found = true;
                                }
                            }
                            if (!found)
                            {
                                Blank blank = new Blank(2);
                                blank.Show();
                                Cursor.Show();
                            }
                        }
                        else if (which == "9=")
                        {
                            Spinner sp = new Spinner(what);
                            sp.Show();
                        }
                    }
                    catch (Exception e)
                    {
                        writetolog("Error proccessing for : " + line + "\n\r", e.Message);
                    }
                }
            }
        }

    }
}
