angular.module('thedrunktest', []).directive('overlay', function () {
  return {
    templateUrl: 'overlay.html'
  };
});

// mask until the call to the service returns.
$('body').append('<div id="temp" style="width:100%; height:100%; background:white; opacity: 0.7; position: absolute; z-index:9998; top: 0px; left:0px;"></div>');
$('body').append('<div id="temp2" style="font-size: 20em; text-align: center; color: black; position: absolute; z-index:9999; top: 0px; left:0px;">Please Blow</div>');

$.ajax('http://drunkchecker.azurewebsites.net/').done(function (result) {
  // setup the angular app
  $('body').attr('ng-app', 'thedrunktest');

  if (result.success && result.value > 100) {
    // you're too drunk -> show overlay.
    console.log("you pisshead! reading: " + result.value);

    // remove everything on page
    $('body').children().remove();
    $('link').remove();

    var $compile = angular.injector(['ng']).get('$compile'),
      $rootScope = angular.injector(['ng']).get('$rootScope'),
      overlayElement = $compile('<div overlay=""></div>')($rootScope.$new());

    // add our custom splash overlay
    $('body').append(overlayElement);
  } else {
    // you're sober, congrats. tell them.
    console.log("you're sober.. booo! congrats I guess.");
    $('#temp').remove();

  }
}).fail(function () {
  console.log('something went wrong whilst requesting a reading...');
  console.log(arguments);
  $('#temp').remove();
});

