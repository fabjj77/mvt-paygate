<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Cổng thanh toán Truyền Hình An Viên | Nguồn lợi ích của đồng bào. Niềm tự hào của người Việt</title>
    <meta name='description' content='Truyền hình An Viên, nguồn lợi ích của đồng bào, niềm tự hào của người Việt. Truyền hình HD giá rẻ chất lượng cao' />
    <meta name='news_keywords' content='truyen hinh, an vien, hd, gia re, chat luong cao, tin tuc, the thao, phim hay, am nhac' />
    <script>
        (function ($) {
            $(document).ready(function () {
                $('.payment .payment-index .payment-item').hover(
		function () {
		    $(this).addClass('payment-item-hover');
		}, function () {
		    $(this).removeClass('payment-item-hover');
		});
            });
        })(jQuery);
        
        function redirect(url) {
            window.location.href = url;
        }
    </script>
    <style>
        #payIndex {
            width: 900px;
            height: 408px;
            border: none;
            position: relative;
            display: block;
        }
        #payIndex a {
            height: 100%;
            width: 100%;
            display: block;
            text-decoration: none;
        }
        
        #payIndex #cardAV 
        {
            background: url("/IMG/thanhtoanantv.png") no-repeat scroll 0 0 transparent;
            /*height: 210px;*/
            height: 420px;
            opacity: 1;
            position: absolute;
            width: 210px;
        }
        
        #payIndex  #CardMobile {
            background: url('/IMG/thanhtoanantv.png') 0 -210px no-repeat;
            opacity: 1;
            position: absolute;
            width: 210px;
            height: 210px;
            top: 210px;
        }
        
        #payIndex #Payoo {
            background: url('/IMG/thanhtoanantv.png') no-repeat -234px 0;
            opacity: 0.5;
            position: absolute;
            width: 210px;
            height: 420px;
            left: 234px;
        }
        
        #payIndex #SmartLink {
            background: url('/IMG/thanhtoanantv.png') no-repeat -467px 0;
            opacity: 1;
            position: absolute;
            width: 210px;
            height: 420px;
            left: 467px;
        }
        
        #payIndex #BankNet {
            background: url('/IMG/thanhtoanantv.png') no-repeat -699px 0;
            /*opacity: 0.5;*/
            position: absolute;
            width: 210px;
            height: 420px;
            left: 699px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="full" id="avg-main-content">
        <h1 class="payment-title">
            Hệ thống thanh toán Truyền Hình An Viên</h1>
        <div id="payIndex">
            <%--<img src="/IMG/thanhtoanantv.png" alt="thanhtoan" width="900" height="408" usemap="#Map" border="0" />
            <map name="Map" id="Map">
              <area shape="rect" coords="1, 0, 202, 208" href="/AnVienCard/" />
              <area shape="rect" coords="1, 211, 202, 445" href="/MobileCard/" />
              <area shape="rect" coords="233, 0, 435, 408" href="#" />
              <area shape="rect" coords="464, 0, 668, 408" href="#" />
              <area shape="rect" coords="697" href="#" />
            </map>--%>
            <div>
                <div id="cardAV">
                    <a href="/AnVienCard/"></a>
                </div>
                <%--<div id="CardMobile">
                    <a href="/MobileCard/"></a>
                </div>--%>
            </div>
            <div id="Payoo">
                <a href="#"></a>
            </div>
            <div id="SmartLink">
                <a href="/SmartLink/"></a>
            </div>
            <div id="BankNet">
                <a href="/Banknet/"></a>
            </div>
        </div>
    </div>
</asp:Content>
