angular.module('swabhav', [])
    .factory('MathService', function ($log, $timeout, $rootScope,$q) {

        $rootScope.companyName = 'HiTech';
        $log.log("Inside Service..");

        var obj = {};
        obj.cubeEvenNumber = function (number) {
            if (number % 2 == 0) {
                return number * number * number;
            }
            else {
                throw 'NOT EVEN NUMBER';
            }
        }

        obj.getDataForEvenNumber = function (number) {
                var defer = $q.defer();
                $timeout(function () {
                    console.log("Inside timeout");
                    if (number % 2 === 0) {
                         defer.resolve(number * number * number);
                    }
                    else{
                        defer.reject('NOT EVEN NUMBER');
                    }
                },2000);
                
                return defer.promise;
            }
        
    
        return obj;
    })

    .controller('firstController', function ($scope, MathService, $rootScope) {
        console.log($rootScope.companyName);
        console.log("Inside FirstController");

        $scope.result = {
            value: MathService.cubeEvenNumber(4)
        };

        MathService.getDataForEvenNumber(4)
            .then(function(response){
                $scope.response = response;
            },function(error){
                $scope.response = error;
            });
    })

    .controller('secondController', function ($scope, MathService, $rootScope) {
        console.log($rootScope.companyName);
        console.log("Inside SecondController");
        $scope.result1 = MathService.cubeEvenNumber(10);

        MathService.getDataForEvenNumber(5)
            .then(function(response){
                $scope.response1 = response;
            },function(error){
                $scope.response1 = error;
            });
    });