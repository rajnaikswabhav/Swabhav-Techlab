angular.module('Student',['ngRoute'])
    .config(['$routeProvider',function($routeProvider){

        console.log("Inside Configuration....");
        $routeProvider
        .when('/addStudent',{
            templateUrl : './Templates/addStudent.html',
            controller : 'addController'
        })
        .when('/editStudent',{
            templateUrl : './Templates/editStudent.html',
            controller : 'editController'
        })
        .when('/index',{
            templateUrl : './index.html',
            controller : 'homeController'
        })
    }]);