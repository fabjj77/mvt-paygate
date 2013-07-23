<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TestServices.PayCard.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Thống kê giao dịch PayCard</title>
     <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <link rel="stylesheet" href="http://jqueryui.com/jquery-wp-content/themes/jqueryui.com/style.css">
    <link href="CSS/Styles.css" rel="stylesheet" type="text/css" />
    <script src="Script/Script.js" type="text/javascript"></script>
    <script>
        $(function () {
            $("#fromDate,#toDate").datepicker({ dateFormat: 'dd/mm/yy' });
        });

        $(document).ready(function () {
            $('#btnFilter').click(function () {
                return CheckSubmit();
            });
        });

        function CheckSubmit() {
            var fromDate = $('#fromDate').val();
            var toDate = $('#toDate').val();
            if (!checkdateinput(fromDate)) {
                $('#fromDate').focus();
                return false;
            }
            if (!checkdateinput(toDate)) {
                $('#toDate').focus();
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <fieldset style="margin-bottom: 20px;">
            <legend>Lọc nâng cao</legend>
            <br />
            <asp:Panel runat="server" ID="pn1" DefaultButton="btnFilter">
                <table cellpadding="2" cellspacing="0">
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
                            Cổng thanh toán:
                        </td>
                        <td>
                            <asp:DropDownList ID="DDL_Type" runat="server">
                            </asp:DropDownList>
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
        <asp:Repeater ID="Contents" runat="server">
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
                            Thời gian
                        </td>
                    </tr>
            </HeaderTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
            <AlternatingItemTemplate>
                <tr class="AlterItem">
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
                        <%#string.Format("{0:HH:mm:ss dd/MM/yyyy}",Eval("CreateDate"))%>
                    </td>
                </tr>
            </AlternatingItemTemplate>
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
                        <%#string.Format("{0:HH:mm:ss dd/MM/yyyy}",Eval("CreateDate"))%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
