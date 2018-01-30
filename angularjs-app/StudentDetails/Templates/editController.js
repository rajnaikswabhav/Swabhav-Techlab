angular.module('Student')
    .controller('editController',['$scope',function($scope){
        $scope.editData = function(result){
            console.log("Inside Edit Data....");
            console.log(result);
            $scope.result = {
                rollNo : result.rollNo,
                name : result.name,
                age : result.age,
                email : result.name,
                date : result.date,
                isMale : result.isMale
            };

        }
    }]);