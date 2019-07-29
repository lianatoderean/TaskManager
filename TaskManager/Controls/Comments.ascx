<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Comments.ascx.cs" Inherits="TaskManager.Controls.Comments" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<link rel="stylesheet" type="text/css" href="/css/styleComment.css" media="screen" />

<asp:PlaceHolder runat="server" ID="CommentFormTop"></asp:PlaceHolder>

<div class="CommentFormContent">

    <asp:Label AssociatedControlID="CommentTitle" runat="server" ID="comTitleLbl" Text="Title: " CssClass="ComLabel"></asp:Label>
    <asp:TextBox runat="server" ID="CommentTitle" CssClass="CommentTitle"></asp:TextBox>

    <asp:Label AssociatedControlID="Comment" runat="server" ID="comContentLbl" Text="Comment: " CssClass="ComLabel"></asp:Label>
    <textarea runat="server" cols="20" rows="2" class="CommentContent" id="comment"></textarea>

    <asp:Button runat="server" OnClick="AddComment_Click" Text="Comment" CssClass="comBtn" />
</div>

<asp:PlaceHolder runat="server" ID="CommentFormBottom"></asp:PlaceHolder>