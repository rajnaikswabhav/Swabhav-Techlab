angular.module('dataBind',[])
    .controller('dataBindController',function($scope , $timeout){
        $scope.developer={

            name: 'Akash',
            company: 'Hitech'
        }
        
        var changData = function(){

            $scope.developer={
                name: 'Brijesh',
                company: 'Swabhav'
            }
        }
        $timeout(function(){
            changData();
        } ,2000 );

        $scope.customer ={
            name: 'Sachin',
            age: 25
        };
    });