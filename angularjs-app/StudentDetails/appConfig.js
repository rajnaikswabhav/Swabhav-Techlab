angular.module('Student',['ngRoute'])
    .config(['$routeProvider',function($routeProvider){
        var type = window.location.hash.substr(1);
        console.log("Inside Configuration....");
        console.log(type);
        $routeProvider
        .when('/addStudent',{
            templateUrl : './Templates/addStudent.html',
            controller : 'addController'
        })
        .when('/editStudent',{
            templateUrl : './Templates/editStudent.html',
            controller : 'editController'
        })
    }]);