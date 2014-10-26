'use strict';
var store = chrome.storage.sync;

store.get('email', function (value) {
  console.log(value.email);
});

angular.module('app', []).controller('options', ['$scope', '$http', '$q', function($scope, $http, $q) {
  store.get(['name', 'email', 'id'], function (value) {
    $scope.email = value.email || '';
    $scope.name = value.name || '';
    $scope.id = value.id || '';
    $scope.$evalAsync();
  });

  $scope.email = '';
  $scope.name = '';
  $scope.save =  function(e) {
    $http.get('http://drunkchecker.azurewebsites.net/User/GetUser', {
      params: { email: $scope.email }
    }).then(function (result) {
      if (result.data === '{success : false}') {
        // email not registered. go register it
        return $q.reject();
      } else {
        // email registered... update name if different
        $scope.name = result.data.Name;
        $scope.id = result.data.Id;
        $scope.$evalAsync();

        store.set({'id': result.data.Id}, function() {
          console.log("saved Id: " + result.data.Id);
        });
      }
    }, function () {
      console.log(arguments);
    }).then(undefined, function () {
      $http.put('http://drunkchecker.azurewebsites.net/User/CreateUser', {
        params: {
          name: $scope.name,
          email: $scope.email
        }
      }).then(function(result) {
        store.set({'id': result.data.Id}, function() {
          console.log("saved Id: " + result.data.Id);
        });
        $scope.id = result.data.Id;
        $scope.$evalAsync();
        console.log(arguments);
      }, function() {
        console.log(arguments);
      })
    });
  };

  $scope.$watch('email', function(newValue, oldValue) {
    if (newValue !== oldValue) {
      store.set({'email': newValue}, function() {
        console.log("saved email: " + newValue);
      });
    }
  });

  $scope.$watch('name', function(newValue, oldValue) {
    if (newValue !== oldValue) {
      store.set({'name': newValue}, function() {
        console.log("saved name: " + newValue);
      });
    }
  });
}]);



