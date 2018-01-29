angular.module('blueBox', [])
    .controller('gameController', ['$scope', function ($scope) {
        $scope.size = [];
        var len = 100;
        var attempts = 0;
        var ranNum = Math.floor(Math.random() * 100);
        console.log("random number is:" + ranNum);
        for (var i = 1; i <= 100; i++) {
            $scope.size.push(i);
        }

        $scope.changeStatus = function (id) {

            if (id > ranNum) {
                console.log("Less than");
                attempts++;
                $scope.color = "red";
                console.log("Attempt: " + attempts);
            }
            else if (id < ranNum) {
                console.log("Greater than");
                attempts++;
                $scope.color = "green";
                console.log("Attempt: " + attempts);
            }
            else if (id == ranNum) {
                $scope.color = "blue";
                alert("Match");
                console.log("Match");
                location.reload();
            }
            if (attempts === 3) {
                alert("You are out of attempts...");
                location.reload();
            }
        }
    }]);