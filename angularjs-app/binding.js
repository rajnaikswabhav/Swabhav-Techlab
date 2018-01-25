angular.module('binding', [])
    .controller('bindingController', function ($scope) {

        $scope.write = function(text){
            $scope.message = text;
        }

        $scope.colorNames = ['red', 'blue', 'green', 'yellow', 'pink'];

        $scope.changeColor = function (value) {
            console.log("Inside Change Color");
            $scope.color = value;
        }
    });