var barChartController = function ($scope, $http) {
    var rows = [];
    $http({
        method: "Get",
        url: "http://localhost:3616/Store/CategoryAndQuantity"
    }).then(function (response) {
        chartData = response.data;
        for (var key in chartData) {
            var category = { v: key }
            var quantity = { v: chartData[key] };
            var col = { c: [category, quantity] };
            rows.push(col);
        }
    });

    $scope.barChartObject = {};
    $scope.barChartObject.type = "ColumnChart";
    $scope.barChartObject.data = {
        "rows": rows,
        "cols": [
            { id: "t", label: "Category", type: "string" },
            { id: "s", label: "Quantity", type: "number" }
        ]
    };

    $scope.barChartObject.options = {
        'title': 'Products Ordered'
    };

    $scope.Title = "Products Ordered by Category";
};

myApp.controller('barChartCtrl', barChartController);