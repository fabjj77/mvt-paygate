<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_menu.ascx.cs" Inherits="Web.usercontrols.uc_menu" %>
<ul id="nav">
    <li class="home navigation-active"><a href="/" class="">Trang chủ</a></li>
    <li class="sep-navi"></li>
    <li><a href="/gioi-thieu/201212/Gioi-thieu-truyen-hinh-an-Vien-8732/">Giới thiệu</a>
        <ul>
            <li><a class="nav-bar-element nav-bar-active" href="/gioi-thieu/201212/Gioi-thieu-truyen-hinh-an-Vien-8732/">
                Giới thiệu Truyền hình An Viên</a></li>
            <li><a class="nav-bar-element" href="/gioi-thieu/201212/Huong-di-cua-Cong-ty-8769/">
                Thông tin tổng quan</a></li>
            <li class="last_chidrl"><a href="/kenh-an-vien/">Các kênh liên kết</a></li>
        </ul>
    </li>
    <li class="sep-navi"></li>
    <li><a href="/tin-tuc/">Tin tức</a>
        <ul>
            <li><a href="/tin-nong/">Tin nóng</a></li>
            <li><a href="/tin-tuc-chung/">Tin tức chung</a></li>
            <li><a href="/bao-chi-voi-avg/">Báo chí viết về AVG</a></li>
            <li class="last_chidrl"><a href="/thong-cao/">Thông cáo báo chí</a></li>
        </ul>
    </li>
    <li class="sep-navi"></li>
    <li><a href="/dich-vu/" class="">Dịch vụ</a>
        <ul style="display: none;">
            <li><a href="/cac-dich-vu-avg/">Các gói kênh của AVG</a></li>
            <li><a href="/tin-avg/201305/Them-kenh-hay-tren-Truyen-hinh-an-Vien-8820/">Gói kênh
                VTVcab</a></li>
            <li class="last_chidrl"><a href="/tin-avg/201306/Giai-phap-xem-duoc-nhieu-tivi-cua-Truyen-hinh-an-Vien-8819/">
                Gói thuê bao phụ</a></li>
        </ul>
    </li>
    <li class="sep-navi"></li>
    <li><a href="/phan-phoi/">Đại lý</a></li>
    <li class="sep-navi"></li>
    <li><a href="/an-vien/" class="">An Viên Clips</a></li>
    <li class="sep-navi"></li>
    <li><a href="/lich-phat-song/">Lịch phát sóng</a></li>
    <li class="sep-navi"></li>
    <li><a href="/ho-tro/">Hỗ trợ</a></li>
    <li class="sep-navi"></li>
    <li><a href="/thu-gian/" class="">Thư giãn</a></li>
    <li class="sep-navi"></li>
    <li><a href="/lien-he/">Liên hệ</a></li>
</ul>
<script type="text/javascript">
    $(document).ready(function () {
        $('#nav li').hover(function () {
            $('ul', this).slideDown(200);
            $(this).children('a:first').addClass("hov");
        }, function () {
            $('ul', this).slideUp(100);
            $(this).children('a:first').removeClass("hov");
        });

        var rootLink = 'http://truyenhinhanvien.vn';
        $('#nav a').is(function () {
            var href = $(this).attr('href');
            $(this).attr('href', rootLink+href);
        });
    });
</script>
