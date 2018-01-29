angular.module('Student')
    .controller('deleteController',['$scope','deleteService',function($scope,deleteService){
        console.log("Inside deleteController..");
        $scope.deleteData = function(studentID){
            console.log("Inside deleteData....");
            console.log(studentID);
            deleteService.deleteStudentData(studentID)
                .then(function(response){
                    alert("Data Deleted..."+response.status);
                    location.assign("./index.html"); 
                },function(error){
                    alert("Error is:"+error);
                });
        }
    }]);