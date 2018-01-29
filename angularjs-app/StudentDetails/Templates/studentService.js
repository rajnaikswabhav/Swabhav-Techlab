angular.module('Student')
    .constant('API_URL', 'http://gsmktg.azurewebsites.net/api/v1/techlabs/test/students')
    .factory('getService', ['$http', 'API_URL', function ($http, API_URL) {

        console.log("Inside getData...");
        var studentObj = {};

        studentObj.getStudentData = function () {
            return $http.get(API_URL);
        }
        return studentObj;
    }])

    .factory('addService', ['$http', 'API_URL', function ($http, API_URL) {

        console.log("Inside Post Service....");
        var studentObj = {};

        studentObj.addStudentData = function (dataObj) {
            return $http.post(API_URL, dataObj);
        }

        return studentObj;
    }])

    .factory('deleteService', ['$http', 'API_URL', function ($http, API_URL) {
        console.log("Inside Delete Service...");
        var studentObj = {};
        studentObj.deleteStudentData = function (studentID) {
            return $http.delete(API_URL + '/' + studentID);
        }
        return studentObj;
    }])

    .factory('editService', ['$http', 'API_URL', function ($http, API_URL) {
        console.log("Inside Edit Service.....");
        var studentObj = {};
        studentObj.editStudentData = function (student) {
            return $http.put(API_URL + '/' + student);
        }
    }])
