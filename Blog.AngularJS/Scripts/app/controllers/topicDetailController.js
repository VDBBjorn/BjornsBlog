angular.module('blog').controller('topicDetailController', [
    "$scope", "$routeParams", "blogService",
    function ($scope, $routeParams, blogService) {
        $scope.isBusy = true;
        $scope.isSendingReply = false;
        $scope.topic = {};
        $scope.newReply = {};

        $scope.addReply = function () {
            $scope.isSendingReply = true;
            blogService.addReply($scope.topic, $scope.newReply)
                .then(function () {
                    // success
                    alertify.success('Reactie toegevoegd!');
                    $scope.newReply = {};
                }, function () {
                    // error
                    alertify.error('Reactie niet toegevoegd');
                })
                .then(function () {
                    // always
                    $scope.isSendingReply = false;
                });
        };

        var t;

        blogService.getTopicById($routeParams.id, { includeReplies: true })
            .then(function(topic) {
                // success
                t = topic;
                alertify.success('Topic met id = ' + topic.id + ' opgehaald!');
                angular.copy(t, $scope.topic);
            }, function() {
                // error
                alertify.error('Er is iets misgelopen bij het ophalen van de topic');
            });

        blogService.getRepliesByTopicId($routeParams.id)
            .then(function (replies) {
            if (t != null) {
                t.replies = replies;
                angular.copy(t, $scope.topic);
                alertify.success('Replies geladen!');
            } else {
                blogService.getTopicById($routeParams.id, { includeReplies: true })
                .then(function (topic) {
                    // success
                    t = topic;
                    alertify.success('Topic met id = ' + topic.id + ' opgehaald!');
                    t.replies = replies;
                    angular.copy(t, $scope.topic);
                    alertify.success('Replies geladen!');
                }, function () {
                    // error
                    alertify.error('Er is iets misgelopen bij het ophalen van de topic');
                });
               
            }

        }, function() {
                alertify.error('Er liep iets mis bij het inladen van de replies');
            }).then(function () {
                $scope.isBusy = false;
            });


    }
]);