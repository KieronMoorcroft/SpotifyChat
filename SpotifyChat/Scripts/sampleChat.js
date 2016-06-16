(function () {
    var app = angular.module('chat-app', []);

    app.controller('ChatController', function ($scope) {
        // scope variables
        // Holds the name, messages, message and chathub which is null
        $scope.name = 'Guest';
        $scope.message = '';
        $scope.messages = [];
        $scope.chatHub = null;
 
        $scope.chatHub = $.connection.chatHub; // initializes hub
        $.connection.hub.start(); // starts hub

        // register a client method on hub to be invoked by the server
        $scope.chatHub.client.broadcastMessage = function (name, message) {
            var newMessage = name + ' says: ' + message;

            // push the newly coming message to the collection of messages
            $scope.messages.push(newMessage);
            $scope.$apply();
        };

        $scope.newMessage = function () {
            // sends a new message to the server
            $scope.chatHub.server.sendMessage($scope.name, $scope.message);

            $scope.message = '';
        };
    });
}());