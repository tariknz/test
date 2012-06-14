<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" method="get">
    <h2>AdMonitor Solr Search</h2>
        <% using (Html.BeginForm("Submit", "Search"))
           { %>
        <div id="search">
            <div id="searchbtn">
                Search Solr:
                <input type="text" style="width: 400px;" name="q"  />
                <input type="submit" value="Search" />
                <br />
                <br />
            </div>
        </div>
        <% } %>
    <% AdMonitorSolr.Models.Ads[] solrSearch = (AdMonitorSolr.Models.Ads[])ViewData["DataSet"];  %>
    <div style="width: 100%; height: 0; border-top: 2px dotted #CCC; font-size: 0;">-</div>
    <p>Results found:
        <%= Html.Encode(solrSearch.Count()) %></p>
    <% for (int i = 0; i < solrSearch.Length; i++)
       {  %>
    <p><b><%= Html.Encode(solrSearch[i].Title) %></b></p>
    <p><%= Html.Encode(solrSearch[i].Description)%></p>
    <p>
        Brands:
        <% if (solrSearch[i].Brands != null)
           {
               foreach (string o in solrSearch[i].Brands)
               { %>
                    [<%= Html.Encode(o)%>] 
            <% }
           }%>
    </p>
    <p>
        <% if (solrSearch[i].adPubs != null)
           { %>
               
               <p><b>Featured in <%= solrSearch[i].adPubs.Count %> publications</b></p>
               <% foreach (AdMonitorSolr.Models.Publication p in solrSearch[i].adPubs)
               { %>
                    Publication: <%= Html.Encode(p.publicationName) %><br />
                    Circulation: <%= Html.Encode(p.circulation) %><br />
                    MediaType: <%= Html.Encode(p.mediaType) %><br />
                    Pages: <%= Html.Encode(p.pages) %><br />
                    Status: <%= Html.Encode(p.status) %><br />
                    Frequency: <%= Html.Encode(p.frequency) %><br />
                    <br />Tags: <%= Html.Encode(p.tags) %><br /><br />
        
            <% }
           }%>
    </p>
    <p>
        <i>Last updated: <%= Html.Encode(solrSearch[i].DateModified)%></i></p>
    <div style="width: 100%; height: 0; border-top: 2px dotted #CCC; font-size: 0;">-</div>
    <% } %>
    <p>
        &nbsp;</p>
    </form>
</asp:Content>
