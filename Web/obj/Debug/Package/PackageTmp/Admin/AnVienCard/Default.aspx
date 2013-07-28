<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web.Admin.AnVienCard.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Thống kê giao dịch PayCard</title>
    <script src="/Admin/JS/Script.js" type="text/javascript"></script>
    <script type="text/javascript">
        var sRoot = 'ContentPlaceHolder1_';
        $(function () {
            $('#' + sRoot + 'fromDate,#' + sRoot + 'toDate').datepicker({ dateFormat: 'dd/mm/yy' });
        });

        $(document).ready(function () {
            $('#' +sRoot +'btnFilter').click(function () {
                return CheckSubmit();
            });
        });

        function CheckSubmit() {
            var fromDate = $('#' + sRoot + 'fromDate').val();
            var toDate = $('#' + sRoot + 'toDate').val();
            if (!checkdateinput(fromDate)) {
                $('#' + sRoot + 'fromDate').focus();
                return false;
            }
            if (!checkdateinput(toDate)) {
                $('#' + sRoot + 'toDate').focus();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
    Thống kê giao dịch thẻ cào An Viên
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset style="margin-bottom: 20px;">
            <legend>Lọc nâng cao</legend>
            <br />
            <asp:Panel runat="server" ID="pn1" DefaultButton="btnFilter">
                <table cellpadding="2" cellspacing="4">
                    <tr>
                        <td>
                            Từ ngày
                            <asp:TextBox ID="fromDate" runat="server" ShowTime="false" Width="70" />
                        </td>
                        <td>
                            Đến ngày
                            <asp:TextBox ID="toDate" runat="server" ShowTime="false" Width="70" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Lọc kết quả:
                        </td>
                        <td>
                            <asp:DropDownList ID="DDL_Status" runat="server">
                                <asp:ListItem Value="0">Tất cả</asp:ListItem>
                                <asp:ListItem Value="1">Giao dịch thành công</asp:ListItem>
                                <asp:ListItem Value="-1">Giao dịch thất bại</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                            <asp:Button ID="btnFilter" runat="server" Text="Lọc" OnClick="btnFilter_Click" Width="100" />
                            &nbsp;<asp:Button ID="btnExportAll" runat="server" Text="Export" 
                                OnClick="btnExportAll_Click" Width="100px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
        <br />
        <asp:Literal ID="LtTotal" runat="server"></asp:Literal>
        <br />
        <br class="clear" />
        <asp:Repeater ID="Contents" runat="server" ViewStateMode="Enabled">
            <HeaderTemplate>
                <table class="Contents" cellpadding="0" cellspacing="0" width="100%">
                    <tr class="Header">
                        <td>
                            ID
                        </td>
                        <td>
                            Mã hợp đồng
                        </td>
                        <td>
                            Mã thẻ
                        </td>
                        <td>
                            Mã giao dịch
                        </td>
                        <td>
                            Thông báo
                        </td>
                        <td>
                            Ngày gia hạn
                        </td>
                        <td>
                            Thời gian
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="Item">
                    <td>
                        <%#Eval("ID")%>
                    </td>
                    <td>
                        <%#Eval("UserId")%>
                    </td>
                    <td>
                        <%#Eval("CardId")%>
                    </td>
                    <td>
                        <%#Eval("ResulId")%>
                    </td>
                    <td>
                        <%#Eval("Msg")%>
                    </td>
                    <td>
                        <%#Eval("ResulFull")%>
                    </td>
                    <td>
                        <%#string.Format("{0:HH:mm:ss dd/MM/yyyy}",Eval("CreateDate"))%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
</asp:Content>
