using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;
using System.Web;
using System.Web.UI;

namespace Controller
{
    public class Payment_cart
    {
        #region Decclare
        dbShopDataContext db = new dbShopDataContext();
        Config cf = new Config();
        public string _Mailbody = string.Empty;
        public static int _idorder = 0;
        #endregion
        // String email
        private string Email_product(string NEWS_TITLE, string BASKET_PRICE, int BASKET_QUANTITY, string _subTotal)
        {

            string _mailBody = string.Empty;
            int _mailCount = 1;
            _mailBody += "<tr>";
            _mailBody += "<td align='center' valign='middle' bgcolor='#FFFFFF' class='red arial14'>" + _mailCount++;
            _mailBody += "</td>";

            _mailBody += "<td align='left' valign='top' bgcolor='#FFFFFF'>" + NEWS_TITLE;// +"-" + item.BASKET_FIELD1 + "/" + item.BASKET_FIELD2;
            _mailBody += "</td>";

            _mailBody += "<td align='right' valign='middle' bgcolor='#FFFFFF'>" + BASKET_PRICE;
            _mailBody += "</td>";

            _mailBody += "<td align='right' valign='middle' bgcolor='#FFFFFF' class='green'>" + BASKET_QUANTITY;

            _mailBody += "</td>";

            _mailBody += "<td align='right' valign='middle' bgcolor='#FFFFFF' class='green arial14'>";
            _mailBody += "<strong>" + _subTotal;
            _mailBody += "</strong>";
            _mailBody += "</td>";

            _mailBody += "</tr>";

            return _mailBody;

        }
        private string Email_info_product_customer(string _mailBody,string _totalmoney, string _sEmail, string _sName, string _sPhone, string _sAddress, string hinhthuc, string _sDesc, string _nameweb, string _url_web)
        {
            string _mail_send = "<table width='100%' border='0' cellspacing='1' cellpadding='10' bgcolor='#CCCCCC'> <tr> <th width='10%' bgcolor='#E3E3E3' class='green'> STT </th> <th width='30%' align='center' bgcolor='#E3E3E3' class='green'> Tên sản phẩm </th> <th width='15%' align='center' bgcolor='#E3E3E3' class='green'> Đơn giá </th> <th width='10%' align='center' bgcolor='#E3E3E3' class='green'> Số lượng </th> <th width='20%' align='center' bgcolor='#E3E3E3' class='green'> Thành tiền </th> </tr>" +
                        _mailBody + " <tr> <th width='5%' align='center' bgcolor='#E3E3E3' class='green'></th></th> <th width='20%' align='center' bgcolor='#E3E3E3' class='green'></th><th width='15%' align='center' bgcolor='#E3E3E3' class='green'> </th><th width='20%' align='center' bgcolor='#E3E3E3' class='red arial14'>TỔNG TIỀN</th><th width='40%' align='right' bgcolor='#E3E3E3' class='red arial14'>" + _totalmoney + "</th></tr></table>";
            string _mail_userinfo = "";
            _mail_userinfo += "<br>Địa chỉ email : " + _sEmail;
            _mail_userinfo += "<br>Tên người mua : " + _sName;
            _mail_userinfo += "<br>Số điện thoại : " + _sPhone;
            _mail_userinfo += "<br>Địa chỉ : " + _sAddress;
            _mail_userinfo += "<br>Hình thức thanh toán : " + hinhthuc + "";
            _mail_userinfo += "<br>Ghi chú : " + _sDesc + "<br>";
            string _sMailBody = "";
            _sMailBody += "Cám ơn quý khách: " + _sName + " đã đặt hàng. Đây là email được gửi từ website của " + _nameweb + "<br>";
            _sMailBody += "<br>" + _mail_userinfo;
            _sMailBody += "<br>Xin vui lòng kiểm tra chi tiết đơn đặt hàng của bạn dưới đây: <br><br>" + _mail_send;
            _sMailBody += "<br>";
            _sMailBody += "<table width='100%' border='0' cellspacing='1' cellpadding='10' bgcolor='#CCCCCC'>";
            _sMailBody += "<tr>";
            _sMailBody += "<th align='left'>Thành tiền</th>";
            _sMailBody += "<td>" + _totalmoney + "</td>";
            _sMailBody += "</tr></table>";
            _sMailBody += "<br> - Cảm ơn bạn đã mua sắm tại website " + _url_web + ". Chúng tôi sẽ giao hàng cho bạn sớm nhất có thể.<br>";
            _sMailBody += "<br><hr>- Để biết thêm thông tin xin liên hệ với chúng tôi trên website: " + _url_web + ". Xin vui lòng không trả lời email này. Cảm ơn!";
            return _sMailBody;

        }
        //Get oder_code
        private string Getorder_code()
        {
            try
            {
                var _vOrderShop = db.GetTable<ESHOP_ORDER>().OrderByDescending(a => a.ORDER_ID);
                int iNo = 0;
                string _sMonth = string.Empty;
                string _sDay = string.Empty;
                if (_vOrderShop.ToList().Count > 0)
                {

                    iNo = _vOrderShop.ToList().Count > 0 ? Utils.CIntDef(_vOrderShop.ToList()[0].ORDER_ID) + 1 : 1;

                    _sMonth = DateTime.Now.Month.ToString().Length == 1 ? "0" + Utils.CStrDef(DateTime.Now.Month) : Utils.CStrDef(DateTime.Now.Month);

                    _sDay = DateTime.Now.Day.ToString().Length == 1 ? "0" + Utils.CStrDef(DateTime.Now.Day) : Utils.CStrDef(DateTime.Now.Day);
                }

                string _sOrderCode = DateTime.Now.Year + _sMonth + _sDay + "_" + iNo;
                return _sOrderCode;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        //Inser order
        private int Insert_Order(decimal Total_All, string Name, string OrderCode, string Email, string Address, int PaymentID, Guid News_guid, string Desc, DateTime Date, string Phone)
        {
            int _orderID = -1;
            ESHOP_ORDER _order = new ESHOP_ORDER();
            _order.ORDER_TOTAL_ALL = Total_All;
            _order.ORDER_NAME = Name;
            _order.ORDER_CODE = OrderCode;
            _order.ORDER_EMAIL = Email;
            _order.ORDER_ADDRESS = Address;
            _order.ORDER_PAYMENT = PaymentID;
            _order.CUSTOMER_OID = News_guid;
            _order.ORDER_PUBLISHDATE = DateTime.Now;
            _order.ORDER_STATUS = 0;
            _order.ORDER_FIELD1 = Desc;
            _order.ORDER_UPDATE = Date;
            _order.ORDER_PHONE = Phone;
            db.ESHOP_ORDERs.InsertOnSubmit(_order);
            db.SubmitChanges();

            var _getOrderID = db.GetTable<ESHOP_ORDER>().Where(a => a.CUSTOMER_OID == News_guid);
            _orderID = _getOrderID.ToList()[0].ORDER_ID;
            return _orderID;
        }
        private int Insert_Order(decimal Total_All, string Name, string OrderCode, string Email, string Address, int PaymentID, Guid News_guid, string Desc, DateTime Date, string Phone, decimal ship)
        {
            int _orderID = -1;
            ESHOP_ORDER _order = new ESHOP_ORDER();
            _order.ORDER_TOTAL_ALL = Total_All;
            _order.ORDER_TOTAL_AMOUNT = ship > 0 ? Total_All + ship : Total_All;
            _order.ORDER_SHIPPING_FEE = ship;
            _order.ORDER_NAME = Name;
            _order.ORDER_CODE = OrderCode;
            _order.ORDER_EMAIL = Email;
            _order.ORDER_ADDRESS = Address;
            _order.ORDER_PAYMENT = PaymentID;
            _order.CUSTOMER_OID = News_guid;
            _order.ORDER_PUBLISHDATE = DateTime.Now;
            _order.ORDER_STATUS = 0;
            _order.ORDER_FIELD1 = Desc;
            _order.ORDER_UPDATE = Date;
            _order.ORDER_PHONE = Phone;
            db.ESHOP_ORDERs.InsertOnSubmit(_order);
            db.SubmitChanges();

            var _getOrderID = db.GetTable<ESHOP_ORDER>().Where(a => a.CUSTOMER_OID == News_guid);
            _orderID = _getOrderID.ToList()[0].ORDER_ID;
            return _orderID;
        }
        private int Insert_Order1(int userId, decimal Total_All, string Name, string OrderCode, string Email, string Address, int PaymentID, Guid News_guid, string Desc, DateTime Date, string Phone)
        {
            int _orderID = -1;
            ESHOP_ORDER _order = new ESHOP_ORDER();
            _order.ORDER_TOTAL_ALL = Total_All;
            _order.ORDER_TOTAL_ALL = Total_All;
            _order.ORDER_NAME = Name;
            _order.ORDER_CODE = OrderCode;
            _order.ORDER_EMAIL = Email;
            _order.ORDER_ADDRESS = Address;
            _order.ORDER_PAYMENT = PaymentID;
            _order.CUSTOMER_OID = News_guid;
            _order.CUSTOMER_ID = userId;
            _order.ORDER_PUBLISHDATE = DateTime.Now;
            _order.ORDER_STATUS = 0;
            _order.ORDER_FIELD1 = Desc;
            _order.ORDER_UPDATE = Date;
            _order.ORDER_PHONE = Phone;
            db.ESHOP_ORDERs.InsertOnSubmit(_order);
            db.SubmitChanges();

            var _getOrderID = db.GetTable<ESHOP_ORDER>().Where(a => a.CUSTOMER_OID == News_guid);
            _orderID = _getOrderID.ToList()[0].ORDER_ID;
            return _orderID;
        }
        public bool Payment_cart_rs(Guid _guid, decimal _totalmoney, string _sEmail, string _sName, string _sPhone, string _sAddress, int _iPaymentID, string hinhthuc, string _sDesc, string _nameweb, string _url_web, decimal ship)
        {
            try
            {
                int _orderID = Insert_Order(_totalmoney, _sName, Getorder_code(), _sEmail, _sAddress, _iPaymentID, _guid, _sDesc, DateTime.Now, _sPhone, ship);
                //Lấy thông tin sản phẩm trong bảng giỏ hàng 
                _idorder = _orderID;
                var _product = from a in db.ESHOP_BASKETs
                               join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                               where a.CUSTOMER_OID == _guid
                               select new
                               {
                                   a.NEWS_ID,
                                   a.BASKET_PRICE,
                                   a.BASKET_QUANTITY,
                                   b.NEWS_TITLE
                               };

                //Thêm thông tin vào bảng chi tiết đơn hàng
                string _mailbody = string.Empty;
                foreach (var item in _product)
                {
                    decimal _subTotal = Convert.ToDecimal(double.Parse(item.BASKET_PRICE.ToString()) * double.Parse(item.BASKET_QUANTITY.ToString()));
                    ESHOP_ORDER_ITEM _orderItem = new ESHOP_ORDER_ITEM();
                    _orderItem.NEWS_ID = Utils.CIntDef(item.NEWS_ID);
                    _orderItem.ITEM_PRICE = item.BASKET_PRICE;
                    _orderItem.ITEM_PUBLISDATE = DateTime.Now;
                    _orderItem.ITEM_QUANTITY = item.BASKET_QUANTITY;
                    _orderItem.ITEM_SUBTOTAL = _subTotal;
                    _orderItem.ORDER_ID = _orderID;

                    db.ESHOP_ORDER_ITEMs.InsertOnSubmit(_orderItem);
                    db.SubmitChanges();
                    _mailbody += Email_product(item.NEWS_TITLE, FormatMoney(item.BASKET_PRICE), Utils.CIntDef(item.BASKET_QUANTITY), FormatMoney(_subTotal));
                }
                string noteship = (ship > 0 ? FormatMoney(ship) : (ship == 0 ? "Miễn phí" : "Liên hệ"));
                string total_amount = ship > 0 ? FormatMoney(_totalmoney + ship) : FormatMoney(_totalmoney);
                string _sMailBody = Email_info_product_customer(_mailbody, FormatMoney(_totalmoney), _sEmail, _sName, _sPhone, _sAddress, hinhthuc, _sDesc, _nameweb, _url_web, noteship, total_amount);
                _Mailbody = _sMailBody;
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private string Email_info_product_customer(string _mailBody, string _totalmoney, string _sEmail, string _sName, string _sPhone, string _sAddress, string hinhthuc, string _sDesc, string _nameweb, string _url_web, string ship, string total_amount)
        {
            string _mail_send = "<table width='100%' border='0' cellspacing='1' cellpadding='10' bgcolor='#CCCCCC'> <tr> <th width='10%' bgcolor='#E3E3E3' class='green'> STT </th> <th width='30%' align='center' bgcolor='#E3E3E3' class='green'> Tên sản phẩm </th> <th width='15%' align='center' bgcolor='#E3E3E3' class='green'> Đơn giá </th> <th width='10%' align='center' bgcolor='#E3E3E3' class='green'> Số lượng </th> <th width='20%' align='center' bgcolor='#E3E3E3' class='green'> Thành tiền </th> </tr>" +
                        _mailBody + " <tr> <th width='5%' align='center' bgcolor='#E3E3E3' class='green'></th></th> <th width='20%' align='center' bgcolor='#E3E3E3' class='green'></th><th width='15%' align='center' bgcolor='#E3E3E3' class='green'> </th><th width='20%' align='center' bgcolor='#E3E3E3' style='font-weight: normal;'>TỔNG TIỀN</th><th width='40%' align='right' bgcolor='#E3E3E3' style='font-weight: normal;'>" + _totalmoney + "</th></tr></table>";
            string _mail_userinfo = "";
            _mail_userinfo += "<br>Địa chỉ email : " + _sEmail;
            _mail_userinfo += "<br>Tên người mua : " + _sName;
            _mail_userinfo += "<br>Số điện thoại : " + _sPhone;
            _mail_userinfo += "<br>Địa chỉ : " + _sAddress;
            _mail_userinfo += "<br>Hình thức thanh toán : " + hinhthuc + "";
            _mail_userinfo += "<br>Ghi chú : " + _sDesc + "<br>";
            string _sMailBody = "";
            _sMailBody += "Cám ơn quý khách: " + _sName + " đã đặt hàng. Đây là email được gửi từ website của " + _nameweb + "<br>";
            _sMailBody += "<br>" + _mail_userinfo;
            _sMailBody += "<br>Xin vui lòng kiểm tra chi tiết đơn đặt hàng của bạn dưới đây: <br><br>" + _mail_send;
            _sMailBody += "<br>";
            _sMailBody += "<table width='100%' border='0' cellspacing='1' cellpadding='10' bgcolor='#CCCCCC'>";
            _sMailBody += "<tr>";
            _sMailBody += "<th align='left' style='font-weight: normal;'>Phí vận chuyển</th>";
            _sMailBody += "<td>" + ship + "</td>";
            _sMailBody += "</tr>";
            _sMailBody += "<tr>";
            _sMailBody += "<th align='left'>Thành tiền</th>";
            _sMailBody += "<td style='color: #FF6600;font-weight: bold;'>" + total_amount + "</td>";
            _sMailBody += "</tr></table>";
            _sMailBody += "<br> - Cảm ơn bạn đã mua sắm tại website " + _url_web + ". Chúng tôi sẽ giao hàng cho bạn sớm nhất có thể.<br>";
            _sMailBody += "<br><hr>- Để biết thêm thông tin xin liên hệ với chúng tôi trên website: " + _url_web + ". Xin vui lòng không trả lời email này. Cảm ơn!";
            return _sMailBody;

        }
        //Payment
        public bool Payment_cart_rs(Guid _guid, decimal _totalmoney, string _sEmail, string _sName, string _sPhone, string _sAddress,int _iPaymentID,string hinhthuc, string _sDesc, string _nameweb, string _url_web)
        {
            try
            {
                int _orderID = Insert_Order(_totalmoney, _sName, Getorder_code(), _sEmail, _sAddress, _iPaymentID, _guid, _sDesc, DateTime.Now, _sPhone);
                 //Lấy thông tin sản phẩm trong bảng giỏ hàng 
                var _product = from a in db.ESHOP_BASKETs
                               join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                               where a.CUSTOMER_OID == _guid
                               select new
                               {
                                   a.NEWS_ID,
                                   a.BASKET_PRICE,
                                   a.BASKET_QUANTITY,
                                   b.NEWS_TITLE
                               };

                //Thêm thông tin vào bảng chi tiết đơn hàng
                string _mailbody = string.Empty;
                foreach (var item in _product)
                {
                    decimal _subTotal = Convert.ToDecimal(double.Parse(item.BASKET_PRICE.ToString()) * double.Parse(item.BASKET_QUANTITY.ToString()));
                    ESHOP_ORDER_ITEM _orderItem = new ESHOP_ORDER_ITEM();
                    _orderItem.NEWS_ID = Utils.CIntDef(item.NEWS_ID);
                    _orderItem.ITEM_PRICE = item.BASKET_PRICE;
                    _orderItem.ITEM_PUBLISDATE = DateTime.Now;
                    _orderItem.ITEM_QUANTITY = item.BASKET_QUANTITY;
                    _orderItem.ITEM_SUBTOTAL = _subTotal;
                    _orderItem.ORDER_ID = _orderID;

                    db.ESHOP_ORDER_ITEMs.InsertOnSubmit(_orderItem);
                    db.SubmitChanges();
                    _mailbody+=Email_product(item.NEWS_TITLE, FormatMoney(item.BASKET_PRICE), Utils.CIntDef(item.BASKET_QUANTITY),FormatMoney(_subTotal));
                }
                string _sMailBody=Email_info_product_customer(_mailbody,FormatMoney(_totalmoney), _sEmail, _sName, _sPhone, _sAddress, hinhthuc, _sDesc, _nameweb, _url_web);
                _Mailbody = _sMailBody;
                return true;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public bool Payment_cart_rs1(int userId, Guid _guid, decimal _totalmoney, string _sEmail, string _sName, string _sPhone, string _sAddress, int _iPaymentID, string hinhthuc, string _sDesc, string _nameweb, string _url_web)
        {
            try
            {
                int _orderID = Insert_Order1(userId, _totalmoney, _sName, Getorder_code(), _sEmail, _sAddress, _iPaymentID, _guid, _sDesc, DateTime.Now, _sPhone);
                //Lấy thông tin sản phẩm trong bảng giỏ hàng 
                _idorder = _orderID;
                var _product = from a in db.ESHOP_BASKETs
                               join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                               where a.CUSTOMER_OID == _guid
                               select new
                               {
                                   a.NEWS_ID,
                                   a.BASKET_PRICE,
                                   a.BASKET_QUANTITY,
                                   b.NEWS_TITLE
                               };

                //Thêm thông tin vào bảng chi tiết đơn hàng
                string _mailbody = string.Empty;
                foreach (var item in _product)
                {
                    decimal _subTotal = Convert.ToDecimal(double.Parse(item.BASKET_PRICE.ToString()) * double.Parse(item.BASKET_QUANTITY.ToString()));
                    ESHOP_ORDER_ITEM _orderItem = new ESHOP_ORDER_ITEM();
                    _orderItem.NEWS_ID = Utils.CIntDef(item.NEWS_ID);
                    _orderItem.ITEM_PRICE = item.BASKET_PRICE;
                    _orderItem.ITEM_PUBLISDATE = DateTime.Now;
                    _orderItem.ITEM_QUANTITY = item.BASKET_QUANTITY;
                    _orderItem.ITEM_SUBTOTAL = _subTotal;
                    _orderItem.ORDER_ID = _orderID;

                    db.ESHOP_ORDER_ITEMs.InsertOnSubmit(_orderItem);
                    db.SubmitChanges();
                    _mailbody += Email_product(item.NEWS_TITLE, FormatMoney(item.BASKET_PRICE), Utils.CIntDef(item.BASKET_QUANTITY), FormatMoney(_subTotal));
                }
                string _sMailBody = Email_info_product_customer(_mailbody, FormatMoney(_totalmoney), _sEmail, _sName, _sPhone, _sAddress, hinhthuc, _sDesc, _nameweb, _url_web);
                _Mailbody = _sMailBody;
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Delete basket
        public void Delete_basket(Guid _deleteBasket)
        {
            try
            {
                //sau khi thêm, xóa hết sản phẩm trong giỏ hàng của người đó
                var _bas = from a in db.ESHOP_BASKETs
                           where a.CUSTOMER_OID == _deleteBasket
                           select a;
                if (_bas.ToList().Count > 0)
                {
                    db.ESHOP_BASKETs.DeleteAllOnSubmit(_bas);

                    db.SubmitChanges();

                    HttpContext.Current.Session["News_guid"] = Guid.NewGuid();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        //Check cart
        public bool Check_Cart(Guid _guid)
        {
            try
            {
                var _vCheck = db.GetTable<ESHOP_BASKET>().Where(a => a.CUSTOMER_OID == _guid);
                if (_vCheck.ToList().Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return false;
            }
        }
        private string FormatMoney(object Expression)
        {
            try
            {
                string Money = String.Format("{0:0,0 VNĐ}", Expression);
                return Money.Replace(",", ".");
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
       
    }
}
