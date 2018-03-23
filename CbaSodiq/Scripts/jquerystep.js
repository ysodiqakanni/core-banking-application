
function scroll_to_class(element_class, removed_height) {
	var scroll_to = $(element_class).offset().top - removed_height;
	if($(window).scrollTop() != scroll_to) {
		$('html, body').stop().animate({scrollTop: scroll_to}, 0);
	}
}

function bar_progress(progress_line_object, direction) {
	var number_of_steps = progress_line_object.data('number-of-steps');
	var now_value = progress_line_object.data('now-value');
	var new_value = 0;
	if(direction == 'right') {
		new_value = now_value + ( 100 / number_of_steps );
	}
	else if(direction == 'left') {
		new_value = now_value - ( 100 / number_of_steps );
	}
	progress_line_object.attr('style', 'width: ' + new_value + '%;').data('now-value', new_value);
}

jQuery(document).ready(function() {

    /*
        Form
    */
    $('.f1 fieldset:first').fadeIn('slow');
    
    $('.f1 input[type="text"], .f1 input[type="password"], .f1 textarea').on('focus', function() {
    	$(this).removeClass('input-error');
    });
    

   
    // next step
    $('.f1 .btn-next').on('click', function () {


        //pick the account number
        //post it to controller method

        debugger;

        //alert("here");
    	var parent_fieldset = $(this).parents('fieldset');
    	var next_step = false;
    	// navigation steps / progress steps
    	var current_active_step = $(this).parents('.f1').find('.f1-step.active');
    	var progress_line = $(this).parents('.f1').find('.f1-progress-line');
    	
    	// fields validation
    	parent_fieldset.find('input[type="text"], input[type="password"], input[id="txtAccountNumber"] textarea').each(function () {
    		if( $(this).val() == "" ) {
    			$(this).addClass('input-error');
    			next_step = false;
    		}
    		else {
    			$(this).removeClass('input-error');
    		}
    	});
        
    	

        //verifying the account number
    	$.ajax({
    	    type: 'POST',
    	    url: "/CustomerAccount/GetAllAccountsInJson?searchstring=" + $("#txtAccountNumber").val(),
    	    dataType: 'json',
    	    data: {},
    	    success: function (jsondata) {
    	        if (jsondata.length < 1) {
    	            next_step = false;
    	            alert("invalid account number");
    	        }
    	        else {
    	            //next_step = true;
    	            parent_fieldset.fadeOut(400, function () {
    	                // change icons
    	                current_active_step.removeClass('active').addClass('activated').next().addClass('active');
    	                // progress bar
    	                bar_progress(progress_line, 'right');
    	                // show next step
    	                $(this).next().fadeIn();
    	                // scroll window to beginning of the form
    	                scroll_to_class($('.f1'), 20);
    	            });
    	        }
    	    }
    	});
        
    	// fields validation
    	
    	//if (next_step) {
    	//    alert("going...");
    	//	parent_fieldset.fadeOut(400, function() {
    	//		// change icons
    	//		current_active_step.removeClass('active').addClass('activated').next().addClass('active');
    	//		// progress bar
    	//		bar_progress(progress_line, 'right');
    	//		// show next step
	    //		$(this).next().fadeIn();
	    //		// scroll window to beginning of the form
    	//		scroll_to_class( $('.f1'), 20 );
	    //	});
    	//}
    	
    });
    
    // previous step
    $('.f1 .btn-previous').on('click', function() {
    	// navigation steps / progress steps
    	var current_active_step = $(this).parents('.f1').find('.f1-step.active');
    	var progress_line = $(this).parents('.f1').find('.f1-progress-line');
    	
    	$(this).parents('fieldset').fadeOut(400, function() {
    		// change icons
    		current_active_step.removeClass('active').prev().removeClass('activated').addClass('active');
    		// progress bar
    		bar_progress(progress_line, 'left');
    		// show previous step
    		$(this).prev().fadeIn();
    		// scroll window to beginning of the form
			scroll_to_class( $('.f1'), 20 );
    	});
    });
    
    // submit
    $('.f1').on('submit', function(e) {
    	
    	// fields validation
    	$(this).find('input[type="text"], input[type="password"], textarea').each(function() {
    		if( $(this).val() == "" ) {
    			e.preventDefault();
    			$(this).addClass('input-error');
    		}
    		else {
    			$(this).removeClass('input-error');
    		}
    	});
    	// fields validation
    	
    });
    
    
});
