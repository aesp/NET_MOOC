angualarModule.controller("indexController", function ($scope, $http) {

    $scope.addToCart = function (Id) {
        $http.get("/Home/AddtoCart", { params: { "ProductId": Id } })
        .success(function (result) {
            $j('#ShoppingcartNumber').text(result);
        })
    }

})

angualarModule.controller("loginController", function (showhideBars, $scope, $http) {
    showhideBars.hide();
    console.log("Logincontroller");
    $scope.submit = function (user) {
        $http.post("/Home/Login", { user: user })
        .success(function (result) {
        })
        .error(function (result) {
        })
    }

});


angualarModule.controller("dashboardController", function ($scope, $http) {

});

angualarModule.controller("profileController", function ($scope) {

});
