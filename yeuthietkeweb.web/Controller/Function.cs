using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vpro.functions;

namespace Controller
{
    public class Function
    {
        public string Getlink_News(object News_url, object Seo_url, object Cat_seo_url)
        {
            string _sType = Utils.CStrDef(Cat_seo_url);
            return string.IsNullOrEmpty(Utils.CStrDef(News_url)) ? "/" + _sType + "/" + Utils.CStrDef(Seo_url) + ".html" : Utils.CStrDef(News_url);
        }
        public string Getlink_News_EN(object News_url, object Seo_url, object Cat_seo_url)
        {
            string _sType = Utils.CStrDef(Cat_seo_url);
            return string.IsNullOrEmpty(Utils.CStrDef(News_url)) ? "/en/" + _sType + "/" + Utils.CStrDef(Seo_url) + ".html" : Utils.CStrDef(News_url);
        }
        public string Getlink_Cat(object Cat_Url, object Cat_Seo_Url)
        {
            return string.IsNullOrEmpty(Utils.CStrDef(Cat_Url)) ? "/" + Utils.CStrDef(Cat_Seo_Url) + ".html" : Utils.CStrDef(Cat_Url);
        }
        public string Getlink_Cat_EN(object Cat_Url, object Cat_Seo_Url)
        {
            return string.IsNullOrEmpty(Utils.CStrDef(Cat_Url)) ? "/en/" + Utils.CStrDef(Cat_Seo_Url) + ".html" : Utils.CStrDef(Cat_Url);
        }
        public string Getprice(object Price)
        {
            decimal _dPrice = Utils.CDecDef(Price);
            return _dPrice != 0 ? String.Format("{0:0,0 VNĐ}",_dPrice) : "Liên hệ";
        }
        public string Getprice1(object Price1, object Price2)
        {
            decimal _dPrice1 = Utils.CDecDef(Price1);
            decimal _dPrice2 = Utils.CDecDef(Price2);
            if (_dPrice2 != 0)
            {
                return String.Format("{0:0,0 VNĐ}", _dPrice2);
            }
            return _dPrice1 != 0 ? String.Format("{0:0,0 VNĐ}", _dPrice1) : "Liên hệ";
        }
        public decimal Getprice_addtocart(object Price1)
        {
            decimal _dPrice1 = Utils.CDecDef(Price1);
            if (_dPrice1 != 0)
            {
                return _dPrice1;
            }
            return 0;
        }
        public string Getprice2(object Price1, object Price2)
        {
            decimal _dPrice1 = Utils.CDecDef(Price1);
            decimal _dPrice2 = Utils.CDecDef(Price2);
            if (_dPrice2 != 0)
            {
                return String.Format("{0:0,0 VNĐ}", _dPrice1);
            }
            return "";
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy HH:mm}", News_PublishDate);
        }
        public string GetImageT_News(object News_Id, object News_Image1)
        {
            try
            {
                if (Utils.CIntDef(News_Id) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(News_Image1)))
                {
                    return PathFiles.GetPathNews(Utils.CIntDef(News_Id)) + Utils.CStrDef(News_Image1);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public string GetImageT_News_Hasclass(object News_Id, object News_Image1,string nameclass)
        {
            try
            {
                if (Utils.CIntDef(News_Id) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(News_Image1)))
                {
                    return "<img class='"+nameclass+"' alt='' src='"+PathFiles.GetPathNews(Utils.CIntDef(News_Id)) + Utils.CStrDef(News_Image1)+"'/>";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public string Getimages_Cat(object cat_id, object cat_img)
        {
            if (Utils.CIntDef(cat_id) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(cat_img)))
            {
                return PathFiles.GetPathCategory(Utils.CIntDef(cat_id)) + Utils.CStrDef(cat_img);
            }
            else
            {
                return "";
            }
        }
        public string GetImageAd(object Ad_Id, object Ad_Image1, object Ad_Target, object Ad_Url)
        {
            try
            {
                if (Utils.CIntDef(Ad_Id) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Ad_Image1)))
                    return "<a href='" + Utils.CStrDef(Ad_Url) + "' target='" + Utils.CStrDef(Ad_Target) + "'><img  width='100%' src='" + PathFiles.GetPathAdItems(Utils.CIntDef(Ad_Id)) + Utils.CStrDef(Ad_Image1) + "' alt='' /></a>";
                return "";
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }

        }
        public string GetImageAd(object Ad_Id, object Ad_Image1, object Ad_Target, object Ad_Url, object type)
        {
            try
            {
                string _sResult = string.Empty;
                if (Utils.CIntDef(type) == 0)
                {

                    if (Utils.CIntDef(Ad_Id) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Ad_Image1)))
                        _sResult = "<a href='" + Utils.CStrDef(Ad_Url) + "' target='" + Utils.CStrDef(Ad_Target) + "'><img src='" + PathFiles.GetPathAdItems(Utils.CIntDef(Ad_Id)) + Utils.CStrDef(Ad_Image1) + "' alt='' /></a>";
                }
                else
                {
                    if (Utils.CIntDef(Ad_Id) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Ad_Image1)))
                    {
                        _sResult += "<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0'  width='960' height='412' id='ShockwaveFlash1' >";
                        _sResult += "<param name='movie' value='" + PathFiles.GetPathAdItems(Utils.CIntDef(Ad_Id)) + Utils.CStrDef(Ad_Image1) + "'>";
                        _sResult += "<param name='Menu' value='0'>";
                        _sResult += "<param name='quality' value='high'>";
                        _sResult += "<param name='wmode' value='transparent'>";
                        _sResult += "<embed width='960' height='412' src='" + PathFiles.GetPathAdItems(Utils.CIntDef(Ad_Id)) + Utils.CStrDef(Ad_Image1) + "' wmode='transparent' ></object>";
                    }

                }
                return _sResult;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return "";
            }

        }
        public string GetImageAd1(object Ad_Id, object Ad_Image1, object Ad_Target, object Ad_Url, object type)
        {
            try
            {
                string _sResult = string.Empty;
                if (Utils.CIntDef(type) == 0)
                {

                    if (Utils.CIntDef(Ad_Id) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Ad_Image1)))
                        _sResult = PathFiles.GetPathAdItems(Utils.CIntDef(Ad_Id)) + Utils.CStrDef(Ad_Image1);
                }
                return _sResult;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return "";
            }

        }
        public string GetImageSlideHome(object Ad_Id, object Ad_Image1, object Ad_Target, object Ad_Url, object Ad_Item_Desc)
        {
            try
            {
                string s = "";                 
                 s+= "<a href='" + Utils.CStrDef(Ad_Url) + "' target='" + Utils.CStrDef(Ad_Target) + "'> ";
                 s+= "<img src='" + PathFiles.GetPathAdItems(Utils.CIntDef(Ad_Id)) + Utils.CStrDef(Ad_Image1) + "' /> ";
                 s+= "</a> ";
                 return s;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }

        }
        public string GetImageCus(object Ad_Id, object Ad_Image1, object Ad_Target, object Ad_Url, object Ad_Item_Desc)
        {
            try
            {
                if (Utils.CIntDef(Ad_Id) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Ad_Image1)))
                {
                    string str = "";
                            str+="<li class='marquee'>";
                            str += "<a class='bwWrapper' href='" + Utils.CStrDef(Ad_Url) + "' target='" + Utils.CStrDef(Ad_Target) + "' title='" + Utils.CStrDef(Ad_Item_Desc) + "'>";
                            str+="<img src='" + PathFiles.GetPathAdItems(Utils.CIntDef(Ad_Id)) + Utils.CStrDef(Ad_Image1) + "' ";
                            //str += " data-hover='http://www.savicor_vi_1.gif' ";
                            str +=" title='" + Utils.CStrDef(Ad_Item_Desc) + "' alt='" + Utils.CStrDef(Ad_Item_Desc) + "' />"; 
                            str += "</a>";
                            str += "</li>";
                    return str;
                }
                return "";

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }

        }
        public string GetImageSliderIndex(object Ad_Id, object Ad_Image1, object Ad_Target, object Ad_Url)
        {
            try
            {
                if (Utils.CIntDef(Ad_Id) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Ad_Image1)))
                    return "<a href='"+ Utils.CStrDef(Ad_Url) +"'> <img src='" + PathFiles.GetPathAdItems(Utils.CIntDef(Ad_Id)) + Utils.CStrDef(Ad_Image1) + "' alt='' /></a>";
                return "";
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }

        }
        //Get images logo - sologan
        public string Getbanner(object Banner_type, object banner_field, object Banner_ID, object Banner_Image)
        {
            string banner = banner_field.ToString();
            return banner == "1" ? "<a href='/'>" + GetImage(Banner_type, Banner_ID, Banner_Image) + "</a>" : "<a href='/'> " + GetImagebanner(Banner_type, Banner_ID, Banner_Image) + "</a>";
        }
        public string GetLogo(object Banner_type, object banner_field, object Banner_ID, object Banner_Image)
        {
            string banner = banner_field.ToString();
            return banner == "1" ? GetImageLogo(Banner_type, Banner_ID, Banner_Image) : "";
        }
        public string GetImage(object Banner_type, object Banner_ID, object Banner_Image)
        {
            try
            {
                string _sResult = string.Empty;
                if (Utils.CIntDef(Banner_type) == 0)
                {
                    if (Utils.CIntDef(Banner_ID) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Banner_Image)))
                        return "<img src='" + PathFiles.GetPathBanner(Utils.CIntDef(Banner_ID)) + Utils.CStrDef(Banner_Image) + "' alt='' />";
                    else
                        return "<img src='/vi-vn/Images/Logo.png'/>";
                }
                else
                {
                    if (Utils.CIntDef(Banner_ID) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Banner_Image)))
                    {
                        _sResult += "<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0'  width='270' height='83' id='ShockwaveFlash1' >";
                        _sResult += "<param name='movie' value='" + PathFiles.GetPathAdItems(Utils.CIntDef(Banner_ID)) + Utils.CStrDef(Banner_Image) + "'>";
                        _sResult += "<param name='Menu' value='0'>";
                        _sResult += "<param name='quality' value='high'>";
                        _sResult += "<param name='wmode' value='transparent'>";
                        _sResult += "<embed width='270' height='83' width='100%' src='" + PathFiles.GetPathBanner(Utils.CIntDef(Banner_ID)) + Utils.CStrDef(Banner_Image) + "' wmode='transparent' ></object>";
                    }

                }
                return _sResult;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public string GetImageLogo(object Banner_type, object Banner_ID, object Banner_Image)
        {
            try
            {
                string _sResult = string.Empty;
                if (Utils.CIntDef(Banner_type) == 0)
                {
                    if (Utils.CIntDef(Banner_ID) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Banner_Image)))
                        return "<img src='" + PathFiles.GetPathBanner(Utils.CIntDef(Banner_ID)) + Utils.CStrDef(Banner_Image) + "' alt='' width='65' style='float:left; margin-right:20px'/>";
                    else
                        return "<img src='/vi-vn/Images/Logo.png'/>";
                }
                else
                {
                    if (Utils.CIntDef(Banner_ID) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Banner_Image)))
                    {
                        _sResult += "<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0'  width='270' height='83' id='ShockwaveFlash1' >";
                        _sResult += "<param name='movie' value='" + PathFiles.GetPathAdItems(Utils.CIntDef(Banner_ID)) + Utils.CStrDef(Banner_Image) + "'>";
                        _sResult += "<param name='Menu' value='0'>";
                        _sResult += "<param name='quality' value='high'>";
                        _sResult += "<param name='wmode' value='transparent'>";
                        _sResult += "<embed width='270' height='83' width='100%' src='" + PathFiles.GetPathBanner(Utils.CIntDef(Banner_ID)) + Utils.CStrDef(Banner_Image) + "' wmode='transparent' ></object>";
                    }

                }
                return _sResult;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public string GetImagebanner(object Banner_type, object Banner_ID, object Banner_Image)
        {
            try
            {
                string _sResult = string.Empty;
                if (Utils.CIntDef(Banner_type) == 0)
                {
                    if (Utils.CIntDef(Banner_ID) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Banner_Image)))
                        return "<img src='" + PathFiles.GetPathBanner(Utils.CIntDef(Banner_ID)) + Utils.CStrDef(Banner_Image) + "' alt='' />";
                    else
                        return "<img src='/vi-vn/Images/Logo.png'/>"; ;
                }
                else
                {
                    if (Utils.CIntDef(Banner_ID) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(Banner_Image)))
                    {
                        _sResult += "<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0'  width='865' height='60' id='ShockwaveFlash1' >";
                        _sResult += "<param name='movie' value='" + PathFiles.GetPathAdItems(Utils.CIntDef(Banner_ID)) + Utils.CStrDef(Banner_Image) + "'>";
                        _sResult += "<param name='Menu' value='0'>";
                        _sResult += "<param name='quality' value='high'>";
                        _sResult += "<param name='wmode' value='transparent'>";
                        _sResult += "<embed width='865' height='60' width='100%' src='" + PathFiles.GetPathBanner(Utils.CIntDef(Banner_ID)) + Utils.CStrDef(Banner_Image) + "' wmode='transparent' ></object>";
                    }

                }
                return _sResult;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        //Support
       
        public string Bind_Online(object Type, object Description, object Nickname)
        {

            try
            {
                int _iType = Utils.CIntDef(Type);
                string _sResult = string.Empty;
                switch (_iType)
                {
                    case 1:
                        _sResult += "<span class='nick_chat'><a href='ymsgr:sendim?" + Utils.CStrDef(Nickname) + "' title='" + Utils.CStrDef(Description) + "'>";
                        _sResult += "<img src='http://opi.yahoo.com/online?u=" + Utils.CStrDef(Nickname) + "&m=g&t=1'   border='0' height='16' />";
                        _sResult += "</a></span> ";

                        break;
                    case 2:
                        _sResult += "<span class='nick_chat'><a href='skype:" + Utils.CStrDef(Nickname) + "?call' title='" + Utils.CStrDef(Description) + "'>";
                        _sResult += "<img src='http://mystatus.skype.com/smallclassic/" + Utils.CStrDef(Nickname) + "' title=" + Utils.CStrDef(Description) + "  alt='' height='16' >";
                        _sResult += "</a></span> ";
                        break;
                    case 0:
                        _sResult += "<b>" + Utils.CStrDef(Nickname) + "</b> <br />" + Utils.CStrDef(Description);
                        break;
                    default:
                        break;
                }
                return _sResult;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public string Bind_SocialNetwork(object Type, object Description, object Nickname)
        {
            try
            {
                int _iType = Utils.CIntDef(Type);
                string _sResult = string.Empty;
                switch (_iType)
                {
                    case 0:
                        _sResult += "<span class='hl'><i class='fa fa-phone-square'></i>" + Utils.CStrDef(Nickname) + "</span>";
                        break;
                    case 3:
                        _sResult += "<a href='" + Utils.CStrDef(Nickname) + "' target='_blank' title='" + Utils.CStrDef(Description) + "' class='facebook'><i class='fa fa-facebook'></i></a>";
                        break;
                    case 4:
                        _sResult += "<a href='" + Utils.CStrDef(Nickname) + "' target='_blank' title='" + Utils.CStrDef(Description) + "' class='google-plus'><i class='fa fa-google-plus'></i></a>";
                        break;
                    case 5:
                        _sResult += "<a href='" + Utils.CStrDef(Nickname) + "' target='_blank' title='" + Utils.CStrDef(Description) + "' class='twitter'><i class='fa fa-twitter'></i></a>";
                        break;
                    case 6:
                        _sResult += "<a href='" + Utils.CStrDef(Nickname) + "' target='_blank' title='" + Utils.CStrDef(Description) + "' class='youtube'><i class='fa fa-youtube'></i></a>";
                        break;
                    default:
                        break;
                }
                return _sResult;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }

        public string limitString(object str, int lenhgth)
        {
            string temp = "";
            string _str = Utils.CStrDef(str);
            int count = _str.Length;
            if (count > lenhgth)
                temp = _str.Substring(0, lenhgth) + "...";
            else temp = _str;
            return temp;
        }
    }
}
