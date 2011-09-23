var __bind = function (fn, me) { return function () { return fn.apply(me, arguments); }; };
$.fn.createTabs = function () {
    var $div, $ul, first, sections, tabId;
    $div = this;
    $div.addClass('tab-container');
    sections = this.children('section');
    $div.before('<ul class="tab-header"></ul>');
    $ul = $('ul.tab-header').first();
    tabId = 1;
    first = true;
    sections.each(function () {
        var $section, title;
        $section = $(this);
        title = $section.attr('title');
        $ul.append("<li><h2><a id=\"tab-header-" + tabId + "\" href=\"#\">" + title + "</a></h2></li>");
        $section.attr('id', 'tab-' + tabId);
        if (first) {
            first = false;
            $section.addClass('active-tab');
        }
        return tabId++;
    });
    first = true;
    return $ul.find('a').each(function () {
        var $a;
        $a = $(this);
        if (first) {
            first = false;
            $a.addClass('active-tab-header');
        }
        return $a.click(function () {
            var $section, $this;
            $('.active-tab').removeClass('active-tab');
            $('.active-tab-header').removeClass('active-tab-header');
            $this = $(this);
            $this.addClass('active-tab-header');
            $section = $('#' + this.id.replace('tab-header', 'tab'));
            $section.addClass('active-tab');
            return false;
        });
    });
};
$.fn.slideItemsIn = function () {
    var delay;
    delay = 0;
    setTimeout(__bind(function () {
        var children;
        children = this.find('*');
        children.each(function () {
            var child;
            child = $(this);
            child.setTransition({
                property: 'opacity, left',
                'timing-function': 'ease-in',
                duration: '0.3s, 0.3s',
                delay: delay + 'ms'
            });
            child.css('opacity', 1);
            child.css('left', '0%');
            delay += 90;
        });
    }, this), 0);
};