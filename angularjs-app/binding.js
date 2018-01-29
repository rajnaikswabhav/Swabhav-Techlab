angular.module('binding', [])
    .controller('bindingController', ['$scope',function ($scope) {
        console.log($scope.message);
        $scope.colorNames = ['red', 'blue', 'green', 'yellow', 'pink'];

        $scope.fontColorChange = function (value) {
            console.log("Inside Change Color");
            $scope.color = value;
        }

        $scope.backGroundColorChange = function(value){
            $scope.colorBg = value;
        }
    }]);