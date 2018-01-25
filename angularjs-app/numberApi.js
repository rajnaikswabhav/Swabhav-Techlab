angular.module('numberApi',[])
    .controller('numberApiController',function($scope,$http){
        console.log("Inside Controller");
        $scope.findNumber = function(){
            
            var number = $scope.numberEntered;
            console.log(number);

            var url = 'http://numbersapi.com/'+number;
            $http.get(url)
                .then(function(response){
                    console.log(response);
                    $scope.response = {
                        data:response.data,
                        status:response.status,
                        statusResult:response.statusText,
                    };
                },function(error){
                    console.log(error);
                } )
        }
    });