var statusInput = $("[name='xhpc_message']");

angular.module('thedrunktest', []).directive('overlay', function () {
  return {
    templateUrl: 'overlay.html'
  };
});

var $http = angular.injector(['ng']).get('$http');

chrome.storage.sync.get('id', function (value) {
  console.log(value.id);
  if (value.id) {
    $http.get('http://drunkchecker.azurewebsites.net/ReadForUser', {
      params: {
        id: value.id
      }
    }).then(function (result) {
      console.log(result);
      if (result.data.success && result.data.value > 100) {
        // you're too drunk -> show overlay.
        console.log("you pisshead! reading: " + result.data.value);
        statusInput.css({backgroundColor:'red'});
      } else {
        // you're sober, congrats. tell them.
        console.log("you're sober.. booo! congrats I guess.");
        console.log("reading was: " + result.data.value);
      }
    }, function () {
      console.log('something went wrong whilst requesting a reading...');
      console.log(arguments);
    });
  }
});

