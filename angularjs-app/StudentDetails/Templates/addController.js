angular.module('Student')
    .controller('addController', ['$scope', 'studentService', function ($scope, studentService) {

        console.log("Inside Add Controller...");

        $scope.postData = function (rollNo, name, age, email, date, isMale) {
            var dataObj = {
                rollNo: rollNo,
                name: name,
                age: age,
                email: email,
                date: date,
                isMale: isMale
            };

            studentService.addStudentData(dataObj)
                .then(function (response) {
                    alert("Data Added....." + "Status : " + status);

                }, function (error) {
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