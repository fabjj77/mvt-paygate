<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_Header.ascx.cs" Inherits="Web.usercontrols.uc_Header" %>
<%@ Register src="share-box.ascx" tagname="share" tagprefix="uc1" %>
<div class="header">
    <div class="logo">
        <a title="Truyền hình An Viên" href="http://truyenhinhanvien.vn/">
            <img width="214" height="45" alt="Truyền hình An Viên" src="http://static.truyenhinhanvien.vn/anvien/themes/templates/anvien/images/logoanvien.png"></a>
    </div>
    <div class="search-box">
        <div id="searchfrm">
            <input type="text" name="km" id="km" class="searchin" value="Từ khóa tìm kiếm..."
                   onfocus="if(this.value=='Từ khóa tìm kiếm...') this.value='';" onblur="if(this.value=='') this.value='Từ khóa tìm kiếm...';"/>
            <input type="submit" class="subin" value="" id="bt_Search"/>
            <div class="clearfix">
            </div>
        </div>
        <script type="text/javascript">
            $(document).ready(function ($) {
                $('#km').keyup(function (e) {
                    var key = e.keyCode || e.which;
                    if (key === 13) {
                        Search();
                    }
                });
                $('#bt_Search').click(function () {
                    Search();
                });
            });

            function Search() {
                if ($(this).length == 0 || $(this).val() == 'Từ khóa tìm kiếm...') {
                    $(this).focus();
                    return;
                }
                window.location.href = "http://truyenhinhanvien.vn/Result/?k=" + $(this).val();
            }
        </script>
    </div>
    <div class="share-box">
        <uc1:share ID="share1" runat="server" />
    </div>
    <div class="clearfix">
    </div>
</div>
