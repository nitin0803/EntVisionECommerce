var placeOrderController = function ($scope, $http, $mdDialog) {
    $http({
        method: 'Get',
        url: "http://localhost:3616/Store/Products"
    }).then(function (response) {
        $scope.Products = response.data;
        $scope.selectedProduct = $scope.Products[0];
        $scope.selectedProduct.qty = 1;
    });

    $scope.cart = [];
    $scope.addItem = function (selectedProduct) {
        if ((selectedProduct.qty < 1) || (selectedProduct.qty === undefined))
            return showAlert();
        $scope.cart.push(selectedProduct);
        alert($scope.cart.length);
    };

    $scope.SubmitOrder = function () {
        return alert($scope.cart.length);
        var requestBody = {
            "RequiredDate": $scope.requiredDate,
            "ShipName": $scope.customerName,
            "ShipAddress": $scope.address,
            "ShipCity": $scope.city,
            "ShipPostalCode": $scope.postalCode,
            "ShipCountry": $scope.country,
            "CustomerID": "nitin0803",
            "orderDetails": [
                {
                    "PartNumber": "I10",
                    "Quantity": 10
                },
                {
                    "PartNumber": "C1",
                    "Quantity": 40
                },
                {
                    "PartNumber": "Q12",
                    "Quantity": 10
                }
            ]
        };
        
        $http.post("http://localhost:3616/Store/PlaceTransportOrder", JSON.stringify(requestBody))
            .then(function (response) {
                alert("suceess");
            });
    }
};

myApp.controller('placeOrderCtrl', placeOrderController);