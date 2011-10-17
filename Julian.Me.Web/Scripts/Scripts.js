var getDelay = function () {

    // CSS3 animation detection - https://github.com/benbarnett/jQuery-Animate-Enhanced/blob/master/scripts/src/jquery.animate-enhanced.js
    var style = (document.body || document.documentElement).style;

    var supported = typeof style.WebkitTransition !== "undefined"
        || typeof style.MozTransition !== "undefined"
        || typeof style.OTransition !== "undefined";

    return supported ? 200 : 0;

};

$.fn.createTabs = function () {
    var $div = this;
    $div.addClass('tab-container');
    var sections = this.children('section');
    $div.before('<ul class="tab-header"></ul>');
    var $ul = $('ul.tab-header').first();
    var tabId = 1;
    var first = true;
    sections.each(function () {
        var $section = $(this);
        var title = $section.attr('title');
        $ul.append("<li><h2><a id=\"tab-header-" + tabId + "\" href=\"#\">" + title + "</a></h2></li>");
        $section.attr('id', 'tab-' + tabId);
        if (first) {
            first = false;
            $section.addClass('active-tab');
        }
        return tabId++;
    });
    first = true;

    $ul.find('a').each(function () {
        var $a = $(this);

        if (first) {
            first = false;
            $a.addClass('active-tab-header');
        }

        return $a.click(function () {
            $('.active-tab').removeClass('active-tab');
            $('.active-tab-header').removeClass('active-tab-header');
            var $this = $(this);
            $this.addClass('active-tab-header');
            var $section = $('#' + this.id.replace('tab-header', 'tab'));
            $section.addClass('active-tab');

            $section.slideItemsIn();

            return false;
        });
    });

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

        setTimeout(function () {

            var children = self.find('*');

            children.each(function () {

                var child = $(this);

                if (hasTransforms === false) {

                    setTimeout(function () {
                        child.animate({
                            opacity: 1,
                            left: '0%'
                        }, 300);
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


var slidePortfolio = function (dir) {

    var left;

    if (dir === 'in') {
        left = '0%';
    } else {
        left = '-100%';
    }

    if (supportsCssTransitions()) {
        var body = $('body');

        setTimeout(function () {
            body.css('position', 'relative');

            body.setTransition({
                property: 'left',
                'timing-function': 'ease-in',
                duration: '0.3s'
            });

            body.css('left', left);
        }, 0);

    } else {
        setTimeout(function () {
            var body = $('body');
            body.css('position', 'relative');
            $('body').animate({
                left: left
            });
        }, 0);
    }
};