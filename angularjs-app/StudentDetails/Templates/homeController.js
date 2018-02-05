angular.module('Student')
    .controller('studentController', ['studentService', '$scope', function (studentService, $scope) {
        studentService.getStudentData()
            .then(function (responses) {
                var data = responses.data
                $scope.results = [];
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
            }, function (error) {
                $scope.result = error;
            });
    }]);