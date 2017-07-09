var angualarModule = angular.module("App", ['ngCookies']);


angualarModule.controller("mainController", function ($scope, $http, $cookies) {

    $scope.NumberProduct = "";

    $http.get("/api/ShoppingCart/GetShoppingCartCount", { params: { "GUID": $cookies.get('GUID') } })
    .success(function (result) {
        //console.log(result);
        $scope.NumberProduct = result;
    }).error(function (result) {
        console.log(result);
    })
    
})


