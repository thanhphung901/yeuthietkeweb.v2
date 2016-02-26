// JavaScript Document
 var windowWidth = $(window).width();
 var windowHeight = $(window).height();
 if(windowWidth<=991){
    $(".navx").addClass("w3-sidenav");
  }
// if(windowWidth>991)
//   $(".navx").addClass("");
if(windowWidth<=991){
     
 
  $( ".navx > ul > li" ).has( "ul" ).addClass("parent");
$( ".navx > ul > li.parent" ).prepend("<i class='fa fa-plus'></i>");
$(function () {
 	$('.navx > ul > li>i').on('click', function (e) {
		$(this).parent("li").addClass("active-left");
		$(this).parent("li").children("ul").slideToggle();
		
		$(this).parent("li").addClass("yl");
		if(	$(this).parent("li").children("i").hasClass("fa fa-plus")){
		$(this).parent("li").children("i").removeClass("fa-plus").addClass("fa-minus");
		
		}
		else{
		$(this).parent("li").children("i").removeClass("fa-minus").addClass("fa-plus");
		$(this).parent("li").removeClass("yl");
		}
});
 
});

 }
	
	
	
$(window).resize(function() {
  //update stuff
 // if(windowWidth<=991){
//  	$(".navx").addClass("animated fadeInLeft w3-sidenav");
//  }
//  if(windowWidth>991)
//  	$(".navx").addClass("");
});

////Open the Navigation Pane Hiding All of the Content
//function w3_open() {
//    document.getElementsByClassName("w3-sidenav")[0].style.width = "100%";
//    document.getElementsByClassName("w3-sidenav")[0].style.display = "block";
//}
//function w3_close() {
//    document.getElementsByClassName("w3-sidenav")[0].style.display = "none";
//}

////Shift the Content to the Right
//function w3_open() {
//  document.getElementById("main").style.marginLeft = "25%";
//  document.getElementsByClassName("w3-sidenav")[0].style.width = "25%";
//  document.getElementsByClassName("w3-sidenav")[0].style.display = "block";
//  document.getElementsByClassName("w3-opennav")[0].style.display = 'none';
//}
//function w3_close() {
//  document.getElementById("main").style.marginLeft = "0%";
//  document.getElementsByClassName("w3-sidenav")[0].style.display = "none";
//  document.getElementsByClassName("w3-opennav")[0].style.display = "inline-block";
//}

//var windowWidth = $(window).width(); //retrieve current window width
//var windowHeight = $(window).height(); //retrieve current window height
//var documentWidth = $(document).width(); //retrieve current document width
//var documentHeight = $(document).height(); //retrieve current document height
//var vScrollPosition = $(document).scrollTop(); //retrieve the document scroll ToP position
//var hScrollPosition = $(document).scrollLeft(); //retrieve the document scroll Left position

function w3_open() {
    document.getElementsByClassName("w3-sidenav")[0].style.display = "block";
}
function w3_close() {
     document.getElementsByClassName("w3-sidenav")[0].style.display = "none";
}

//popup search
 var notH = 1,
$pop = $('.popupSearch').hover(function () { notH ^= 1; });

  $(document).on('mouseup keyup', function (e) {
	  if (notH || e.which == 27) $pop.stop().hide();
  });
  $('.popupSearch').hide();
$(document).ready(function () {
  $('.trigger').click(function () {
	  $('.popupSearch').slideToggle();  
  });
  $('.btn-close').click(function () {
	  $('.popupSearch').slideUp('fast');
  });
});


//back top
  (function($){
	$.fn.UItoTop = function(options) {

 		var defaults = {
			text: '',
			min: 500,			
			scrollSpeed: 800,
  			containerID: 'back-top',
  			containerClass: 'fa fa-chevron-up',
			easingType: 'linear'
					
 		};

 		var settings = $.extend(defaults, options);
		var containerIDhash = '#' + settings.containerID;
		var containerHoverIDHash = '#'+settings.containerHoverID;
			
		$('body').append(' <a href="#" id="'+settings.containerID+'" class="'+settings.containerClass+'" >'+settings.text+'</a> ');		
		
		$(containerIDhash).hide().click(function(){			
			$('html, body').stop().animate({scrollTop:0}, settings.scrollSpeed, settings.easingType);
			$('#'+settings.containerHoverID, this).stop().animate({'opacity': 0 }, settings.inDelay, settings.easingType);
			return false;
		})
		
								
		$(window).scroll(function() {
			var sd = $(window).scrollTop();
			if(typeof document.body.style.maxHeight === "undefined") {
				$(containerIDhash).css({
					'position': 'absolute',
					'top': $(window).scrollTop() + $(window).height() - 50
				});
			}
			if ( sd > settings.min ) 
				$(containerIDhash).stop(true,true).fadeIn(600);
			else 
				$(containerIDhash).fadeOut(600);
		});
};
})(jQuery);


$(".w3-third, h2, h3, h4, .w3-half  ul").addClass("wow fadeInUp");
 
//(function($){
//	$.fn.jTruncate = function(options) {
//		var defaults = {
//			length: 300,
//			minTrail: 20,
//			moreText: "more",
//			lessText: "less",
//			ellipsisText: "...",
//			moreAni: "",
//			lessAni: ""
//		};
//		
//		var options = $.extend(defaults, options);
//	   
//		return this.each(function() {
//			obj = $(this);
//			var body = obj.html();
//			
//			if(body.length > options.length + options.minTrail) {
//				var splitLocation = body.indexOf(' ', options.length);
//				if(splitLocation != -1) {
//					// truncate tip
//					var splitLocation = body.indexOf(' ', options.length);
//					var str1 = body.substring(0, splitLocation);
//					var str2 = body.substring(splitLocation, body.length - 1);
//					obj.html(str1 + '<span class="truncate_ellipsis">' + options.ellipsisText + 
//						'</span>' + '<span class="truncate_more">' + str2 + '</span>');
//					obj.find('.truncate_more').css("display", "none");
//					
//					// insert more link
//					obj.append(
//						'<div class="clearboth">' +
//							'<a href="#" class="truncate_more_link">' + options.moreText + '</a>' +
//						'</div>'
//					);
//
//					// set onclick event for more/less link
//					var moreLink = $('.truncate_more_link', obj);
//					var moreContent = $('.truncate_more', obj);
//					var ellipsis = $('.truncate_ellipsis', obj);
//					moreLink.click(function() {
//						if(moreLink.text() == options.moreText) {
//							moreContent.show(options.moreAni);
//							moreLink.text(options.lessText);
//							ellipsis.css("display", "none");
//						} else {
//							moreContent.hide(options.lessAni);
//							moreLink.text(options.moreText);
//							ellipsis.css("display", "inline");
//						}
//						return false;
//				  	});
//				}
//			} // end if
//			
//		});
//	};
//})(jQuery);
//
//
//$().ready(function() {  
//    $('.text-exp').jTruncate({  
//        length: 130,  
//        minTrail: 60,  
//        moreText: "More",  
//        lessText: "[hide extra]",  
//        ellipsisText: "...",  
//        moreAni: "fast",  
//        lessAni: 2000  
//    });  
//});  
//$('.text-exp').text()=$this.parent('.des-exp').children('.text-exp').trim().substring(0, 60) + "…";
//var myTag;
//var truncated = myTag.trim().substring(0, 60) + "…";
//  $('.video-title a').text(truncated);


//var p=$('.text-exp');
//var divh=$('.text-exp').height();
//while ($(p).outerHeight()>divh) {
//    $(p).text(function (index, text) {
//        return text.replace(/\W*\s(\S)*$/, '...');
//    });
//}


