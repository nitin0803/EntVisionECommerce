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
    $scope.TotalPrice = 0;
    $scope.addItem = function (selectedProduct) {
        if ((selectedProduct.qty < 1) || (selectedProduct.qty === undefined))
            return showAlert();
        $scope.cart.push(selectedProduct);
        $scope.TotalPrice += selectedProduct.qty * selectedProduct.UnitPrice;
    };

    $scope.SubmitOrder = function () {
        var orderDetails = [];
        for (var i = 0; i < $scope.cart.length; i++) {
            var partAndQuantity = {
                "PartNumber": $scope.cart[i].PartNumber,
                "Quantity": $scope.cart[i].qty
            }
            orderDetails.push(partAndQuantity);
        };

        var requestBody = {
            "RequiredDate": $scope.requiredDate,
            "ShipName": $scope.customerName,
            "ShipAddress": $scope.address,
            "ShipCity": $scope.city,
            "ShipPostalCode": $scope.postalCode,
            "ShipCountry": $scope.country,
            "CustomerID": "nitin0803",
            "orderDetails": orderDetails
        };
        
        $http.post("http://localhost:3616/Store/PlaceTransportOrder", JSON.stringify(requestBody))
            .then(function (response) {
                alert("suceess");
            });
    }
};

myApp.controller('placeOrderCtrl', placeOrderController);