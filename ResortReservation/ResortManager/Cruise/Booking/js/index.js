<!-- Slider-JavaScript -->
			<script src="js/responsiveslides.min.js"></script>
			<script>
				$(function () {
					$("#slider1, #slider2, #slider3, #slider4").responsiveSlides({
						auto: true,
						nav: true,
						speed: 1500,
						namespace: "callbacks",
						pager: true,
					});
				});
			</script>
		<!-- //Slider-JavaScript -->

		<!-- Slide-To-Top JavaScript (No-Need-To-Change) -->
			<script type="text/javascript">
				$(document).ready(function() {
					var defaults = {
						containerID: 'toTop', // fading element id
						containerHoverID: 'toTopHover', // fading element hover id
						scrollSpeed: 100,
						easingType: 'linear'
					};
					$().UItoTop({ easingType: 'easeOutQuart' });
				});
			</script>
			<a href="#" id="toTop" class="agileits " style="display: block;"> <span id="toTopHover" style="opacity: 0;"> </span></a>
		<!-- //Slide-To-Top JavaScript -->

		<!-- Smooth-Scrolling-JavaScript -->
			<script type="text/javascript" src="js/move-top.js"></script>
			<script type="text/javascript" src="js/easing.js"></script>
			<script type="text/javascript">
					jQuery(document).ready(function($) {
						$(".scroll, .navbar li a, .footer li a").click(function(event){
							$('html,body').animate({scrollTop:$(this.hash).offset().top},1000);
						});
					});
			</script>
		<!-- //Smooth-Scrolling-JavaScript -->

	<!-- //Custom-JavaScript-File-Links -->
	<!-- ############ CHECK AREA CALENDER FORM ############ -->
	<script type="text/javascript">
            $(document).ready(function(){
                  
	             function search(){

	                  var title=$("#search").val();
	                  var date=$("#date").val();
	                  var s="";
	                  if(title!=""){
	                     $.ajax({
	                        type:"post",
	                        url:"./Search.php",
	                        data:"title="+title+"&m=1",
	                        dataType : 'json',
			                processData: false,
	                        success:function(data){
	                        	 s='<table class="table table-striped"><caption>Filter Data</caption><thead><tr><th>Camera</th><th>License</th><th>Date</th><th>Extra</th></tr></thead><tbody>';
	                             $.each(data,function(key,val){ 
	                             	s+='<tr><td>'+val.cameraName+'</td><td>'+val.licensePlate+'</td><td>'+val.dateAdded+'</td><td>'+val.extra+'</td></tr>';
	                             	//alert(val.extra);
			                        });
	                             s+='</tbody></table>';
	                             //alert(s);
	                             $("#all-data").hide();
	                             $('#search-data').html(s);
	                         }


	                      });
	                  }
	                  else
	                  {
	                  	$("#all-data").show();
	                  	$("#search-data").hide();
	                  }
	                   
	             }
 				function searchByDate(){

	                  var date1=$("#date1").val();
	                  var date2=$("#date2").val();
	                  var s="";
	                  if(date1!="" && date2!=""){
	                     $.ajax({
	                        type:"post",
	                        url:"./Search.php",
	                        data:"date1="+date1+"&m=2"+"&date2="+date2,
	                        dataType : 'json',
			                processData: false,
	                        success:function(data){
	                        	 s='<table class="table table-striped"><caption>Filter Data</caption><thead><tr><th>Camera</th><th>License</th><th>Date</th><th>Extra</th></tr></thead><tbody>';
	                             $.each(data,function(key,val){ 
	                             	s+='<tr><td>'+val.cameraName+'</td><td>'+val.licensePlate+'</td><td>'+val.dateAdded+'</td><td>'+val.extra+'</td></tr>';
	                             	//alert(val.extra);
			                        });
	                             s+='</tbody></table>';
	                             //alert(s);
	                             $("#all-data").hide();
	                             $('#search-data').html(s);
	                         }


	                      });
	                  }
	                  else
	                  {
	                  	$("#all-data").show();
	                  	$("#search-data").hide();
	                  }
	                   
	             }
 
                  $('#search').keyup(function(e) {
                  	  search();
                  });
                  $('#date2').change(function(e) {
                  	  searchByDate();
                     
                  });
            });
        </script>