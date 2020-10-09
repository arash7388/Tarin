;
var tooltipContent = "";
var adApp = angular.module('tarin', ['infinite-scroll']);

adApp.factory("AdsDataService", function ($http, $q) {
	var myads = [];

	var OpType = {
		GetAllAds: 1,
		GetAdsByCatagory: 2,
		GetadById: 3
	};

	var read = function (operation, data) {
		var deferred = $q.defer();
		if (operation === OpType.GetAllAds || operation === OpType.GetadById) {
			deferred.resolve($http({
				method: 'GET',
				url: 'api/ad/' + data
			}));
			return deferred.promise;

		}
		else if (operation === OpType.GetAdsByCatagory) {
			deferred.resolve($http({
				method: 'POST',
				url: 'api/ad/',
				data: JSON.stringify(data),
				headers: { 'Content-Type': 'application/json; charset=utf-8', 'dataType': 'json' }
			}));
			return deferred.promise;
		}
		else { console.log("Unidentified method"); }
	};

	return {
		Ads: myads,
		ReadAds: read,
		OperationType: OpType
	};
});

adApp.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/ads', {
		templateUrl: 'ads.html',
		controller: 'MainCtrl'
	});
	$routeProvider.when('/ad-detail/:adId', {
		templateUrl: 'ad-detail.html',
		controller: 'AdCtrl'
	});
	$routeProvider.otherwise({ redirectTo: '/ads' });
}]);

adApp.controller('MainCtrl', function ($scope, AdsDataService, $http) {
	var tooltipJS = new ToolTipJS();
	$scope.ads = [];
	$scope.menus = [];
	$selectedMenu = 0;
	$scope.pageIndex = 0;
	$scope.busy = true;
	$scope.category = {};
	
	$scope.myPagingFunction = function () {
		debugger;
		$scope.busy = true;
		$scope.category.Status = $scope.ads.length;


	    AdsDataService.ReadAds(AdsDataService.OperationType.GetAdsByCatagory, $scope.category).
	        then(function(config, data, headers, status) {
	            debugger;
	            $scope.ads = $scope.ads.concat(arguments[0].data[0].Ads);
	            if (arguments[0].data[0].Ads.length > 0) {
	                $scope.busy = false;
	            }
	        });
	};
	
	AdsDataService.ReadAds(AdsDataService.OperationType.GetAllAds, "").
		then(function (config, data, headers, status) {

			$scope.ads = arguments[0].data[0].Ads;
			$scope.menus = arguments[0].data[0].Cats;
			$scope.pageIndex = $scope.ads.length;
			$scope.busy = false;
			
			$scope.go = function (category) {
				debugger;
				if (category.Id != $selectedMenu && category.Id >= 0) {
					category.Status = 0;
					$scope.GetAdsByCategory(category);
					$selectedMenu = category.Id;

					$scope.category = category;
					$scope.busy = false;
				}
			}
	  });

	//set the tooltip location preference, these can be reordered as required
	tooltipJS.addLocationPreference(new tooltipJS.tooltipLocation(tooltipJS.LocationConstants.Top, "tooltip-Css"));
	tooltipJS.addLocationPreference(new tooltipJS.tooltipLocation(tooltipJS.LocationConstants.Right, "tooltip-Css"));
	tooltipJS.addLocationPreference(new tooltipJS.tooltipLocation(tooltipJS.LocationConstants.Left, "tooltip-Css"));
	tooltipJS.addLocationPreference(new tooltipJS.tooltipLocation(tooltipJS.LocationConstants.Bottom, "tooltip-Css"));

	
	$scope.GetAdsByCategory = function (category) {
		AdsDataService.ReadAds(AdsDataService.OperationType.GetAdsByCatagory, category).
			then(function (config, data, headers, status) {
				
				$scope.ads = arguments[0].data[0].Ads;
				if (arguments[0].data[0].Cats.length > 0) {

					$scope.menus = arguments[0].data[0].Cats;

					$scope.go = function (category) {

						if (category.Id != $selectedMenu && category.Id >= 0) {
							category.Status = 0;
							$scope.GetAdsByCategory(category);
							$selectedMenu = category.Id;

							$scope.category = category;
							$scope.busy = false;
						}
					}
				}

			});
	};

	
	$scope.SetToolTip = function (id, name, author, publisher, price, discount, language, year, isbn13, isbn10) {
		var content = tooltipContent;
		content = content.replace("{{Name}}", name);
		content = content.replace("{{Mobile}}", author);


		tooltipJS.applyTooltip("imgad" + id, content, 5, true);
	};

	//Get our helper methods
	$scope.GetRatingImage = getRatingImage;
	$scope.GetActualPrice = GetActualPrice;
	$scope.HasDiscount = HasDiscount;

});

adApp.controller('AdCtrl', function ($scope, $routeParams, AdsDataService) {
	$scope.adId = $routeParams.adId;
	$scope.ad = {};
	AdsDataService.ReadAds(AdsDataService.OperationType.GetadById, $scope.adId).
		then(function (config, data, headers, status) { $scope.ad = arguments[0].data[0]; });

	//Get our helper methods
	$scope.GetRatingImage = getRatingImage;
	$scope.GetActualPrice = GetActualPrice;
	$scope.HasDiscount = HasDiscount;
});

//Gets rating image based on the rating value passed
function getRatingImage(rating) {
	return rating + "star.png";
}

//Gets the actual price after deducting the discount
function GetActualPrice(price, discount) {
	var discountString = Math.round(discount * 100) + "%";
	var finalPrice = price - (price * discount)
	if (discount > 0) {
		return "Rs. " + Math.round(finalPrice) + "(" + discountString + ")";
	}
	else {
		return "";
	}
};

//Determines if there is any discount for the ad or not
function HasDiscount(discount) {
	return (discount > 0);
};

//Set the tooltip html content
tooltipContent = "<div style='text-align:center'><table>"
tooltipContent += "<span style='font:bold;font-family:Arial;font-weight:800;font-size:large'>{{Name}}</span><br />";
tooltipContent += "<tr><td>Author</td><td>{{AuthorName}}</td></tr>";
tooltipContent += "<tr><td>Publisher</td><td>{{PublisherName}}</td></tr>";
tooltipContent += "<tr><td>Price</td><td>{{Price}}</td></tr>";
tooltipContent += "<tr><td>Discount</td><td>{{Discount}}</td></tr>";
tooltipContent += "<tr><td>Language</td><td>{{Language}}</td></tr>";
tooltipContent += "<tr><td>Publication Year</td><td>{{PublicationYear}}</td></tr>";
tooltipContent += "<tr><td>ISBN-13</td><td>{{ISBN13}}</td></tr>";
tooltipContent += "<tr><td>ISBN-10</td><td>{{ISBN10}}</td></tr></table></div>";


