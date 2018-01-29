angular.module('student', [])
    .constant('API_URL', 'http://gsmktg.azurewebsites.net/api/v1/techlabs/test/students')
    .factory('getData', ['$http', 'API_URL', function ($http, API_URL) {

        console.log("Inside FetchData...");
        var studentObj = {};

        studentObj.getStudentData = function () {
            return $http.get(API_URL);
        }
        return studentObj;
    }])

    .controller('studentController', ['getData', '$scope', function (getData, $scope) {
        getData.getStudentData()
            .then(function (responses) {
                var data = responses.data
                $scope.results  = [];
                for (var i in data) {
                    var result = {
                        rollNo: data[i].rollNo,
                        name: data[i].name,
                        age: data[i].age,
                        email: data[i].email,
                        date: data[i].date,
                        gender: data[i].isMale,
                    };
                    $scope.results.push(result);
                    console.log(result);
                }
                console.log(responses.data);
            }, function (error) {
                $scope.result = error;
            });

    }]);