function checkdateinput(tdate) {
    var check = new RegExp("/", "gi");
    if (check.exec(tdate) != null) {
        if (tdate != "") {
            if (tdate.length == 10) {
                var dd = tdate.slice(0, 2);
                var dd = new Number(dd);
                var mm = tdate.slice(3, 5);
                var mm = new Number(mm);
                var yyyy = tdate.slice(6, 10);
                var yyyy = new Number(yyyy);
                var today = new Date();
                var thisy = today.getYear();
                var thism = today.getMonth();
                var thisd = today.getDate();
                var testyyyy = yyyy % 4;
                var check = tdate;
                if (dd != null && mm != null && yyyy != null) {
                    //if (yyyy < thisy) {
                    if (mm == 1 || mm == 3 || mm == 5 || mm == 7 || mm == 8 || mm == 10 || mm == 12) {
                        if (dd >= 1 && dd <= 31) { ; }
                        else { alert("ngày/tháng/năm không hợp lệ..."); return false; }
                    }
                    else if (mm == 2) {
                        if (testyyyy == 0 && dd <= 29 && dd >= 1) { ; }
                        else if (dd >= 1 && dd <= 28 && testyyyy != 0) { ; }
                        else { alert("ngày/tháng/năm không hợp lệ..."); return false; }
                    }
                    else {
                        if (dd >= 1 && dd <= 30 && mm == 4 || mm == 6 || mm == 9 || mm == 11) { ; }
                        else { alert("ngày/tháng/năm không hợp lệ..."); return false; }
                    }
                }
                else { alert("ngày/tháng/năm không hợp lệ..." + dd + " " + mm + " " + yyyy + dd.constructor); return false; }
            }
            else { alert("mời bạn nhập lại ngày/tháng/năm theo mẫu dd/mm/yyyy"); return false; }
        }
        else { alert("mời bạn nhập ngày/tháng/năm..."); return false; }
    }
    else { alert("ngày/tháng/năm không hợp lệ..."); return false; }

    return true;
};