'use strict';

angular.module('blog').factory('blogService', [
    "$http", "$q",
    function ($http, $q) {

        var topics = [];
        var areTopicsLoaded = false;

        var loadTopics = function () {
            var deferred = $q.defer();

            $http.get('http://localhost:5726/api/v1/topics')
                .then(function (result) {
                    angular.copy(result.data, topics);
                    deferred.resolve();
                    areTopicsLoaded = true;
                }, function () {
                    deferred.reject();
                });

            return deferred.promise;
        };

        var addTopic = function (topic) {

            var deferred = $q.defer();

            $http.post('http://localhost:5726/api/v1/topics', topic)
                .then(function (result) {
                    // success
                    topics.push(result.data);
                    deferred.resolve();
                }, function () {
                    // error
                    deferred.reject();
                });

            return deferred.promise;
        };

        var getTopicById = function (id) {

            var deferred = $q.defer();

            $http.get('http://localhost:5726/api/v1/topics/' + id)
                .then(function (result) {
                    // success
                    deferred.resolve(result.data);
                }, function () {
                    // error
                    deferred.reject();
                });

            return deferred.promise;
        };

        var getRepliesByTopicId = function (id) {

            var deferred = $q.defer();

            $http.get('http://localhost:5726/api/v1/topics/' + id + '/replies')
                .then(function (result) {
                    // success
                    deferred.resolve(result.data);
                }, function () {
                    // error
                    deferred.reject();
                });

            return deferred.promise;
        };

        var addReply = function (topic, reply) {

            var deferred = $q.defer();

            $http.post('http://localhost:5726/api/v1/topics/' + topic.id + '/replies', reply)
                .then(function (result) {
                    // success
                    topic.replies = topic.replies || [];
                    var newReply = result.data;
                    topic.replies.push(newReply);
                    deferred.resolve(newReply);
                }, function () {
                    // error
                    deferred.reject();
                });

            return deferred.promise;
        };

        return {
            loadTopics: loadTopics,
            topics: topics,
            areTopicsLoaded: function () { return areTopicsLoaded; },
            addTopic: addTopic,
            getTopicById: getTopicById,
            addReply: addReply,
            getRepliesByTopicId: getRepliesByTopicId
        };
    }
]);