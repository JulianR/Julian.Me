var getDelay = function () {

    // CSS3 animation detection - https://github.com/benbarnett/jQuery-Animate-Enhanced/blob/master/scripts/src/jquery.animate-enhanced.js
    var style = (document.body || document.documentElement).style;

    var supported = typeof style.WebkitTransition !== "undefined"
        || typeof style.MozTransition !== "undefined"
        || typeof style.OTransition !== "undefined";

    return supported ? 200 : 0;

};

// http://blogs.msdn.com/b/giorgio/archive/2009/04/14/how-to-detect-ie8-using-javascript-client-side.aspx
var isLegacyIE = (function () {

    if (navigator.appName == 'Microsoft Internet Explorer') {

        var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");

        if (re.exec(navigator.userAgent) != null) {
            var num = parseFloat(RegExp.$1);
            return num < 9;
        }
    }

    return false;

})();


$.fn.createTabs = function () {
    var $div = this;
    $div.addClass('tab-container');
    var sections = this.children('section');
    $div.before('<ul class="tab-header"></ul>');
    var $ul = $('ul.tab-header').first();
    var tabId = 1;

    var hash = window.location.hash.substring(1);

    var first = !Boolean(hash);

    sections.each(function () {
        var $section = $(this);
        var title = $section.attr('title');
        $ul.append("<li><h2><a id=\"tab-header-" + tabId + "\" href=\"#" + title + "\">" + title + "</a></h2></li>");
        $section.attr('id', 'tab-' + tabId);
        if (first || title == hash) {
            first = false;
            $section.addClass('active-tab');
        }
        return tabId++;
    });

    first = !Boolean(hash);

    $ul.find('a').each(function () {
        var $a = $(this);

        if (first || $a.text() == hash) {
            first = false;
            $a.addClass('active-tab-header');
        }

        $a.click(function () {

            $('.active-tab').removeClass('active-tab');
            $('.active-tab-header').removeClass('active-tab-header');
            var $this = $(this);


            $this.addClass('active-tab-header');
            var $section = $('#' + this.id.replace('tab-header', 'tab'));
            $section.addClass('active-tab');

            window.location.hash = $this.text();

            if ($section.hasClass('slideable')) {
                $section.slideItemsIn();
            }

            return false;
        });
    });

    var activeSection = $('section.active-tab');

    if (activeSection.hasClass('slideable')) {
        activeSection.slideItemsIn();
    }

    return this;
};

var supportsCssTransitions = (function () {

    var style = (document.body || document.documentElement).style;

    var hasTransforms = typeof style.WebkitTransition !== "undefined"
    || typeof style.MozTransition !== "undefined";

    return function () {
        return hasTransforms;
    };

})();

$.fn.slideItemsIn = (function () {

    var hasTransforms = supportsCssTransitions();

    return function () {
        var delay = 0;
        var self = this;
        var id = self.attr('id');

        setTimeout(function () {

            var children = self.find('*');

            children.each(function () {

                if (this.parentNode.tagName != "DIV" && this.parentNode.id != id) {
                    return;
                }

                var child = $(this);

                if (hasTransforms === false) {

                    var animationOptions = {
                        //opacity: 1,
                        left: '0%'
                    };

                    if (!isLegacyIE) {
                        animationOptions.opacity = 1;
                    }

                    setTimeout(function () {
                        child.animate(animationOptions, 300);
                    }, delay);

                    delay += 70;
                } else { // Chrome's animation was choppy with jQuery's CSS animation plugin, works fine with the below way

                    child.setTransition({
                        property: 'opacity, left',
                        'timing-function': 'ease-in',
                        duration: '0.3s',
                        delay: delay + 'ms'
                    });

                    child.css('opacity', 1);
                    child.css('left', '0%');
                    delay += 70;
                }


            });
        }, 0);
    }
})();

