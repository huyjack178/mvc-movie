var app = angular.module('myApp', []);
app.controller('myCtrl', function ($scope) {
    $scope.getMovie = function (id) {
        $scope.Id = id;

        $.ajax({
            url: "/Movie/Delete/" + $scope.Id,
            type: "GET",
            complete: function (textStatus) {
                console.log(textStatus);
            },
            error: function (xhr) {
                alert(xhr.status);
                $("#deleteModal").modal('hide');
            }
        });
    };

    $scope.deleteMovie = function () {
        $.ajax({
            url: "/Movie/Delete/" + $scope.Id,
            type: "POST",
            success: function (textStatus) {
                console.log(textStatus);
            },
            complete: function (xhr, textStatus) {
                console.log(textStatus);
            },
            error: function (xhr) {
                console.log(xhr);
            }
        });

        location.reload();
    };
});