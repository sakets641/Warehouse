<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication5.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 153px">

            Enter mutualf id</div>
        <div>

            <asp:TextBox ID="foliono" runat="server" OnTextChanged="foliono_TextChanged"></asp:TextBox>

        </div>
        <div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged1">
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:APICoreDBConnectionString3 %>" SelectCommand="SELECT * FROM [Foliodetail]"></asp:SqlDataSource>
        <asp:TextBox ID="TextBox1" runat="server" Height="496px" TextMode="MultiLine" Width="1071px"></asp:TextBox>
    </form>
</body>
</html>
