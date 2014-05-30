'use strict';

angular.module('blog').controller('blogController', [
    "$scope", "$location", "blogService",
    function ($scope, $location, blogService) {
        $scope.isBusy = true;
        $scope.data = blogService;
        $scope.go = function (route) {
            $location.path(route);
        };

        if (blogService.areTopicsLoaded()) {
            $scope.isBusy = false;
        }
        else {
            blogService.loadTopics()
                .then(function () {
                    // success
                    alertify.success("Topics ingeladen!", 1500);
                    

                }, function () {
                    // error
                    alertify.error("Topics konden niet opgehaald worden!", 1500);
                })
                .then(function () {
                    // always
                    $scope.isBusy = false;
                });
        }
    }
]);