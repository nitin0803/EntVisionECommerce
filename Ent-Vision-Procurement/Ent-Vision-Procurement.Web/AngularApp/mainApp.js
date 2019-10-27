/// <reference path="../scripts/angular.min.js" />


var myApp = angular.module("EntVisionStoreModule", ["ngMaterial", "ngMessages", "ngRoute", "ui.bootstrap", "googlechart"])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when("/products", {
            templateUrl: "/Templates/products.html",
            controller: "productsStoreCtrl"
        });
        $routeProvider.when("/placeorder", {
            templateUrl: "/Templates/placeOrder.html",
            controller: "placeOrderCtrl"
        });
        $routeProvider.when("/vieworder", {
            templateUrl: "/Templates/chartView.html",
            controller: "barChartCtrl"
        });
        $routeProvider.when("/complete", {
            templateUrl: "/Templates/orderSubmitted.cshtml",
            controller: "productsStoreCtrl"
        });
        $routeProvider.otherwise("/products", {
            templateUrl: "/Templates/products.html",
            controller: "productsStoreCtrl"
        });
        $locationProvider.html5Mode(true);
    });
