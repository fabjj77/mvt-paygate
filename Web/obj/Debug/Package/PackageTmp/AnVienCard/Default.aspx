<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web.PayCard.Default" %>
<%@ Register src="~/usercontrols/uc_otherCard.ascx" tagname="uc_otherCard" tagprefix="uc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        var BASE_URL = 'http://thanhtoan.truyenhinhanvien.vn';
        var TEMPLATE_URL = 'http://thanhtoan.truyenhinhanvien.vn/wp-content/themes/avgtv';
    </script>
    <script src="/AnVienCard/Script/General.js" type="text/javascript"></script>
    <style>
        .pay-right .widget-payment-method {
            background: url("/IMG/pay-widget-bg.png") no-repeat scroll -4px 0 transparent;
            display: inline-block;
            overflow: hidden;
        }
        .pay-right .widget-payment-method #payment-method-content {
            background: url("/IMG/pay-widget-bg.png") no-repeat scroll -4px -255px transparent;
            display: block;
            overflow: hidden;
            height: 260px;
            width: 267px;
            opacity: 0.5;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <form id="form1" runat="server">
    <div id="avg-main-content" class="fullwidth" style="overflow: hidden">
        <div class="pay-left">
            <div class="pay">
                <div id="pay-loading">
                    <div class="loading-content">Đang thực hiện giao dịch...</div>
                </div>
                <h1 class="pay-h1">
                    Hệ thống thanh toán cước bằng thẻ cào của Truyền Hình An Viên</h1>
                <div class="pay-wrapper">
                    <input type="hidden" name="gw" value="paycard" />
                    <div class="pay-background pay-field">
                        <div class="pay-background pay-field-icon">
                        </div>
                        <div class="pay-field-content">
                            <div class="field-left">
                                <div class="pay-head">
                                    <b>Bước 1:</b> <em>Nhập thông tin thẻ cào</em></div>
                                <div class="pay-input">
                                    <input type="text" id="am_CardCode" name="am_CardCode" size="20" placeholder="Nhập Mã thẻ cào"
                                        maxlength="12" tabindex="1" class="pay-background" onfocus="if(this.value=='Nhập Mã thẻ cào')this.value='';"
                                        onblur="if(this.value=='')this.value='Nhập Mã thẻ cào';" autocomplete="off" /><em class="pay-background error-message"></em></div>
                            </div>
                            <div class="field-right pay-message">
                                Mã số thẻ cào gồm 12 ký tự là số ở phía sau của thẻ cào<br />
                                Bấm vào <a href="javascript:;" onclick="helpGetCard()" title="Hướng dẫn xem thông tin mã số thẻ cào">
                                    <strong>đây</strong></a> để xem hướng dẫn
                            </div>
                            <div class="help">
                                <div style="display: none;" class="radiums" id="cardInfo">
                                    <a title="Đóng cửa sổ" class="pay-background close">&nbsp;</a>
                                    <h2>
                                        Hướng dẫn lấy thông tin mã thẻ</h2>
                                    <div class="content">
                                        <div align="center">
                                            <img src="http://www.anvien.tv/wp-content/uploads/2012/05/huong-dan-xem-ma-so-the-cao.png"
                                                title="huong-dan-xem-ma-so-the-cao" class="size-full wp-image-166763 aligncenter">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="pay-background pay-field">
                        <div class="pay-background pay-field-icon">
                        </div>
                        <div class="pay-field-content">
                            <div class="field-left">
                                <div class="pay-head head-step2">
                                    <b>Bước 2:</b> <em>Nhập thông tin thuê bao</em></div>
                                <div class="pay-color pay-radio">
                                    <strong class="pay-color">Bạn nhập theo: </strong>
                                    <label>
                                        <input type="radio" value="1" name="am_InType" id="am_InType-1" checked="checked"
                                            onclick="paycardInTypeClick(this.value);" />
                                        Mã số thẻ</label>
                                    <label>
                                        <input type="radio" value="2" name="am_InType" id="am_InType-2" onclick="paycardInTypeClick(this.value);" />
                                        Mã hợp đồng</label>
                                </div>
                                <div class="pay-input">
                                    <input type="text" id="am_STB" name="am_STB" value="Nhập 12 ký tự Mã số thẻ vào đây"
                                        size="20" maxlength="14" tabindex="2" class="pay-background" onfocus="paycardFocusSTB();"
                                        onblur="paycardBlurSTB();" /><em class="pay-background error-message"></em></div>
                            </div>
                            <div class="field-right pay-message message-step2">
                                Để tra mã số thẻ khách hàng nhấn nút <b>"Hỗ trợ"</b> (i) ba lần trên điều khiển
                                đầu thu<br />
                                Bấm vào <a href="javascript:;" onclick="helpGetSmartCard()" title="Hướng dẫn xem thông tin mã số thuê bao">
                                    <strong>đây</strong></a> để xem hướng dẫn<br />
                                Để tra <strong>"Mã hợp đồng"</strong> khách hàng xem trên hợp đồng
                            </div>
                                    
                            <div class="help">
                                <div style="display: none;" class="radiums" id="smartCardInfo">
                                    <a class="pay-background close">&nbsp;</a>
        	                        <h2>Hướng dẫn lấy thông tin thuê bao</h2>
                                    <div class="content"><h3>Cách 1: Tìm số thẻ bằng cách sử dụng điều khiển đầu thu và màn hình TV</h3>
                                
                                <center>
                                        Bước 1: Dùng điều khiển đầu thu
                                    <p align="center" style="text-align: center;">
                                        <a href="http://www.anvien.tv/wp-content/uploads/2012/05/b1-c1.png">
                                            <img width="200" height="233" alt="" src="http://www.anvien.tv/wp-content/uploads/2012/05/b1-c1.png"
                                                title="b1-c1" class="size-full wp-image-50121 aligncenter"></a></p>
                                    &nbsp; Nhấn nút i 3 lần, màn hình sẽ hiển thị số thẻ. Số thẻ được hiển thị trên
                                    màn hình như bên. Bước 2: Màn hình hiển thị số thẻ &nbsp;
                                    <p align="center" style="text-align: center;">
                                        <a href="http://www.anvien.tv/wp-content/uploads/2012/05/b2.png">
                                            <img width="380" alt="" src="http://www.anvien.tv/wp-content/uploads/2012/05/b2.png"
                                                title="b2" class="aligncenter size-full wp-image-50123"></a></p>
                                    &nbsp;
                                    <p style="clear: both">
                                    </p>
                                    <h3>
                                        Cách 2: Tìm số thẻ bằng cách sử dụng thẻ của đầu thu AVG</h3>
                                    Bước 1: Rút thẻ của đầu thu
                                    <p align="center" style="text-align: center;">
                                        <a href="http://www.anvien.tv/wp-content/uploads/2012/05/b1-c2.png">
                                            <img width="380" alt="" src="http://www.anvien.tv/wp-content/uploads/2012/05/b1-c2.png"
                                                title="b1-c2" class="aligncenter size-full wp-image-50122"></a></p>
                                    &nbsp; Bước 2: Đọc số thẻ
                                    <p align="center" style="text-align: center;">
                                        <a href="http://www.anvien.tv/wp-content/uploads/2012/05/b2-c2.png">
                                            <img width="380" alt="" src="http://www.anvien.tv/wp-content/uploads/2012/05/b2-c2.png"
                                                title="b2-c2" class="aligncenter size-full wp-image-50124"></a></p>
                                    &nbsp;
                                    <p align="</center>" style="text-align: center;"><a href="http://www.anvien.tv/wp-content/uploads/2012/05/b3-c2.png"><img width="380" alt="" src="http://www.anvien.tv/wp-content/uploads/2012/05/b3-c2.png" title="b3-c2" class="aligncenter size-full wp-image-50125"></a></p></div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="pay-background pay-field">
                        <div class="pay-background pay-field-icon">
                        </div>
                        <div class="pay-field-content">
                            <div class="field-left">
                                <div class="pay-head head-step3">
                                    <b>Bước 3:</b> <em>Xác nhận mã kiểm tra</em></div>
                                <div class="pay-input">
                                    <img id="imgCaptcha" style="margin-top: 10px;width: 60px;height: 25px;" src="/Ajax/captcha.ashx?v=1"
                                        onclick="setTimeout('RefreshCaptcha()', 300); return false;" alt="Captcha"
                                        title="Chọn ảnh khác" align="absmiddle" />
                                    <img src="http://thanhtoan.truyenhinhanvien.vn/wp-content/themes/avgtv/images/refresh.gif"
                                        id="refresh" class="refresh" width="25" height="22" border="0" alt="Chọn ảnh khác"
                                        title="Chọn ảnh khác" align="absmiddle" onclick="setTimeout('RefreshCaptcha()', 300); return false;" />
                                    <input type="hidden" id="am_HasCatpcha" name="hasCatpcha" value="" />
                                    <input id="am_Captcha" type="text" name="am_Captcha" value="Nhập Mã kiểm tra" maxlength="5" placeholder="Nhập Mã kiểm tra"
                                        size="10" autocomplete="off" tabindex="3" class="pay-background" onfocus="if(this.value=='Nhập Mã kiểm tra')this.value='';"
                                        onblur="if(this.value=='')this.value='Nhập Mã kiểm tra';" /><em class="pay-background error-message"></em>
                                </div>
                            </div>
                            <div class="field-right pay-message message-step3">
                                Nhấn nút
                                <img src="http://thanhtoan.truyenhinhanvien.vn/wp-content/themes/avgtv/images/refresh.gif"
                                    idth="25" height="22" border="0" alt="Chọn ảnh khác" title="Chọn ảnh khác" align="absmiddle"
                                    original="http://thanhtoan.truyenhinhanvien.vn/wp-content/themes/avgtv/images/refresh.gif"
                                    style="display: inline;">
                                để có mã mới.
                            </div>
                        </div>
                    </div>
                    <div class="pay-button">
                        <input type="button" id="paySubmits" name="SubButL" value="Thanh toán" class="pay-background payment-submit"
                            tabindex="4" /></div>
                </div>
                <script type="text/javascript">
                    (function ($) {
                        $(document).ready(function () {
                            $('#am_CardCode').focus();
                            $(".pay-left .pay-field").live('click focus', function () {
                                $(this).children('.pay-field-icon').addClass('pay-field-icon-hover');
                            }).live('blur', function () {
                                $(this).children('.pay-field-icon').removeClass('pay-field-icon-hover');
                            }).hover(function () {
                                $(this).children('.pay-field-icon').addClass('pay-field-icon-hover');
                            }, function () {
                                $(this).children('.pay-field-icon').removeClass('pay-field-icon-hover');
                            });
                        });
                    })(jQuery)
                </script>
            </div>
        </div>
        <div class="pay-right">
            <div id="avg-sidebar">
                <div id="avgpaymentsidebar-5" class="avg-content-box widget_avgpaymentsidebar">
                    <uc1:uc_otherCard ID="uc_otherCard1" runat="server" />
                </div>
                <div id="text-3" class="avg-content-box widget_text">
                    <div class="textwidget">
                        <img src="http://thanhtoan.truyenhinhanvien.vn/wp-content/uploads/2013/06/1900-19006.png" /></div>
                </div>
            </div>
        </div>
    </div>
    </form>
</asp:Content>

