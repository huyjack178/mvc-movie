var app = angular.module('myApp', []);
app.controller('myCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.getMovie = function (id) {
        $scope.Id = id;
    };

    var data = {
        __RequestVerificationToken: $(':input[name="__RequestVerificationToken"]').val()
    };

    var deleteUrl = "/Movie/Delete/";

    $scope.deleteMovie = function () {
        //location.reload();

        $http.get(deleteUrl + $scope.Id).success(function () {
            $.ajax({
                url: deleteUrl + $scope.Id,
                type: 'POST',
                data: data,
                success: function (data, status, jqxhr) {
                    alert("Delete success")
                },
                error: function (status) {
                    alert(status)
                }
            });
        }).error(function (status) {
            alert(status)
        })
    };
}]);