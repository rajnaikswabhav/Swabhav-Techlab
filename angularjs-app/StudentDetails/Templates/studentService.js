angular.module('Student')
    .constant('API_URL', 'http://gsmktg.azurewebsites.net/api/v1/techlabs/test/students')
    .factory('studentService', ['$http', 'API_URL', function ($http, API_URL) {

        console.log("Inside getData...");
        var studentObj = {};

        studentObj.getStudentData = function () {
            return $http.get(API_URL);
        }

        studentObj.addStudentData = function (dataObj) {
            return $http.post(API_URL, dataObj);
        }

        studentObj.deleteStudentData = function (studentID) {
            return $http.delete(API_URL + '/' + studentID);
        }

        studentObj.editStudentData = function (studentID,dataObj) {
            return $http.put(API_URL+'/'+studentID,dataObj);
        }

        return studentObj;
    }]);
