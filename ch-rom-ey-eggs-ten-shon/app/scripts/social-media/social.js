var $http = angular.injector(['ng']).get('$http');

chrome.storage.sync.get('id', function (value) {
  console.log(value.id);
  var statusInput = ($("[name='xhpc_message']").length && $("[name='xhpc_message']")) || $('#tweet-box-mini-home-profile'),
    offset = statusInput.offset();
  if (value.id && statusInput && statusInput.length) {
    var el = $('<div id="temp" style="font-size: 2em; z-index: 999; position: absolute; background: white; color: red;">Blow into the breathalyser</div>');
    statusInput.css({visibility:'hidden'});
    el.css({
      top: offset.top,
      left: offset.left
    });

    $('body').append(el);

    $http.get('https://drunkchecker.azurewebsites.net/ReadForUser', {
      params: {
        id: value.id,
        notifyIce: true
      }
    }).then(function (result) {
      console.log(result);
      if (result.data.success && result.data.drunkLevel > 1) {
        //you're too drunk -> show youtube vid.
        console.log("you pisshead! reading: " + result.data.value);
        var $uTub = ('<iframe width="470" height="315" src="//www.youtube.com/embed/gvdf5n-zI14" frameborder="0" allowfullscreen></iframe>');
        el.text('');
        el.append($uTub);
      } else {
        // you're sober, congrats. tell them.
        console.log("you're sober.. booo! congrats I guess.");
        console.log("reading was: " + result.data.value);
        el.remove();
        statusInput.css({visibility:'visible'});
      }
    }, function () {
      console.log('something went wrong whilst requesting a reading...');
      console.log(arguments);
      el.remove();
      statusInput.css({visibility:'visible'});
    });
  }
});


