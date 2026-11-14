$(document).ready(function () {
	 $(".chosen-select").chosen({no_results_text: "موردی یافت نشد!!"}); 
	 
var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));

elems.forEach(function(html) {
  var switchery = new Switchery(html);
});

//$.fn.peity.defaults.pie = {
//  delimiter: null,
//  height: null,
//  radius: 8,
//  width: null
//}

//$("span.pie").peity("pie", {
//        fill: ['#1ab394', '#d7d7d7', '#ffffff']
//    })

});