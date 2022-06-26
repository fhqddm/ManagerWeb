using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;

namespace ManagerApi.Controllers
{
    public class VideoController : Controller
    {
        [HttpPost]
        public IActionResult Query(string cond)
        {
            var infos = GenVideoInfos();
            var results = new List<VideoInfo>();
            results = infos.Where(x => x.No.Contains(cond)).ToList();
            if (results.Count>0)
            {
                return Ok(results);
            }
            results = infos.Where(x => x.Actor.Contains(cond)).ToList();
            if (results.Count > 0)
            {
                return Ok(results);
            }
            results = infos.Where(x => x.KeyWords.Contains(cond)).ToList();
            if (results.Count > 0)
            {
                return Ok(results);
            }
            results = infos.Where(x => x.VideoName.Contains(cond)).ToList();
            if (results.Count > 0)
            {
                return Ok(results);
            }
            return View();
        }

        public void GetFiles(DirectoryInfo directory, string pattern, ref List<string> fileList)
        {
            if (directory.Exists || pattern.Trim() != string.Empty)
            {
                try
                {
                    foreach (FileInfo info in directory.GetFiles(pattern))
                    {
                        fileList.Add(info.FullName.ToString());
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                foreach (DirectoryInfo info in directory.GetDirectories())//获取文件夹下的子文件夹
                {
                    GetFiles(info, pattern, ref fileList);//递归调用该函数，获取子文件夹下的文件
                }
            }
        }

        public List<VideoInfo> GenVideoInfos()
        {
            List<VideoInfo> videoInfos = new List<VideoInfo>();
            List<string> pathes = new List<string>();
            DirectoryInfo directoryInfo = new DirectoryInfo("A:\\Japan");
            GetFiles(directoryInfo, "*.*", ref pathes);

            foreach (var path in pathes)
            {
                VideoInfo videoInfo = new VideoInfo();
                if (path.Contains("nfo"))
                {
                    //videoInfo.VideoPath = path;
                    videoInfo.No = Path.GetFileNameWithoutExtension(path);
                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);
                    XmlNode xn = doc.SelectSingleNode("movie");
                    foreach (XmlNode node in xn)
                    {
                        if (node.Name == "title")
                        {
                            videoInfo.VideoName = node.InnerText;
                        }
                        if (node.Name == "actor")
                        {
                            videoInfo.Actor = node.ChildNodes[0].InnerText;
                        }
                        if (node.Name == "genre")
                        {
                            videoInfo.KeyWords = videoInfo.KeyWords + node.InnerText + ",";
                        }
                        //Console.WriteLine(node.InnerText);
                    }
                    //string rr = Path.GetFileName(path);
                    videoInfo.VideoPath = pathes.Where(x => x.Contains(videoInfo.No) && Path.GetExtension(x) !=".nfo" && Path.GetExtension(x)!=".jpg").FirstOrDefault();
                    videoInfo.Cover = path.Replace(".nfo","") + "-fanart.jpg";
                    videoInfo.Poster = path.Replace(".nfo", "") + "-poster.jpg";
                    //Console.WriteLine('-');
                    videoInfos.Add(videoInfo);
                }
            }

            
            return videoInfos;
        }
    }

    public class VideoInfo
    {
        public string No { get; set; }
        public string VideoName { get; set; }
        public string Actor { get; set; }
        public string KeyWords{ get; set; }
        public string VideoPath { get; set; }
        public string Cover { get; set; }
        public string Poster { get; set; }
    }
}
