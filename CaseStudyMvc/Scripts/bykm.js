
//$(document).ready(function () {
//    $(window).scroll(function () {
//        if ($(this).scrollTop() > 500) {
//            $('#back-to-top').fadeIn();
//        } else {
//            $('#back-to-top').fadeOut();
//        }
//    });
    
//    $('#back-to-top').click(function () {
//        $('#back-to-top').tooltip('hide');
//        $('body,html').animate({ scrollTop: 0}, 1200);
//        return false;
//    });

//    $('#back-to-top').tooltip('show');
//});

function ajaxReturnJson(url) {
    $.ajax({
        url: url,
        type: "GET",
        dataType: 'json',
        cache: false,
        success: function (data) {

            if (typeof afterJson === 'function') {
                afterJson(data);
            }
            else {
                $.each(data, function (key, val) {
                    if ($('#obj-' + key).length )
                    $('#obj-' + key).html(val);
                });
            }

        },
        error: function (xhr, status, error) {
            alert(xhr.responseText);
        }
    });
}


function AjaxPartialView(url, elementID) {
    $.ajax({
        url: url,
        type: "GET",
        cache: false,
        success: function (data) {
            $(elementID).html(data);
            $(elementID).show();
        },
        error: function (xhr, status, error) {
            alert(xhr.responseText);
        }
    });
}


function myAjax(url, elementID, cssProperty, newValue) {
    var xmlReq = new XMLHttpRequest();

    xmlReq.onreadystatechange = function(){
        if (xmlReq.readyState == 4) {
            if (xmlReq.status == 200) {

                if ( elementID != null ) {
                    if (cssProperty != null) {
                        if (newValue != null) {
                            if (xmlReq.responseText == 'ok') {
                                $(elementID).css(cssProperty, newValue);
                            }
                            else {
                                alert(xmlReq.responseText);
                            }
                        }
                        else {
                            $(elementID).css(cssProperty, xmlReq.responseText);
                        }
                    }
                    else {
                        $(elementID).html(xmlReq.responseText);
                    }
                }

                if (typeof afterAjax === 'function') {
                    afterAjax();
                }
            }
        }
    };

    xmlReq.open("GET", url, true);
    xmlReq.send("");
}


function CookieYaz(name, value) {
    var date = new Date();
    date.setTime(date.getTime() + (1 * 60 * 60 * 1000));
    var expires = "; expires=" + date.toGMTString();
    document.cookie = name + "=" + value + expires + "; path=/";
}

function CookieOku(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function isNumber(n) {
    try {
        n = n.replace(' ', '');
        if(n == ''){ return false; }
        n = n.replace(',', '.');
        return !isNaN( parseFloat(n) ) && isFinite(n);
    } catch(err)
    { return false;}
}

function numberFromSpan(spanID) {
    var spn = document.getElementById(spanID);
    if (spn == null) return 0;
    try {
        var n = spn.innerText;
        n = n.replace(' ', '');
        if (n == '') { return 0; }
        n = n.replace(',', '.');
        return parseFloat(n);
    } catch (err)
    { return 0; }
}

function numberFromTxt(txtID)
{
    var txt = document.getElementById(txtID);
    if (txt == null) return 0;
    try {
    var n = txt.value;
        n = n.replace(' ', '');
        if (n == '') { return 0; }
        n = n.replace(',', '.');
        return parseFloat(n);
    } catch (err)
    { return 0; }
}

function stringFromTxt(txtID) {
    var txt = document.getElementById(txtID);
    if (txt == null) return '';
    try {
    var n = txt.value;
        return n;
    } catch (err)
    { return ''; }
}




