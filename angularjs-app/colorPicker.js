angular.module('colorPicker', [])
    .controller('colorPickerController', function ($scope) {

        $scope.colorNames = ['red', 'blue', 'green', 'yellow', 'pink'];

        $scope.changeColor = function (value) {
            console.log("Inside Change Color");
            $scope.color = value;
        }
    });