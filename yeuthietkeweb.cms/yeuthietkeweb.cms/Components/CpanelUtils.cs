using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Data;
using vpro.functions;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;

namespace yeuthietkeweb.cms.Components
{
    public static class CpanelUtils
    {
        public static void createItemTarget(ref DropDownList ddl)
        {
            List<string[]> l = new List<string[]> { new string[] { "_parent" }, new string[] { "_blank" } };

            ddl.DataSource = from obj in l
                             select new
                             {
                                 Id = obj[0],
                                 Name = obj[0]
                             };

            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataBind();
            ddl.SelectedIndex = 0;

        }

        public static void createItemLanguage(ref RadioButtonList rbl)
        {
            List<string[]> l = new List<string[]> { new string[] { "1", "Việt Nam" }, new string[] { "2", "English" } };

            rbl.DataSource = from obj in l
                             select new
                             {
                                 Id = obj[0],
                                 Name = obj[1]
                             };

            rbl.DataTextField = "Name";
            rbl.DataValueField = "Id";
            rbl.DataBind();
            rbl.SelectedIndex = 0;
        }

        public static void createItemAdPos(ref RadioButtonList rbl)
        {
            List<string[]> l = new List<string[]> 
            { 
                new string[] { "0", "Slideshow" }, 
                new string[] { "1", "Ảnh quảng cáo" },
            };

            rbl.DataSource = from obj in l
                             select new
                             {
                                 Id = obj[0],
                                 Name = obj[1]
                             };

            rbl.DataTextField = "Name";
            rbl.DataValueField = "Id";
            rbl.DataBind();
            rbl.SelectedIndex = 0;
        }

        public static void TransformTableWithSpace(ref DataTable source, DataTable dest, DataRelation rel, DataRow parentRow)
        {
            if (parentRow == null)
            {
                foreach (DataRow row in source.Rows)
                {
                    if (!row.HasErrors && (Utils.CIntDef(row["PROP_PARENT_ID"]) <= 0))
                    {
                        row["PROP_NAME"] = (Utils.CIntDef(row["PROP_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(row["PROP_RANK"]))) + row["PROP_NAME"];
                        dest.Rows.Add(row.ItemArray);
                        row.RowError = "dirty";
                        if (Utils.CStrDef(row["PROP_NAME"]) != "------- Root -------")
                            TransformTableWithSpace(ref source, dest, rel, row);
                    }
                }
            }
            else
            {
                DataRow[] children = parentRow.GetChildRows(rel);
                if (!parentRow.HasErrors)
                {
                    parentRow["PROP_NAME"] = (Utils.CIntDef(parentRow["PROP_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(parentRow["PROP_RANK"]))) + parentRow["PROP_NAME"];
                    dest.Rows.Add(parentRow.ItemArray);
                    parentRow.RowError = "dirty";
                }
                if (children != null && children.Length > 0)
                {
                    foreach (DataRow child in children)
                    {
                        if (!child.HasErrors)
                        {
                            child["PROP_NAME"] = (Utils.CIntDef(child["PROP_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(child["PROP_RANK"]))) + child["PROP_NAME"];
                            dest.Rows.Add(child.ItemArray);
                            child.RowError = "dirty";
                            TransformTableWithSpace(ref source, dest, rel, child);
                        }
                    }
                }
            }
        }

        public static void TransformTableWithSpace1(ref DataTable source, DataTable dest, DataRelation rel, DataRow parentRow)
        {
            if (parentRow == null)
            {
                foreach (DataRow row in source.Rows)
                {
                    if (!row.HasErrors && (Utils.CIntDef(row["AREA_PARENT_ID"]) <= 0))
                    {
                        row["AREA_NAME"] = (Utils.CIntDef(row["AREA_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(row["AREA_RANK"]))) + row["AREA_NAME"];
                        dest.Rows.Add(row.ItemArray);
                        row.RowError = "dirty";
                        if (Utils.CStrDef(row["AREA_NAME"]) != "------- Root -------")
                            TransformTableWithSpace1(ref source, dest, rel, row);
                    }
                }
            }
            else
            {
                DataRow[] children = parentRow.GetChildRows(rel);
                if (!parentRow.HasErrors)
                {
                    parentRow["AREA_NAME"] = (Utils.CIntDef(parentRow["AREA_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(parentRow["AREA_RANK"]))) + parentRow["AREA_NAME"];
                    dest.Rows.Add(parentRow.ItemArray);
                    parentRow.RowError = "dirty";
                }
                if (children != null && children.Length > 0)
                {
                    foreach (DataRow child in children)
                    {
                        if (!child.HasErrors)
                        {
                            child["AREA_NAME"] = (Utils.CIntDef(child["AREA_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(child["AREA_RANK"]))) + child["AREA_NAME"];
                            dest.Rows.Add(child.ItemArray);
                            child.RowError = "dirty";
                            TransformTableWithSpace(ref source, dest, rel, child);
                        }
                    }
                }
            }
        }

        public static string Duplicate(string partToDuplicate, int howManyTimes)
        {
            string result = "";

            for (int i = 0; i < howManyTimes; i++)
                result += partToDuplicate;

            return result;
        }

        public static void ImageResize(string ImageSavePath, string fileName, int MaxWidthSideSize, Stream Buffer)
        {
            int intNewWidth;
            int intNewHeight;
            //int intNewWidth1;
            //int intNewHeight1;
            System.Drawing.Image imgInput = System.Drawing.Image.FromStream(Buffer);
            ImageCodecInfo myImageCodecInfo;
            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter;

            int intOldWidth = imgInput.Width;
            int intOldHeight = imgInput.Height;

            int intMaxSide;
            intMaxSide = intOldWidth;

            if (intMaxSide > MaxWidthSideSize)
            {
                double dblCoef = MaxWidthSideSize / (double)intMaxSide;
                intNewWidth = Convert.ToInt32(dblCoef * intOldWidth);
                intNewHeight = Convert.ToInt32(dblCoef * intOldHeight);
            }
            else
            {
                intNewWidth = intOldWidth;
                intNewHeight = intOldHeight;
            }
            //int intMaxSide1;
            //intMaxSide1 = intNewHeight;

            //if (intNewHeight > MaxHeightSideSize)
            //{
            //    double dblCoef = MaxHeightSideSize / (double)intMaxSide1;
            //    intNewWidth1 = Convert.ToInt32(dblCoef * intNewWidth);
            //    intNewHeight1 = Convert.ToInt32(dblCoef * intNewHeight);
            //}
            //else
            //{
            //    intNewWidth1 = intNewWidth;
            //    intNewHeight1 = intNewHeight;
            //}

            Bitmap bmpResized = new Bitmap(imgInput, intNewWidth, intNewHeight);
            //Chat luong anh (tot nhat) 80L;
            myEncoderParameter = new EncoderParameter(myEncoder, 80L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            bmpResized.Save(ImageSavePath + fileName, myImageCodecInfo, myEncoderParameters);
            imgInput.Dispose();
            bmpResized.Dispose();
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        public static string ClearUnicode(string SourceString)
        {

            SourceString = Regex.Replace(SourceString, "[ÂĂÀÁẠẢÃÂẦẤẬẨẪẰẮẶẲẴàáạảãâầấậẩẫăằắặẳẵ]", "a");
            SourceString = Regex.Replace(SourceString, "[ÈÉẸẺẼÊỀẾỆỂỄèéẹẻẽêềếệểễ]", "e");
            SourceString = Regex.Replace(SourceString, "[IÌÍỈĨỊìíịỉĩ]", "i");
            SourceString = Regex.Replace(SourceString, "[ÒÓỌỎÕÔỒỐỔỖỘƠỜỚỞỠỢòóọỏõôồốộổỗơờớợởỡ]", "o");
            SourceString = Regex.Replace(SourceString, "[ÙÚỦŨỤƯỪỨỬỮỰùúụủũưừứựửữ]", "u");
            SourceString = Regex.Replace(SourceString, "[ỲÝỶỸỴỳýỵỷỹ]", "y");
            SourceString = Regex.Replace(SourceString, "[đĐ]", "d");
            return SourceString;
        }
        public static string FormatMoney(object Expression)
        {
            try
            {
                string Money = String.Format("{0:0,#}", Expression);
                return Money;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
    }
}