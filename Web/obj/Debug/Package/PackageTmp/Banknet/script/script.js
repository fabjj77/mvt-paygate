var sText1 = 'Nhập 12 ký tự Mã số thẻ vào đây';
var sText2 = 'Nhập 14 ký tự Mã hợp đồng vào đây';

function RefreshCaptcha() {
    $('#refresh').attr('src', TEMPLATE_URL + '/images/refresh-loading.gif');
    var img = $('#imgCaptcha');
    img.attr('src', '/Ajax/captcha.ashx?v=' + Math.random());
    $('#am_Captcha').val('Nhập Mã kiểm tra');
    setTimeout(function () {
        $('#refresh').attr('src', TEMPLATE_URL + '/images/refresh.gif');
    }, 1000);
}

function RefreshCaptcha2() {
    $('#refresh2').attr('src', TEMPLATE_URL + '/images/refresh-loading.gif');
    var img = $('#imgCaptcha2');
    img.attr('src', '/Ajax/captcha.ashx?v=' + Math.random());
    $('#am_Captcha2').val('Nhập Mã kiểm tra');
    $('#am_Captcha2').focus();
    setTimeout(function () {
        $('#refresh2').attr('src', TEMPLATE_URL + '/images/refresh.gif');
    }, 1000);
}

$(document).ready(function () {
    $("#TB_KeyID").keypress(function (e) {
        return enterInt(e);
    });

    $("#TB_KeyID, #am_Captcha").keypress(function (event) {
        var charCode;
        if (window.event)
            charCode = window.event.keyCode;
        else
            charCode = event.which;
        if (charCode == 13) $('#BT_GetInfo').click();
    });

    $('#CardSerials, #CardPin, #am_Captcha2').keypress(function (event) {
        var charCode;
        if (window.event)
            charCode = window.event.keyCode;
        else
            charCode = event.which;
        if (charCode == 13) $('#BT_submit').click();
    });

    if ($('.banknet .banknet-what').length) {
        $('.banknet .banknet-what a').click(function () {
            $('.pay #banknet-infor').fadeIn();

        });
        $('.pay #banknet-infor a.close').click(function () {
            $('.pay #banknet-infor').hide();
        });

        $('#smartCardInfo .close,#cardInfo .close').click(function () {
            $(this).parent().hide();
        });
    }

    $('#smartCardInfo .close,#cardInfo .close').click(function () {
        $(this).parent().hide();
    });

    $('#TB_KeyID').attr('placeholder', sText1);

    $('#refresh,#imgCaptcha').click(function () {
        RefreshCaptcha();
    });

    $('#refresh2,#imgCaptcha2').click(function () {
        RefreshCaptcha2();
    });

    $('#TB_KeyID').val(sText1);

    $('#BT_GetInfo').click(function () {
        var UserId = $('#TB_KeyID').val();
        var Chaptcha = $('#am_Captcha').val();
        if (UserId.indexOf('Nhập') > 0) UserId = '';
        if (Chaptcha == 'Nhập Mã kiểm tra') Chaptcha = '';
        if (!validate(Chaptcha, UserId)) {
            return;
        }

        $("#BT_GetInfo").attr("disabled", "disabled");
        $('#pay-loading').css('display', 'block');
        $('#Loading-content').text('Đang kiểm tra thông tin khách hàng...');
        $.ajax({
            url: "/Ajax/BanknetCmd.ashx",
            data: { Type: 'GetInfo', UserId: UserId, CaptCha: Chaptcha },
            type: 'GET',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('#pay-loading').css('display', 'none');
                $("#BT_GetInfo").removeAttr("disabled");
                if (data.error != 0) {
                    RefreshCaptcha();
                    alert("lỗi: " + data.msg);
                }
                else {
                    filldata(data);
                    RefreshCaptcha2();
                    $('#Div1').css('display', 'none');
                    $('#Div2').css('display', 'block');
                    $('#Div3').css('display', 'none');
                    $('#Div_Step').removeClass('step-1');
                    $('#Div_Step').addClass('step-2');
                }
            }
        });
    });

    $('#BT_submit').click(function () {
        var vouchers = $('#vouchers').val();

        var Chaptcha = $('#am_Captcha2').val();
        if (Chaptcha == 'Nhập Mã kiểm tra') Chaptcha = '';

        if (!validateCaptcha(Chaptcha)) return false;

        $("#BT_submit").attr("disabled", "disabled");
        $('#pay-loading').css('display', 'block');
        $('#Loading-content').text('Đang thực hiện giao dịch...');
        $.ajax({
            url: "/Ajax/BanknetCmd.ashx",
            data: { Type: 'SetInfo', vouchers: vouchers, CaptCha: Chaptcha },
            type: 'GET',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('#pay-loading').css('display', 'none');
                $("#BT_submit").removeAttr("disabled");
                if (data.error != 0) {
                    RefreshCaptcha2();
                    alert("lỗi: " + data.msg);
                }
                else {
                    if (!window.confirm('Giao dịch sẽ chuyển qua cổng thanh toán trang web thanh toán của Banknet?')) {
                        RefreshCaptcha2();
                    }
                    else {
                        window.location.href = data.msg;
                    }
                }
            }
        });
    });

    var hash = window.location.hash;
    if (hash != '') {
        var arr = hash.split('|');
        if (arr.length >= 2) {
            if (arr[1] == 'T') {
                $('#Div1').css('display', 'none');
                $('#Div2').css('display', 'none');
                $('#Div3').css('display', 'block');
                $('#Div_Step').removeClass('step-1');
                $('#Div_Step').removeClass('step-2');
                $('#Div_Step').addClass('step-3');
            }
            if (arr[1] == 'F') {
                $('#Div1').css('display', 'none');
                $('#Div2').css('display', 'none');
                $('#Div3').css('display', 'block');
                $('#Div_Step').removeClass('step-1');
                $('#Div_Step').removeClass('step-2');
                $('#Div_Step').addClass('step-3');
                var sSorry = '';
                if (arr.length == 3) {
                    if (arr[2] == 'Y')
                        sSorry = 'có lỗi phát sinh, chúng tôi sẽ khắc phục sớm!';
                    if (arr[2] == 'N')
                        sSorry = 'Tài khoản của bạn chưa bị trừ tiền';
                }
                $('#Div3 .pay-finish').html('<div class="pay-background finish-icon unsuccess-icon"></div><div class="success-icon"><h2 class="unsuccess">Thanh toán không thành công</h2><p>' + sErr + '</p><p>' + sSorry +'<p><p align="center"><a href="/Banknet/"><strong>Thực hiện tiếp thanh toán</strong></a></p><p>&nbsp;</p></div>');
            }
        }
    }
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

function filldata(obj) {
    $('#subnum').val(obj.subnum);
    $('#contactname').val(obj.contactname);
    $('#contactaddress').val(obj.contactaddress);
    $('#contactphone').val(obj.contactphone);
    $('#contactemail').val(obj.contactemail);
    $('#servicename').val(obj.servicename);
    $('#substatusname').val(obj.substatusname);
    $('#expirationdate').val(obj.expirationdate);
    
    $('#vouchers option').remove();
    var select = $('#vouchers');
    $.each(obj.vouchers, function (i, val) {
        select.append('<option value="' + val.vouchervalue + '">' + addCommas(val.vouchervalue) + ' VNĐ' + ' - ' + val.duration + ' ' + val.durationuomaltcode +  '</option>');
    });
}

function validate(Chaptcha, UserId) {
    if (UserId == '') {
        alert('Hãy nhập mã thuê bao');
        $('#TB_KeyID').focus();
        return false;
    }

    if (isNaN(UserId)) {
        alert('Số thuê bao phải là số');
        $('#TB_KeyID').focus();
        return false;
    }

    if ($('#check1').is(':checked')) {
        if (UserId.length != 12) {
            alert('Mã số thẻ phải là 12 số');
            $('#TB_KeyID').focus();
            return false;
        }
    }
    else {
        if (UserId.length != 14) {
            alert('Mã hợp đồng phải là 14 số');
            $('#TB_KeyID').focus();
            return false;
        }
    }

    if (!validateCaptcha(Chaptcha)) return false;

    return true;
}

function validate2(Chaptcha, CardSerials, CardPin) {
    if (CardSerials == '') {
        alert('Hãy nhập số Serial');
        $('#CardSerials').focus();
        return false;
    }
    /*
    if (isNaN(CardSerials)) {
    alert('Số serial phải là số');
    $('#CardSerials').focus();
    return false;
    }
            
    if (CardSerials.length != 11) {
    alert('Số serial phải là 11 số');
    $('#CardSerials').focus();
    return false;
    }
    */
    
    if (!CheckNumberCard(CardSerials)) {
        alert('Số serial không hợp lệ');
        $('#CardSerials').focus();
        return false;
    }

    if (CardPin == '') {
        alert('Hãy nhập mã thẻ');
        $('#CardPin').focus();
        return false;
    }

    /*
    if (isNaN(CardPin)) {
    alert('Mã số thẻ phải là số');
    $('#CardPin').focus();
    return false;
    }

    if (CardPin.length != 13) {
    alert('Mã số thẻ phải có 13 số');
    $('#CardPin').focus();
    return false;
    }
    */

    if (!CheckNumberCard(CardPin)) {
        alert('Số thẻ không hợp lệ');
        $('#CardPin').focus();
        return false;
    }
    
    if (!validateCaptcha(Chaptcha)) return false;

    return true;
}

function validateCaptcha(value) {
    if (value == '') {
        alert('Hãy nhập Captcha');
        $('#am_Captcha').focus();
        return false;
    }

    if (value.length != 5) {
        alert('Captcha có 5 ký tự');
        $('#am_Captcha').focus();
        return false;
    }
    return true;
}

function paycardInTypeClick(v) {
    switch (v) {
        case 12:
            $('#TB_KeyID').attr('maxlength', v);
            if ($('#TB_KeyID').val() == '' || $('#TB_KeyID').val().indexOf('Nhập') >= 0) {
                $('#TB_KeyID').val(sText1);
                $('#TB_KeyID').attr('placeholder', sText1);
            }
            break;
        case 14:
            $('#TB_KeyID').attr('maxlength', v);
            if ($('#TB_KeyID').val() == '' || $('#TB_KeyID').val().indexOf('Nhập') >= 0) {
                $('#TB_KeyID').val(sText2);
                $('#TB_KeyID').attr('placeholder', sText2);
            }
            break;
        default: break;
    }
}

function paycardBlurSTB() {
    var inType = $('input[name="inputType"]:checked').val();
    switch (inType) {
        case '12':
            if ($('#TB_KeyID').val() == '') {
                $('#TB_KeyID').val(sText1);
                $('#TB_KeyID').attr('placeholder', sText1);
            }
            break;
        default:
        case '14':
            if ($('#TB_KeyID').val() == '') {
                $('#TB_KeyID').val(sText2);
                $('#TB_KeyID').attr('placeholder', sText2);
            }
            break;
    }
}

function paycardFocusSTB() {
    if ($('#TB_KeyID').val().indexOf('Nhập') >= 0) {
        $('#TB_KeyID').val('');
    }
}

function helpGetSmartCard() {
    $('#smartCardInfo').show();
}

function CheckNumberCard(s) {
    var filter = /^[0-9A-Za-z]+$/;
    if (filter.test(s)) {
        return true;
    }
    return false;
}

function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}