function loadScript() {
    //var n = document.createElement("script");
    //n.type = "text/javascript", n.src = "https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false&callback=initialize", document.body.appendChild(n)
}

function initialize() {
    {
        var n = { zoom: 17, center: new google.maps.LatLng(35.690123, 51.421407) };
        new google.maps.Map(document.getElementById("map_canvas"), n)
    }
}

jQuery(document).ready(function(n) {
    n(".main-menu a").click(function() {
        var e = n(this).attr("class");
        return e = e.split("-"), n("a.active").removeClass("active"), n(this).addClass("active"), n("#menu-container .content").hide(), n("#menu-container #menu-" + e[1]).fadeIn(), n("#menu-container .homepage").hide(), !1
    }), n(".main-menu a.homebutton").click(function() { return n("#menu-container .homepage").fadeIn(), !1 }), n(".main-menu a.aboutbutton").click(function() { return n("#menu-container .about-section").fadeIn(), !1 }), n(".main-menu a.projectbutton").click(function() { return n("#menu-container .project-section").fadeIn(), !1 }), n(".main-menu a.blogbutton").click(function() { return n("#menu-container .blog-section").fadeIn(), !1 }), n(".main-menu a.contactbutton").click(function() { return n("#menu-container .contact-section").fadeIn(), loadScript(), !1 }), n("a.toggle-nav").click(function() { n(".menu-responsive").slideToggle() }), n(".menu-responsive a").click(function() { n(".menu-responsive").slideToggle().hide() })
});