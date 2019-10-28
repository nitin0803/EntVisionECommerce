var productsStoreController = function ($scope, $http) {
    
    // pagination controls
    $scope.Products = [];
    $scope.Categories = [];
    $scope.PartNumbers = [];
    $scope.paginatedProducts = [];
    $scope.currentPage = 1;
    $scope.numPerPage = 7;
    $scope.maxSize = 5; // items per page
    $scope.selectedPartNumber = "All";
    $scope.selectedCategory = "All";

    $scope.Categories.push($scope.selectedCategory);
    $scope.PartNumbers.push($scope.selectedPartNumber);

    $http({
        method: 'Get',
        url: "http://localhost:3616/Store/GetAllProducts"
    }).then(function (response) {
        $scope.Products = response.data;
        $scope.$watch('currentPage + numPerPage', updateFilteredItems);

        for (var i = 0; i < $scope.Products.length; i++) {
            $scope.Categories.push($scope.Products[i].CategoryName);
            $scope.PartNumbers.push($scope.Products[i].PartNumber);
        }

        $scope.Categories = $scope.Categories.filter(onlyUnique);
    });

    function updateFilteredItems() {
        var begin = ($scope.currentPage - 1) * $scope.numPerPage;
        var end = begin + $scope.numPerPage;
        $scope.paginatedProducts = $scope.Products.slice(begin, end);
    }

    function onlyUnique(value, index, self) {
        return self.indexOf(value) === index;
    }


    $scope.categoryChange = function (selectedCategory) {
        $scope.selectedPartNumber = "All";
        var requestBody = {
            "CategoryName": selectedCategory
        };

        $http.post("http://localhost:3616/Store/ProductsByCategory", JSON.stringify(requestBody))
             .then(function (response) {
                $scope.Products = [];
                $scope.Products = response.data;
                $scope.$watch('currentPage + numPerPage', updateFilteredItems);
             });
    }

    $scope.partNumberChange = function (selectedPartNumber) {
        $scope.selectedCategory = "All";
        var requestBody = {
            "PartNumber": selectedPartNumber
        };

        $http.post("http://localhost:3616/Store/ProductByPartNumber", JSON.stringify(requestBody))
             .then(function (response) {
                 $scope.Products = [];
                 $scope.Products = response.data;
                 $scope.$watch('currentPage + numPerPage', updateFilteredItems);
             });
    }

};

myApp.controller("productsStoreCtrl", productsStoreController);