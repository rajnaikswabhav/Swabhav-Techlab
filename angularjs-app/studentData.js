angular.module('student',[])
    .factory('fetchData',['$http',function($http){

        console.log("Inside FetchData...");
        var studentObj={};

        studentObj.getStudentData = function(){
            $http.get('http://gsmktg.azurewebsites.net/api/v1/techlabs/test/students')
                .then(function(response){
                    var result={
                        rollNo : response.rollNo,
                        name : response.name,
                        age : response.age,
                        email : response.email,
                        date : response.date,
                        gender : response.isMale,
                     };

                    return result;
                },function(error){
                    console.log(error);
                    return error;
                });
        }
        return studentObj;
    }])

    .controller('studentController',['fetchData','$scope',function($scope,fetchData){
        $scope.result = fetchData.getStudentData();
    }]);