<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="angTest.aspx.cs" Inherits="Tarin.angTest" %>

<!DOCTYPE html>

<html ng-app="ngtest">
<head runat="server">
    <title></title>
    <script src="Scripts/angular.js"></script>
    <script src="Scripts/ngtest.js"></script>
    <script src="Scripts/ng-infinite-scroll.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>aaaa {{3+2}}</h1>
        </div>
        <article>
            <section ng-controller="booksCtrl">
                <h2>{{data.id}} - {{data.title}}</h2>
            </section>
        </article>

<%--        <div ng-controller="testCtrl">
            {{data[0].Id}} - {{data[0].Name}}
        </div>


        <div ng-controller="testCtrl">
            <div ng-repeat="d in data">
                <h1>{{d.Id}} - {{d.Name}}</h1>
            </div>
        </div>


        <div ng-controller="DemoController">
            <div infinite-scroll='loadMore()' infinite-scroll-distance='2'>
                <img ng-repeat="image in images" ng-src="http://placehold.it/225x250&text={{image}}">
            </div>
        </div>--%>
        
        <div ng-app='myApp' ng-controller='numbersController'>
            <div infinite-scroll='loadMore()' infinite-scroll-distance='2'>
                <img class="tile" ng-repeat='n in numbers' ng-src='http://placehold.it/100x100&text={{n}}'>
            </div>
        </div>
    </form>
</body>
</html>
