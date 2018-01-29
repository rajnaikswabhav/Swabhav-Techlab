angular.module('Student')
    .controller('addController',['$scope','addService',function($scope,addService){

        console.log("Inside Add Controller...");
       
        $scope.postData = function(rollNo,name,age,email,date,isMale){
        var dataObj = {
            rollNo : $scope.student.rollNo,
            name : $scope.student.name,
            age : $scope.student.age,
            email : $scope.student.email,
            date : $scope.student.date,
            isMale : $scope.student.isMale    
        };
    
        addService.addStudentData(dataObj)
        .then(function(response){
            alert("Data Added....."+"Status : "+response.status);    
            location.assign("./index.html"); 
        },function(error){
            alert(error);
        });

        $scope.student.rollNo = '';
        $scope.student.name = '';
        $scope.student.age = '';
        $scope.student.email = '';
        $scope.student.date = '';
        $scope.student.isMale = '';
    }
    }]);