/*
 *
 * login-register modal
 * Autor: Creative Tim
 * Web-autor: creative.tim
 * Web script: http://creative-tim.com
 * 
 */
function showRegisterForm(){
    $('.loginBox').fadeOut('fast',function(){
        $('.registerBox').fadeIn('fast');
        $('.login-footer').fadeOut('fast',function(){
            $('.register-footer').fadeIn('fast');
        });
        $('.modal-title').html('Şununla Kayıt Ol');
    }); 
    $('.error').removeClass('alert alert-danger').html('');
       
}
function showLoginForm(){
    $('#loginModal .registerBox').fadeOut('fast',function(){
        $('.loginBox').fadeIn('fast');
        $('.register-footer').fadeOut('fast',function(){
            $('.login-footer').fadeIn('fast');    
        });
        
        $('.modal-title').html('Şununla Giriş Yap');
    });       
     $('.error').removeClass('alert alert-danger').html(''); 
}

function openLoginModal(){
    showLoginForm();
    setTimeout(function () {
        $('#loginModal').removeClass('hide');
        $('#loginModal').modal('show');    
    }, 230);
    $("html, body").animate({ scrollTop: 0 }, 'slow', function () {
        $("nav li a").removeClass('active');
    });
}
function openRegisterModal(){
    openLoginModal();
    $('#loginModal').modal('hide');    
    showRegisterForm();
    $("html, body").animate({ scrollTop: 0 }, 'slow', function () {
        $("nav li a").removeClass('active');
    });
}


   