angular.module('swabhav', ['hitech'])
    .controller('customerController', function ($rootScope) {
        console.log("Inside Customer Controller");
        console.log("$rootScope");
        $rootScope.customer = {
            id: 101,
            name: 'Akash'
        };
    });

angular.module('hitech', [])
    .controller('invoiceController', function ($scope, $rootScope) {
        console.log("Inside Invoice Controller");
        console.log($rootScope.customer);
        $scope.invoice = {
            id: 201,
            name: 'book',
            cost: 500
        };
    });