<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Web.GateCard.Default" %>

<%@ Register src="~/usercontrols/uc_otherCard.ascx" tagname="uc_otherCard" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Hệ thống thanh toán cước THAV bằng thẻ cào điện thoại</title>
    <style>
        #Div2, #Div3
        {
            display: none;
        }
        .InputStyle
        {
            background-position: 82% -270px;
            border: 1px solid #999999;
            border-radius: 8px;
            color: #8D5A27;
            display: block;
            font-size: 12px;
            height: 25px;
            line-height: 25px;
            padding: 5px 10px;
            width: 210px;
        }
        .pay-field-content2 
        {
            margin-top: 10px;
            width: 540px;
            display: block;
            margin-left: 30px;
        }
        .pay-field-content2 table tr {
            margin-top: 6px;
            display: block;
        }
        .pay-field-content2 table tr td {
            padding-left: 30px;
        }
        
        .pay-field-content2 table tr td input{
            margin-top: 5px;
        }
        
        .DDLType {
            border: 1px solid #aaa;
            color: #777;
            border-radius: 8px;
            -moz-border-radius: 8px;
            -webkit-border-radius: 8px;
            line-height: 38px;
            padding: 8px 5px;
            width: 230px;
            margin-top: 5px;
        }
        
        .field-left2 {
            display:inline-block;
            width: 240px;
        }
        
        .pay-left .banknet .banknet-step{
            background: url("/IMG/payoo-step.png") no-repeat scroll 0 -28px transparent;
            border-radius: 20px;
        }
        .pay-left .banknet .step-2 {
            background: url("/IMG/payoo-step.png") no-repeat scroll 0 -98px transparent;
        }
        
        .pay-left .banknet .step-3 {
            background: url("/IMG/payoo-step.png") no-repeat scroll 0 -168px transparent;
        }
        
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
    <script src="/MobileCard/script/script.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    <div id="avg-content-wrapper">
        <!-- PHOTO SLIDE AREA -->
        <div class="fullwidth" id="avg-main-content">
            <div class="pay-left">
                <div class="pay">
                    <div id="pay-loading">
                        <div class="loading-content" id="Loading-content">Đang thực hiện giao dịch...</div>
                    </div>
                    <h1 class="pay-h1">
                        Hệ thống thanh toán cước THAV bằng thẻ cào điện thoại</h1>
                    <div class="banknet-info" id="banknet-infor" style="display: none;">
                        <div class="banknet-what-bg banknet-infor-head">
                            <img src=""><a class="pay-background close" href="javascript:;">&nbsp;</a></div>
                        <%--<div class="pay-color banknet-infor-content" id="jsPanel">
                            <p>
                                <strong>&nbsp;Banknetvn là gì:</strong> Banknetvn là Công ty Cổ Phần Chuyển mạch
                                Tài chính Quốc gia Việt Nam.</p>
                            <p>
                                <strong>Banknetvn</strong> được Chính phủ đầu tư vốn góp thông qua Ngân hàng nhà
                                nước để thực hiện sứ mệnh trở thành trung tâm chuyển mạch thẻ thống nhất Quốc gia,
                                thực hiện chuyển mạch thẻ trên ATM/POS đối với hệ thống các ngân hàng Việt Nam và
                                liên kết chuyển mạch thẻ với các tổ chức chuyển mạch tài chính trong khu vực và
                                toàn thế giới.<br>
                                Dịch vụ Thanh toán hóa đơn của <strong>Banknetvn</strong> cho phép khách hàng thanh
                                toán an toàn, tiện lợi, nhanh chóng các loại hóa đơn: Truyền hình cáp, Điện, Nước,
                                Internet, Học phí, Cước viễn thông… trực tiếp bằng thẻ/tài khoản Ngân hàng.</p>
                        </div>--%>
                        <div class="banknet-what-bg banknet-infor-foot">
                        </div>
                    </div>
                    <%--<form method="post" action="/Ajax/GateCardCmd.ashx.cs" onsubmit="return false;">--%>
                    <div class="pay-wrapper banknet">
                        <%--<div class="banknet-what">
                            <a charset="banknet-help-link" href="javascript:;">PayGate là gì?</a></div>--%>
                        <div id="Div_Step" class="banknet-step step-1 cleanfix">
                            &nbsp;</div>
                        <div id="Div1">
                            <div class="pay-background pay-field">
                                <div class="pay-background pay-field-icon">
                                </div>
                                <div class="pay-field-content">
                                    <div class="field-left">
                                        <div class="pay-color pay-radio">
                                            <strong class="pay-color">Bạn nhập theo: </strong>
                                            <label>
                                                <input type="radio" value="12" id="check1" name="inputType" checked="checked" onclick="paycardInTypeClick(12);" />Mã
                                                số thẻ</label>
                                            <label>
                                                <input type="radio" value="14" id="check2" name="inputType" onclick="paycardInTypeClick(14);" />Mã
                                                hợp đồng</label>
                                        </div>
                                        <div class="pay-input">
                                            <input type="text" onblur="paycardBlurSTB();" onfocus="paycardFocusSTB();" class="pay-background InputStyle"
                                                placeholder="Nhập 12 ký tự Mã số thẻ vào đây" tabindex="2" maxlength="12" size="20"
                                                name="TB_KeyID" id="TB_KeyID" autocomplete="off" />
                                            <%--    <em class="pay-background error-message error">Chưa nhập Mã hợp đồng</em>--%>
                                        </div>
                                    </div>
                                    <div class="field-right pay-message help-step1">
                                        Để tra mã số thẻ khách hàng nhấn nút <b>"Hỗ trợ"</b> (i) ba lần trên điều khiển
                                        đầu thu. Bấm vào <a onclick="helpGetSmartCard();" href="javascript:void(0);"><strong>đây</strong></a>
                                        để xem hướng dẫn.<br>
                                        <br>
                                        Để tra <strong>"Mã hợp đồng"</strong>, khách hàng xem trên hợp đồng
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
                                            <em>Xác nhận mã kiểm tra</em></div>
                                        <div class="pay-input">
                                            <img align="absmiddle" title="Chọn ảnh khác" alt="Captcha" src="/Ajax/captcha.ashx?v=1"
                                                style="margin-top: 10px; width: 60px; height: 25px;" id="imgCaptcha">
                                            <img width="25" border="0" align="absmiddle" height="22" title="Chọn ảnh khác" alt="Chọn ảnh khác"
                                                class="refresh" id="refresh" src="http://thanhtoan.truyenhinhanvien.vn/wp-content/themes/avgtv/images/refresh.gif">
                                            <input type="hidden" value="" name="hasCatpcha" id="am_HasCatpcha">
                                            <input type="text" onblur="if(this.value=='')this.value='Nhập Mã kiểm tra';" onfocus="if(this.value=='Nhập Mã kiểm tra')this.value='';"
                                                class="pay-background" tabindex="3" autocomplete="off" size="10" placeholder="Nhập Mã kiểm tra"
                                                maxlength="5" value="Nhập Mã kiểm tra" name="am_Captcha" id="am_Captcha"><em class="pay-background error-message"></em>
                                        </div>
                                    </div>
                                    <div class="field-right pay-message message-step3">
                                        Nhấn nút
                                        <img border="0" align="absmiddle" height="22" style="display: inline;" original="http://thanhtoan.truyenhinhanvien.vn/wp-content/themes/avgtv/images/refresh.gif"
                                            title="Chọn ảnh khác" alt="Chọn ảnh khác" idth="25" src="http://thanhtoan.truyenhinhanvien.vn/wp-content/themes/avgtv/images/refresh.gif">
                                        để có mã mới.
                                    </div>
                                </div>
                            </div>
                            
                            <div class="pay-button">
                                <input type="button" tabindex="4" class="pay-background payment-submit" value="Tiếp tục"
                                    name="SubButL" id="BT_GetInfo">
                            </div>
                        </div>
                        <div id="Div2">
                            <div>
                                <div class="pay-field-content2">
                                    <div class="pay-head head-step2"><b>Thông tin thuê bao:</b></div>
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td width="50%">
                                                <div class="pay-color"><strong class="pay-color">Số hợp đồng: </strong></div>
                                                <div class="pay-input">
                                                    <input id="subnum" readonly="readonly" type="text" class="pay-background InputStyle" size="20" />
                                                </div>
                                            </td>
                                            <td width="50%">
                                                <div class="pay-color"><strong class="pay-color">Họ tên: </strong></div>
                                                <div class="pay-input">
                                                    <input id="contactname" readonly="readonly" type="text" class="pay-background InputStyle" size="20" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%">
                                                <div class="pay-color"><strong class="pay-color">Địa chỉ: </strong></div>
                                                <div class="pay-input">
                                                    <input id="contactaddress" readonly="readonly" type="text" class="pay-background InputStyle" size="20" />
                                                </div>
                                            </td>
                                            <td width="50%">
                                                <div class="pay-color"><strong class="pay-color">Số điện thoại: </strong></div>
                                                <div class="pay-input">
                                                    <input id="contactphone" readonly="readonly" type="text" class="pay-background InputStyle" size="20" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%">
                                                <div class="pay-color"><strong class="pay-color">Email: </strong></div>
                                                <div class="pay-input">
                                                    <input id="contactemail" readonly="readonly" type="text" class="pay-background InputStyle" size="20" />
                                                </div>
                                            </td>
                                            <td width="50%">
                                                <div class="pay-color"><strong class="pay-color">Gói dịch vụ: </strong></div>
                                                <div class="pay-input">
                                                    <input id="servicename" readonly="readonly" type="text" class="pay-background InputStyle" size="20" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%">
                                                <div class="pay-color"><strong class="pay-color">Trạng thái: </strong></div>
                                                <div class="pay-input">
                                                    <input id="substatusname" readonly="readonly" type="text" class="pay-background InputStyle" size="20" />
                                                </div>
                                            </td>
                                            <td width="50%">
                                                <div class="pay-color"><strong class="pay-color">Thời hạn: </strong></div>
                                                <div class="pay-input">
                                                    <input id="expirationdate" readonly="readonly" type="text" class="pay-background InputStyle" size="20" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <!--thanh toán-->

                            <div class="pay-background pay-field">
                                <div class="pay-background pay-field-icon"></div>
                                <div class="pay-field-content">
                                    <div class="pay-head head-step" style="color: red;font-weight: bold;margin-top: 10px;"><b>Chú ý:</b> chỉ áp dụng với thẻ nạp có mệnh giá 100.000 vnđ, 200.000 vnđ, 500.000 vnđ</div>
                                    <div class="field-left">
                                        <div class="pay-head"><em>Chọn thẻ nạp:</em></div>
                                        <div class="pay-input">
                                            <%--<asp:DropDownList ID="DDL_CardType" runat="server" CssClass="DDLType"></asp:DropDownList>--%>
                                            <%= sDDLType%>
                                            <%--<select class="DDLType" id="MainContent_DDL_CardType" name="ctl00$MainContent$DDL_CardType">
	                                            <option value="CardInputViettel" selected="selected">Thẻ Viettel</option>
	                                            <option value="CardInputVMS">Thẻ Mobiphone</option>
	                                            <option value="CardInputVNP">Thẻ Vinaphone</option>
	                                            <option value="CardInputGate">Thẻ Gate</option>
                                            </select>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                              
                            <div class="pay-background pay-field">
                                <div class="pay-background pay-field-icon"></div>
                                <div class="pay-field-content">
                                    <div class="field-left2">
                                        <div class="pay-head"> <em>Số serials thẻ:</em></div>
                                        <div class="pay-input">
                                            <input type="text" class="pay-background InputStyle" placeholder="Nhập số serials thẻ vào đây" tabindex="2" maxlength="20" size="20"id="CardSerials" />
                                        </div>
                                    </div>
                                    
                                    <div class="field-left2" style="padding-left: 20px;">
                                        <div class="pay-head"> <em>Số Thẻ:</em></div>
                                        <div class="pay-input">
                                            <input type="text" class="pay-background InputStyle" placeholder="Nhập số thẻ vào đây" tabindex="3" maxlength="20" size="20" id="CardPin" />
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
                                            <em>Xác nhận mã kiểm tra</em></div>
                                        <div class="pay-input">
                                            <img align="absmiddle" title="Chọn ảnh khác" alt="Captcha" src="/Ajax/captcha.ashx?v=1"
                                                style="margin-top: 10px; width: 60px; height: 25px; float: left;" id="imgCaptcha2">
                                            <img width="25" border="0" align="absmiddle" height="22" title="Chọn ảnh khác" alt="Chọn ảnh khác"
                                                class="refresh" id="refresh2" src="http://thanhtoan.truyenhinhanvien.vn/wp-content/themes/avgtv/images/refresh.gif">
                                            <input type="text" onblur="if(this.value=='')this.value='Nhập Mã kiểm tra';" onfocus="if(this.value=='Nhập Mã kiểm tra')this.value='';"
                                                class="pay-background InputStyle" tabindex="4" autocomplete="off" size="10" placeholder="Nhập Mã kiểm tra"
                                                maxlength="5" value="Nhập Mã kiểm tra" name="am_Captcha2" id="am_Captcha2" style="width: 114px;"><em class="pay-background error-message"></em>
                                        </div>
                                    </div>
                                    <div class="field-right pay-message message-step3">
                                        Nhấn nút
                                        <img border="0" align="absmiddle" height="22" style="display: inline;" original="http://thanhtoan.truyenhinhanvien.vn/wp-content/themes/avgtv/images/refresh.gif"
                                            title="Chọn ảnh khác" alt="Chọn ảnh khác" idth="25" src="http://thanhtoan.truyenhinhanvien.vn/wp-content/themes/avgtv/images/refresh.gif">
                                        để có mã mới.
                                    </div>
                                </div>
                            </div>
                            
                            <div class="pay-button">
                                <input type="button" tabindex="5" class="pay-background payment-submit" value="Thanh toán"
                                       name="SubButL" id="BT_submit"/>
                            </div>
                        </div>
                        <div id="Div3">
                            <div class="pay-finish pay-sucess">
  	                            <div class="pay-background finish-icon success-icon"></div>
                                <div class="success-icon">
    	                            <h2 class="success">Thanh toán thành công</h2>
			                            <p>&nbsp;</p>
                                        <p>Thời hạn hợp đồng của bạn kéo dài đến <b><span id="Span_Time"></span></b></p>
			                            <p align="center"><a href="/MobileCard/"><strong>Thực hiện tiếp thanh toán</strong></a></p>
			                            <p>&nbsp;</p>
                                </div>
                              </div>
                        </div>
                    </div>
                    <%--</form>--%>
                </div>
            </div>
            <div class="pay-right">
                <div id="avg-sidebar">
                    <div class="avg-content-box widget_avgpaymentsidebar" id="avgpaymentsidebar-5">
                        <uc1:uc_otherCard ID="uc_otherCard1" runat="server" />
                    </div>
                    <div class="avg-content-box widget_text" id="text-3">
                        <div class="textwidget">
                            <img src="http://thanhtoan.truyenhinhanvien.vn/wp-content/uploads/2013/06/1900-19006.png"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
