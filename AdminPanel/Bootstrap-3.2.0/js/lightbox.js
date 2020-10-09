//Problem: User when clicking on image goes to a dead end
//Solution: Create an overlay with the large image - Lightbox

var $overlay = $("#overlayWindowContainer");
var $image = $("<img>");
var $caption = $("<p></p>");

//An image to overlay
$overlay.append($image);

//A caption to overlay
$overlay.append($caption);

//Add overlay
//var $x = $('#overlayWindowContainer');
//$('#overlayWindowContainer').html($image);

//$overlay.append($overlay);

//Capture the click event on a link to an image
$("#t1 a").click(function (event) {
    debugger;
  event.preventDefault();
  //var imageLocation = $(this).attr("href");
  var loc = $(this).children()[0].src;
  //Update overlay with the image linked in the link
  $image.attr("src", loc);
  
  //Show the overlay.
  $overlay.show();
  
  //Get child's alt attribute and set caption
  var captionText = $(this).children("img").attr("alt");
  $caption.text(captionText);
});

//When overlay is clicked
$overlay.click(function(){
  //Hide the overlay
  $overlay.hide();
});










