var productsStoreController = function ($scope, $http) {
    init();
    // pagination controls
    $scope.Products = [];
    $scope.paginatedProducts = [];
    $scope.currentPage = 1;
    $scope.numPerPage = 7;
    $scope.maxSize = 5; // items per page

    
    function init() {

        $http({
            method: 'Get',
            url: "http://localhost:3616/Store/Products"
        }).then(function (response) {
            $scope.Products = response.data;
            $scope.selectedProduct = $scope.Products[0];
            $scope.selectedProduct.qty = 1;
        });
    }

    $scope.$watch('currentPage + numPerPage', updateFilteredItems);
    function updateFilteredItems() {
        var begin = ($scope.currentPage - 1) * $scope.numPerPage;
        var end = begin + $scope.numPerPage;
        $scope.paginatedProducts = $scope.Products.slice(begin, end);
    } 

};

myApp.controller("productsStoreCtrl", productsStoreController);