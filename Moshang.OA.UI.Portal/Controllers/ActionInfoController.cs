using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Mvc;
using Moshang.OA.BLL;
using Moshang.OA.IBLL;
using Moshang.OA.Model;
using Moshang.OA.Model.Param;

namespace Moshang.OA.UI.Portal.Controllers
{
    public class ActionInfoController : Controller
    {
        short delflagNormal = (short)Moshang.OA.Model.Enum.DelFlagEnum.Normal;
        public IActionInfoService ActionInfoService { get; set; }

        // GET: ActionInfo
        public ActionResult Index()
        {
            return View();
        }

        //获取数据
        public ActionResult GetAllActionInfos()
        {
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;

            string schName = Request["schName"];
            string schRemark = Request["schRemark"];


            var queryParam = new ActionQueryParam()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Total = 0,
                SchName = schName,
                SchRemark = schRemark
            };

            var pageData = ActionInfoService.LoagPageData(queryParam);
            var temp = pageData.Select(
                a => new
                {
                    ID = a.ID,
                    a.IsMenu,
                    a.Url,
                    a.Remark,
                    a.Sort,
                    a.SubTime,
                    a.ModfiedOn,
                    a.HttpMethd,
                    a.MenuIcon,
                    a.ActionName
                });

            var data = new { total = queryParam.Total, rows = temp.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //添加
        public ActionResult Add(ActionInfo actionInfo)
        {
            actionInfo.ModfiedOn = DateTime.Now;
            actionInfo.SubTime = DateTime.Now;
            actionInfo.DelFlag = delflagNormal;

            actionInfo.IsMenu = Request["IsMenu"] == "true" ? true : false;

            ActionInfoService.Add(actionInfo);
            return Content("ok");
        }

        //修改
        public ActionResult Edit(int id)
        {
            ViewData.Model = ActionInfoService.GetEntities(u => u.ID == id).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ActionInfo actionInfo)
        {
            ActionInfoService.Update(actionInfo);
            return Content("ok");
        }

        //删除
        public ActionResult Delete(string ids)
        {
            //判断是否为空
            if (string.IsNullOrEmpty(ids))
            {
                return Content("请选中要删除的数据！");
            }
            string[] strids = ids.Split(',');
            List<int> idList = new List<int>();
            foreach (var strId in strids)
            {
                idList.Add(int.Parse(strId));
            }
            ActionInfoService.DeleteListByLogical(idList);

            return Content("OK");
        }


        #region 图片上传
        public ActionResult UpLoadImage()
        {
            var files = Request.Files;

            if (files.Count > 0)
            {
                //判断上传的文件是否有内容,这里只上传了一个文件,所以下标为0
                if (files[0].ContentLength > 0)
                {
                    //获取上传的文件的全名
                    string oldName = files[0].FileName;

                    //获取上传的文件的后缀名
                    string extName = Path.GetExtension(oldName);

                    //判断文件是否是图片
                    if (extName == ".jpg" || extName == ".png" || extName == ".gif")
                    {
                        //设置相对路径
                        string relativePath = "/UploadFiles/UploadImgs/";

                        //设置上传文件存储的物理路径
                        string physicsPath = Server.MapPath(relativePath);

                        //设置缩略图存放的路径
                        string pathChild = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";

                        //判断文件夹是否存在
                        if (!Directory.Exists(physicsPath + pathChild))
                        {
                            //创建文件夹
                            Directory.CreateDirectory(physicsPath + pathChild);
                        }

                        //设置上传的图片要保存的名字
                        string newName = Guid.NewGuid() + "-" + extName;

                        //保存图片
                        files[0].SaveAs(physicsPath + newName);

                        //生成缩略图
                        ImgHelper.MakeThumbnail(physicsPath + newName, physicsPath + pathChild + newName, 100, 100, ThumbnailMode.Cut);
                        return Content(relativePath + pathChild + newName);
                    }
                }
                else return Content("no");

            }
            return Content("no");


        }

        public enum ThumbnailMode
        {
            /// <summary>
            ///  指定高宽缩放（可能变形） 
            /// </summary>
            Hw,
            /// <summary>
            /// 指定宽，高按比例
            /// </summary>
            W,
            /// <summary>
            /// 指定高，宽按比例
            /// </summary>
            H,
            /// <summary>
            /// 指定高宽裁减（不变形）               
            /// </summary>
            Cut
        }

        public class ImgHelper
        {
            #region 缩略图
            /// <summary>
            /// 生成缩略图
            /// </summary>
            /// <param name="originalImagePath">源图路径（物理路径）</param>
            /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
            /// <param name="width">缩略图宽度</param>
            /// <param name="height">缩略图高度</param>
            /// <param name="mode">生成缩略图的方式</param>   
            public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height,

                ThumbnailMode mode)
            {
                System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

                int towidth = width;//50
                int toheight = height;//50

                int x = 0;
                int y = 0;
                int ow = originalImage.Width;
                int oh = originalImage.Height;

                switch (mode)
                {
                    case ThumbnailMode.Hw:  //指定高宽缩放（可能变形）               
                        break;
                    case ThumbnailMode.W:   //指定宽，高按比例                   
                        toheight = originalImage.Height * width / originalImage.Width;
                        break;
                    case ThumbnailMode.H:   //指定高，宽按比例
                        towidth = originalImage.Width * height / originalImage.Height;
                        break;
                    case ThumbnailMode.Cut: //指定高宽裁减（不变形）               
                        if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)

                            toheight)
                        {
                            oh = originalImage.Height;
                            ow = originalImage.Height * towidth / toheight;
                            y = 0;
                            x = (originalImage.Width - ow) / 2;
                        }
                        else
                        {
                            ow = originalImage.Width;
                            oh = originalImage.Width * height / towidth;
                            x = 0;
                            y = (originalImage.Height - oh) / 2;
                        }
                        break;
                    default:
                        break;
                }

                //新建一个bmp图片
                System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

                //新建一个画板
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

                //设置高质量插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //清空画布并以透明背景色填充
                g.Clear(System.Drawing.Color.Transparent);

                //在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight), new

                    System.Drawing.Rectangle(x, y, ow, oh), System.Drawing.GraphicsUnit.Pixel);

                try
                {
                    //以jpg格式保存缩略图
                    bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    originalImage.Dispose();
                    bitmap.Dispose();
                    g.Dispose();
                }
            }
            #endregion

        }
        #endregion

    }
}