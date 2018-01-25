angular.module('numberApi2', [])
    .factory('numberApiService',['$timeout','$http','$q',function ($timeout, $http,$q) {
        var object = {};

        object.getNumberData = function (number) {
            var url = 'http://numbersapi.com/' + number;
            var defer = $q.defer();
            $timeout(function (){
                $http.get(url)
                    .then(function (response) {
                        defer.resolve(response.data);
                    }, function (error) {
                        defer.reject(error);
                    })
            }, 1000);
            return defer.promise;
        }
        return object;
    }])

    .controller('numberApiController',['$scope','numberApiService',function($scope,numberApiService){
        $scope.findNumber = function(){
        var number = $scope.numberEntered;
        if(isNaN(number) ){
            $scope.response= 'NOT A NUMBER';
        }
        else{
        $scope.response = 'Loading...';
        numberApiService.getNumberData(number)
            .then(function(response){
                $scope.response = response;
            },function(error){
                $scope.response = error;
            });
        }
        }
    }]);