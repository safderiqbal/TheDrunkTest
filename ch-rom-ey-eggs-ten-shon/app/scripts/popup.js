'use strict';
var store = chrome.storage.sync,
  websites;

store.get('websites', function (value) {
  websites = value.websites;
});

angular.module('app', []).controller('popup', ['$scope', function($scope) {
  $scope.name = '';
  $scope.add = function(e) {
    console.log('add');
    $('<body>').onclick(function (e) {

      websites[$scope.name] = e.target;
      store.set(websites, function() {
        console.log("saved name: " + newValue);
      });
    });
  };
}]);



