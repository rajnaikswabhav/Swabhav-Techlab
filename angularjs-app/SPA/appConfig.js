angular.module('swabhav',['ngRoute'])
    .config(['$routeProvider',intializeConfiguration]);

    function intializeConfiguration($routeProvider){
        console.log("Inside Configuration");
        $routeProvider
        .when('/',{
                templateUrl : './partialTempletes/home.html',
                controller : 'homeController'
            })  

        .when('/career',{
                templateUrl : './partialTempletes/career.html',
                controller : 'careerController'
            })

        .when('/about',{
                templateUrl : './partialTempletes/about.html',
                controller : 'aboutController'
            }
        );
    }