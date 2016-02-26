using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;

namespace Controller
{
    public class Addto_cart
    {
        #region Decclare
        dbShopDataContext db = new dbShopDataContext();
        #endregion
        public bool Add_To_Cart(int News_id, object Guid)
        {
            var _news = from a in db.ESHOP_NEWs
                        where a.NEWS_ID == News_id
                        select a;
            decimal _dPrice = 0;

            if (_news.ToList().Count > 0)
            {

                if (Utils.CDecDef(_news.ToList()[0].NEWS_PRICE1) == 0)
                {
                    _dPrice = 0;
                }
                else
                {
                    if (Utils.CDecDef(_news.ToList()[0].NEWS_PRICE2) != 0)
                    {
                        _dPrice = Utils.CDecDef(_news.ToList()[0].NEWS_PRICE2);
                    }
                    else
                    {
                        _dPrice = Utils.CDecDef(_news.ToList()[0].NEWS_PRICE1);
                    }
                }
            }

            int _cus_id = 0;

            //string _sCode = News_id + "_" + clsFormat.ClearUnicode(Color) + "_" + Size;
            if (_dPrice != 0)
            {
                if (Check_Exist_Item(News_id, (Guid)Guid))
                {
                    //Nếu đã có sản phẩm trong giỏ hàng thì thêm 1
                    //ESHOP_BASKET _bas = db.ESHOP_BASKETs.Single(a => a.NEWS_ID == News_id);

                    var _vBasket = db.GetTable<ESHOP_BASKET>().Where(a => a.NEWS_ID == News_id);
                    if (_vBasket.ToList().Count > 0)
                    {
                        _vBasket.Single().BASKET_QUANTITY += 1;
                        db.SubmitChanges();
                    }
                }
                else
                {
                    //Nếu chưa có sản phẩm trong giỏ hàng thì thêm sản phẩm mới vào đó
                    ESHOP_BASKET _basket = new ESHOP_BASKET();
                    _basket.BASKET_PRICE = _dPrice;
                    _basket.CUSTOMER_OID = (Guid)Guid;
                    _basket.BASKET_PUBLISHDATE = DateTime.Now;
                    _basket.BASKET_QUANTITY = 1;
                    _basket.CUSTOMER_ID = _cus_id;
                    _basket.NEWS_ID = News_id;
                    db.ESHOP_BASKETs.InsertOnSubmit(_basket);
                    db.SubmitChanges();
                }
                return true;
            }
            return false;
        }
        public bool Add_To_Cart(int News_id, object Guid, int quantity)
        {
            var _news = from a in db.ESHOP_NEWs
                        where a.NEWS_ID == News_id
                        select a;
            decimal _dPrice = 0;

            if (_news.ToList().Count > 0)
            {

                if (Utils.CDecDef(_news.ToList()[0].NEWS_PRICE1) == 0)
                {
                    _dPrice = 0;
                }
                else
                {
                    if (Utils.CDecDef(_news.ToList()[0].NEWS_PRICE2) != 0)
                    {
                        _dPrice = Utils.CDecDef(_news.ToList()[0].NEWS_PRICE2);
                    }
                    else
                    {
                        _dPrice = Utils.CDecDef(_news.ToList()[0].NEWS_PRICE1);
                    }
                }
            }

            int _cus_id = 0;

            //string _sCode = News_id + "_" + clsFormat.ClearUnicode(Color) + "_" + Size;
            if (_dPrice != 0)
            {
                if (Check_Exist_Item(News_id, (Guid)Guid))
                {
                    //Nếu đã có sản phẩm trong giỏ hàng thì thêm 1
                    //ESHOP_BASKET _bas = db.ESHOP_BASKETs.Single(a => a.NEWS_ID == News_id);

                    var _vBasket = db.GetTable<ESHOP_BASKET>().Where(a => a.NEWS_ID == News_id);
                    if (_vBasket.ToList().Count > 0)
                    {
                        _vBasket.Single().BASKET_QUANTITY += 1;
                        db.SubmitChanges();
                    }
                }
                else
                {
                    //Nếu chưa có sản phẩm trong giỏ hàng thì thêm sản phẩm mới vào đó
                    ESHOP_BASKET _basket = new ESHOP_BASKET();
                    _basket.BASKET_PRICE = _dPrice;
                    _basket.CUSTOMER_OID = (Guid)Guid;
                    _basket.BASKET_PUBLISHDATE = DateTime.Now;
                    _basket.BASKET_QUANTITY = Utils.CIntDef(quantity) != 0 ? quantity : 1;
                    _basket.CUSTOMER_ID = _cus_id;
                    _basket.NEWS_ID = News_id;
                    db.ESHOP_BASKETs.InsertOnSubmit(_basket);
                    db.SubmitChanges();
                }
                return true;
            }
            return false;
        }
        private bool Check_Exist_Item(int News_id, object Guid)
        {
            Guid _guid = (Guid)Guid;
            var rausach = from a in db.ESHOP_BASKETs
                          where a.CUSTOMER_OID == _guid && a.NEWS_ID == News_id
                          select a;
            if (rausach.ToList().Count > 0)
            {
                return true;
            }
            else
                return false;
        }
        

    }
}
