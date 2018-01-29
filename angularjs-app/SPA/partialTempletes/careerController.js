angular.module('swabhav')
    .controller('careerController',['$scope',function($scope){
        console.log("Inside CareerController");
        $scope.careerData = 'This is Career Page....';
    }]);