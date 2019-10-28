var productsStoreController = function ($scope, $http) {
    
    // pagination controls
    $scope.Products = [];
    $scope.paginatedProducts = [];
    $scope.currentPage = 1;
    $scope.numPerPage = 7;
    $scope.maxSize = 5; // items per page

    $http({
        method: 'Get',
        url: "http://localhost:3616/Store/Products"
    }).then(function (response) {
        $scope.Products = response.data;
    });

    $scope.$watch('currentPage + numPerPage', updateFilteredItems);
    function updateFilteredItems() {
        var begin = ($scope.currentPage - 1) * $scope.numPerPage;
        var end = begin + $scope.numPerPage;
        $scope.paginatedProducts = $scope.Products.slice(begin, end);
    } 

};

myApp.controller("productsStoreCtrl", productsStoreController);