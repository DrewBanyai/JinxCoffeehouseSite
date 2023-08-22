//  Function used to fade each quote in, in order
function fade($element) {
    $element.fadeIn(1000).delay(3000).fadeOut(1000, function() {
        //  Grab the next quote entry, and ensure we circle around if we've reached the end
        var $next = $(this).next('.quote');
        $next = $next.length > 0 ? $next : $(this).parent().children().first();

        //  Fade the next entry in and start the process over
        fade($next);
   });
}
fade($('.quoteLoop > .quote').first());



//  When scrolling, if we've gone past the top portion of the site, stick the navigation bar to the top of the screen
$(window).scroll(function() {

    if ($(window).scrollTop() > 300) { $('.main_nav').addClass('sticky'); }
    else { $('.main_nav').removeClass('sticky'); }
});

// For mobile navigation, click to open and close the navigation menu
$('.mobile-toggle').click(function() {
    if ($('.main_nav').hasClass('open-nav')) {
        $('.main_nav').removeClass('open-nav');
    } else {
        $('.main_nav').addClass('open-nav');
    }
});

$('.main_nav li a').click(function() {
    if ($('.main_nav').hasClass('open-nav')) {
        $('.navigation').removeClass('open-nav');
        $('.main_nav').removeClass('open-nav');
    }
});

// Smooth Scrolling

jQuery(document).ready(function($) {

   $('.smoothscroll').on('click',function (e) {
	    e.preventDefault();

	    var target = this.hash,
	    $target = $(target);

	    $('html, body').stop().animate({
	        'scrollTop': $target.offset().top
	    }, 800, 'swing', function () {
	        window.location.hash = target;
	    });
	});
  
});


TweenMax.staggerFrom(".heading", 0.8, {opacity: 0, y: 20, delay: 0.2}, 0.4);