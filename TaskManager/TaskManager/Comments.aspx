<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Comments.aspx.cs" Inherits="TaskManager.Admin.Comments" %>

<%@ Register TagPrefix="ctrl" TagName="CommentSection" Src="~/Controls/Comments.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <ctrl:CommentSection runat="server" ID="comS">
    </ctrl:CommentSection>
</asp:Content>