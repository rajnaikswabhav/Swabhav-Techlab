angular.module('swabhav')
    .controller('aboutController',['$scope',function($scope){
        console.log("Inside AboutController");
        $scope.aboutData = 'This is About Page....';
    }]);