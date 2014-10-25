

angular.module('thedrunktest').run(["$templateCache", function($templateCache) {
  $templateCache.put("overlay.html",
    "<!-----start-wrap--------->"
  + "<div class='wrap'>"
  + "<!-----start-content--------->"
  + "<div class='content'>"
  + "<!-----start-logo--------->"
  + "<div class='logo'>"
  + "<h1><span>The Drunk Test</span></h1>"
  + "<span><span id='signal'></span>Oops! Looks like you're DRUNK</span>"
  + "</div>"
  + "<div class='buttom'>"
  + "<div class='sign'>"
  + "<p>I can't let you into your online banking.<br/>"
  + "You should go to <span>bed</span>.</p>"
  + "</div>"
  + "</div>"
  + "</div>"
  + "</div>"
  + "<!---------end-wrap---------->"
  );
}]);