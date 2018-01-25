angular.module('imageLoader',[])
    .controller('imageLoaderController',['$scope',function($scope){
        $scope.loadResume = function(){
            console.log("Loadind Data");
            $scope.resume = 
            {
                id:101,
                name:'Akash',
                photo: './Desert.jpg',
                age: 21
            }
        };

        $scope.loadResumes = function(){

            $scope.resumes = [
                
                {
                    id:101,
                    name:'Akash',
                    photo: './Desert.jpg',
                    age: 21
                },

                {
                        id:102,
                        name: 'Brijesh',
                        photo: './rose-blue-flower.jpeg',
                        age: 21      
                },

                {
                    id:103,
                    name: 'Parth',
                    photo: './Jellyfish.jpg',
                    age: 21
                }
                
            ];
        };
    }]);