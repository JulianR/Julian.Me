$(function () {

    var determineRotations = function (event, $this) {

        var offset = $this.offset();

        var x = event.pageX;
        var y = event.pageY;

        var relativeX = x - offset.left;
        var relativeY = y - offset.top;

        var width = $this.width();
        var height = $this.height();

        var normalizedX = (relativeX / width) - 0.5;
        var normalizedY = (relativeY / height) - 0.5;

        var rotX = Math.floor(-2000 * normalizedY) / 100;
        var rotY = Math.floor(2000 * normalizedX) / 100;

        return { rotX: rotX, rotY: rotY };
    };

    var tiles = $('table#portfolio-projects div.tile');

    tiles.mouseover(function (event) {
        var $this = $(this);
        var rotation = determineRotations(event, $this);
        $this.css('-webkit-transform', 'rotateX(' + rotation.rotX + 'deg) rotateY(' + rotation.rotY + 'deg)');

    });

    tiles.mousedown(function (event) {
        var $this = $(this);
        var rotation = determineRotations(event, $this);
        $this.css('-webkit-transform', 'rotateX(' + rotation.rotX + 'deg) rotateY(' + rotation.rotY + 'deg) translateZ(-20px)');
    });


    tiles.mouseup(function (event) {
        var $this = $(this);
        var rotation = determineRotations(event, $this);
        $this.css('-webkit-transform', 'rotateX(' + rotation.rotX + 'deg) rotateY(' + rotation.rotY + 'deg) translateZ(0px)');

    });

    tiles.mouseleave(function (event) {
        var $this = $(this);
        $this.css('-webkit-transform', 'rotateX(0deg) rotateY(0deg) translateZ(-0px)');
    });

    setTimeout(function () {

        $('div#effect-layer').animate({
            opacity: 1
        }, 1000);

    }, 1000);

    $('a.tile').click(function () {

        var $this = $(this);

        setTimeout(function () {

            window.location = $this.attr('href');

        }, 500);

        return false;

    });

    $('body').css('overflow-x', 'hidden');

    $('a#back-arrow').click(function () {

        slidePortfolio('in');

        return false;

    });

});

var slidePortfolio = function (dir) {

    var left;

    if (dir === 'in') {
        left = '0%';
    } else {
        left = '-100%';
    }

    if (supportsCssTransitions()) {

        setTimeout(function () {

            var body = $('body');

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