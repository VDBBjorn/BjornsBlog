'use strict';

angular.module('blog').controller('newTopicController', [
    '$scope', '$location', 'blogService',
    function ($scope, $location, blogService) {

        $scope.newTopic = {};

        $scope.addTopic = function () {
            alertify.success($scope.newTopic.title + ' ' + $scope.newTopic.body);
            blogService.addTopic($scope.newTopic)
                .then(function () {
                    // success
                    alertify.success('Topic toegevoegd!');

                    // redirect to topics
                    $location.path('/topics');
                }, function () {

                    // error
                    alertify.error('Oei er is iets misgelopen');
                });
        };

    }
]);