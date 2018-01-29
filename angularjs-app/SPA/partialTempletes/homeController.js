angular.module('swabhav')
    .controller('homeController',['$scope',homeControllerFunction]);

    function homeControllerFunction($scope){
        console.log("Inside HomeController");
        $scope.data = 'This is home Page.....';
    }