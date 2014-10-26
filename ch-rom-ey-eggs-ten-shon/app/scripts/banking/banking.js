angular.module('thedrunktest', []).directive('overlay', function () {
  return {
    templateUrl: 'overlay.html'
  };
});

var $http = angular.injector(['ng']).get('$http');

chrome.storage.sync.get('id', function (value) {
  console.log(value.id);
  if (value.id) {
    // mask until the call to the service returns.
    $('body').append('<div id="temp" style="width:100%; height:100%; background:white; opacity: 0.7; position: absolute; z-index:9998; top: 0px; left:0px;"></div>');
    $('body').append('<div id="temp2" style="font-size: 20em; text-align: center; color: black; position: absolute; z-index:9999; top: 0px; left:0px;">Please Blow</div>');
    $http.get('http://drunkchecker.azurewebsites.net/ReadForUser', {
      params: {
        id: value.id
      }
    }).then(function (result) {
      console.log(result);
      // setup the angular app
      $('body').attr('ng-app', 'thedrunktest');

      if (result.data.success /*&& result.data.drunkLevel > 0*/) {
      // you're too drunk -> show overlay.
        console.log("you pisshead! reading: " + result.data.value);

        // remove everything on page
        $('body').children().remove();
        $('link').remove();

        var $compile = angular.injector(['ng']).get('$compile'),
          $rootScope = angular.injector(['ng']).get('$rootScope'),
          overlayElement = $compile('<div overlay=""></div>')($rootScope.$new());

        //add our custom splash overlay
        $('body').append(overlayElement);
      } else {
        // you're sober, congrats. tell them.
        console.log("you're sober.. booo! congrats I guess.");
        console.log("reading was: " + result.data.value);
        $('#temp').remove();
        $('#temp2').remove();
      }
    }, function () {
      console.log('something went wrong whilst requesting a reading...');
      console.log(arguments);
      $('#temp').remove();
      $('#temp2').remove();
    });
  }
});

