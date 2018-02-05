angular.module('Student')
    .controller('editController', ['$rootScope', '$scope', 'studentService', function ($rootScope, $scope, studentService) {
        $scope.editData = function (result) {
            console.log("Inside Edit Data....");
            var id = result.rollNo;
            console.log(result.rollNo);
            $rootScope.eData = {
                rollNo: result.rollNo,
                name: result.name,
                age: result.age,
                email: result.email,
                date: result.date,
                gender: result.gender,
            };
        }

        $scope.edit = function (rollNo, name, age, email, date, gender) {
            var dataObj = {
                rollNo: rollNo,
                name: name,
                age: age,
                email: email,
                date: date,
                isMale: gender
            };

            studentService.editStudentData(rollNo, dataObj)
                .then(function (response) {
                    alert("Data Added....." + "Status : " + response.status);
                }, function (error) {
                    alert(error);
                });

            $scope.eData.rollNo = '';
            $scope.eData.name = '';
            $scope.eData.age = '';
            $scope.eData.email = '';
            $scope.eData.date = '';
            $scope.eData.isMale = '';
        }
    }]);