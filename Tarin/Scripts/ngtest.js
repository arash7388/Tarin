var app = angular.module('ngtest', ['infinite-scroll']);

app.controller('booksCtrl', function ($scope, $http) {
    debugger;
    $http.get("http://jsonplaceholder.typicode.com/posts/1")
      .success(function (data) {
          debugger;
          $scope.data = data;
      });
});

app.controller('testCtrl', function ($scope, $http) {
    debugger;
    $http.get("/api/test")
      .success(function (data) {
          debugger;
          $scope.data = data;
      });
});

app.controller('DemoController', function ($scope) {
    $scope.images = [1, 2, 3, 4, 5, 6, 7, 8];

    $scope.loadMore = function () {
        var last = $scope.images[$scope.images.length - 1];
        for (var i = 1; i <= 8; i++) {
            $scope.images.push(last + i);
        }
    };
});


var numbersController = function ($scope) {
    $scope.numbers = [];
    $scope.counter = 0;

    $scope.loadMore = function () {
        for (var i = 0; i < 25; i++) {
            $scope.numbers.push(++$scope.counter);
        };
    }
    $scope.loadMore();
}
app.controller('numbersController', numbersController);