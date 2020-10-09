<%@ Page Title="Title" Language="C#" AutoEventWireup="true" CodeBehind="ngtest.aspx.cs" Inherits="Tarin.Default"%>


<html ng-app="ngtest">
<head>
	<title></title>
	<script src="Scripts/angular.js"></script>
</head>
<body>
	<form runat="server" id="f1">
		
	<h1>aaaa {{3+2}}</h1>
	<%-- <h1>aaaa {{3+2}}</h1>

	Write some text in textbox:
	<input type="text" ng-model="sometext" />
	<h1 ng-show="sometext">Hello {{ sometext }}</h1>--%>


	<%--<div ng-controller="ContactController">
		Email:<input type="text" ng-model="newcontact" />
		<button ng-click="add()">Add</button>
		<h2>Contacts</h2>

		<ul>
			<li ng-repeat="contact in contacts">{{ contact }} </li>
		</ul>

	</div>--%>

  
</form>
</body>

</html>
