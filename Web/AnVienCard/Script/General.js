function helpGetCard() {
    $('#cardInfo').show();
}

function helpGetSmartCard() {
    $('#smartCardInfo').show();
}

$(document).ready(function () {
    $('#smartCardInfo .close,#cardInfo .close').click(function () {
        $(this).parent().hide();
    });
});

function paycardBlurSTB() {
    var inType = $('input[name="am_InType"]:checked').val();
    switch (inType) {
        case '1':
            if ($('#am_STB').val() == '') {
                $('#am_STB').val('Nhập 12 ký tự Mã số thẻ vào đây');
            }
            break;
        case '2':
            if ($('#am_STB').val() == '') {
                $('#am_STB').val('Nhập 14 ký tự Mã hợp đồng vào đây');
            }
            break;
        default:
    }
}

function paycardFocusSTB() {
    var inType = $('input[name="am_InType"]:checked').val();
    switch (inType) {
        case '1':
            if ($('#am_STB').val() == 'Nhập 12 ký tự Mã số thẻ vào đây') {
                $('#am_STB').attr('placeholder', 'Nhập 12 ký tự Mã số thẻ vào đây');
                $('#am_STB').val('');
            }
            break;
        case '2':
            if ($('#am_STB').val() == 'Nhập 14 ký tự Mã hợp đồng vào đây') {
                $('#am_STB').attr('placeholder', 'Nhập 14 ký tự Mã số thẻ vào đây');
                $('#am_STB').val('');
            }
            break;
        default:
    }
}

function paycardInTypeClick(v) {
    (function ($) {
        switch (v) {
            case '1':
                if ($('#am_STB').val() == '' || $('#am_STB').val() == 'Nhập 14 ký tự Mã hợp đồng vào đây') {
                    $('#am_STB').val('Nhập 12 ký tự Mã số thẻ vào đây');
                    $('#am_STB').attr('placeholder', 'Nhập 12 ký tự Mã số thẻ vào đây');
                    $('#am_STB').attr('maxlength', 12);
                }
                break;
            case '2':
                if ($('#am_STB').val() == '' || $('#am_STB').val() == 'Nhập 12 ký tự Mã số thẻ vào đây') {
                    $('#am_STB').val('Nhập 14 ký tự Mã hợp đồng vào đây');
                    $('#am_STB').attr('placeholder', 'Nhập 14 ký tự Mã số thẻ vào đây');
                    $('#am_STB').attr('maxlength', 14);
                }
                break;
            default: break;
        }
    })($);
}

function RefreshCaptcha() {
    $('#refresh').attr('src', TEMPLATE_URL + '/images/refresh-loading.gif');
    img = $('#imgCaptcha');
    img.attr('src', '/Ajax/captcha.ashx?v=' + Math.random());
    $('#am_Captcha').val('');
    setTimeout(function() {
        $('#refresh').attr('src', TEMPLATE_URL + '/images/refresh.gif');
    }, 1000);
}

$(document).ready(function () {
    $("#paySubmits").click(function () {
        var CardNumber = $('#am_CardCode').val();
        var UserId = $('#am_STB').val();
        var Chaptcha = $('#am_Captcha').val();
        if (CardNumber == 'Nhập Mã thẻ cào') CardNumber = '';
        if (UserId.indexOf('Nhập') > 0) UserId = '';
        if (Chaptcha == 'Nhập Mã kiểm tra') Chaptcha = '';
        if (!validate(Chaptcha, UserId, CardNumber)) {
            return;
        }

        $("#paySubmits").attr("disabled", "disabled");
        $('#pay-loading').css('display', 'block');
        $.ajax({
            url: "/Ajax/PaycardCmd.ashx",
            data: { UserId: UserId, CardId: CardNumber, CaptCha: Chaptcha },
            type: 'GET',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $("#paySubmits").removeAttr("disabled");
                $('#pay-loading').css('display', 'none');
                if (data.error != 0) {
                    //refresh captchar
                    RefreshCaptcha();
                    $('#am_Captcha').val('');
                    alert("lỗi: " + data.msg);
                }
                else {
                    $("#avg-page").html('<div class="pay-left"><div class="pay-finish pay-sucess"><div class="pay-background finish-icon success-icon"></div><div class="success-icon"><h2 class="success">Thanh toán thành công</h2><p>&nbsp;</p><p>Thời hạn hợp đồng của bạn kéo dài đến <b> ' + data.msg +  '</b></p><p align="center"><a href="/AnVienCard/"><strong>Thực hiện tiếp thanh toán</strong></a></p><p>&nbsp;</p></div></div></div>');
                }
            }
        });
    });

    $("#am_CardCode, #am_STB").keypress(function (e) {
        return enterInt(e);
    });

    $("#am_CardCode, #am_STB,#am_Captcha").keypress(function (event) {
        var charCode;
        if (window.event)
            charCode = window.event.keyCode;
        else
            charCode = event.which;
        if (charCode == 13) $('#paySubmits').click();
    });
});

function enterInt(event) {
    var charCode;
    if (window.event)
        charCode = window.event.keyCode;
    else
        charCode = event.which;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
};

function validate(Chaptcha, UserId, CardNumber) {
    if (CardNumber == '') {
        alert('Hãy nhập mã thẻ');
        $('#am_CardCode').focus();
        return false;
    }

    if(isNaN(CardNumber)) {
        alert('Mã thẻ phải là số');
        $('#am_CardCode').focus();
        return false;
    }

    if (CardNumber.length != 12) {
        alert('Mã có 12 số');
        $('#am_CardCode').focus();
        return false;
    }
    
    if (UserId == '') {
        alert('Hãy nhập mã thuê bao');
        $('#am_STB').focus(); 
        return false;
    }

    if (isNaN(UserId)) {
        alert('Số thuê bao phải là số');
        $('#am_STB').focus();
        return false;
    }

    if ($('#am_InType-1').is(':checked')) {
        if (UserId.length != 12) {
            alert('Mã số thẻ phải là 12 số');
            $('#am_STB').focus();
            return false;
        }
    }
    else {
        if (UserId.length != 14) {
            alert('Mã hợp đồng phải là 14 số');
            $('#am_STB').focus();
            return false;
        }
    }
    
    if (Chaptcha == '') {
        alert('Hãy nhập Captcha');
        $('#am_Captcha').focus(); 
        return false;
    }

    if (Chaptcha.length != 5) {
        alert('Captcha có 5 ký tự');
        $('#am_Captcha').focus();
        return false;
    }

    return true;
}