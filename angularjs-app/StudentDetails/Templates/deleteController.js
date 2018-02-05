angular.module('Student')
    .controller('deleteController',['$scope','studentService',function($scope,studentService){
        console.log("Inside deleteController..");
        $scope.deleteData = function(studentID){
            console.log("Inside deleteData....");
            console.log(studentID);
            studentService.deleteStudentData(studentID)
                .then(function(response){
                    alert("Data Deleted..."+response.status);
                },function(error){
                    alert("Error is:"+error);
                });
        }
    }]);