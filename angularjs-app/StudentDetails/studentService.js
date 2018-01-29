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
    
    .factory('addService',['$http','API_URL',function($http,API_URL){

        console.log("Inside Post Service....");
        var studentObj = {};

        studentObj.addStudentData = function(dataObj){
            return $http.post(API_URL,dataObj);
        }

        return studentObj;
    }])
