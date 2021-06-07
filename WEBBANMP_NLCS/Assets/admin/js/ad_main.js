$(function () {
    if ($('#success').val()) {
        displayMessage($('#success').val(), 'success');
    }
    if ($('#info').val()) {
        displayMessage($('#info').val(), 'info');
    }
    if ($('#warning').val()) {
        displayMessage($('#warning').val(), 'warning');
    }
    if ($('#error').val()) {
        displayMessage($('#error').val(), 'error');
    }
});
var displayMessage = function (message, msgType) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "positionClass": "toast-top-right",
        "onClick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "3000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
    toastr[msgType](message);
};


$("#btnAdd").click(function () {

    //Phương thức find là tìm đến thẻ nào đó: ở đây là thẻ tr (:last-child) là thẻ tr cuối cùng trong thẻ tblChiTietPhieuNhap
    var id_cuoi = $(".tblChiTietPhieuNhap").find("tr:last-child").attr("data-id");
    i = parseInt(id_cuoi) + 1;

    //Bước 1: Nội dung phía trong thẻ trAppend
    var tdnoidung = $(".trAppend").html();

    //Bước 2:Tạo 1 thẻ tr bao ngoài nội dung
    var trnoidung = "<tr class=\"trAppended\" data-id=\"" + i + "\">" + tdnoidung + "</tr>";

    //Bước 3: Lấy thẻ table append vào 1 tr
    $(".tblChiTietPhieuNhap").append(trnoidung);
    loadIDLENTHE();

});


//Phương thức xử lý lấy thuộc tính attr từ thẻ tr gán xuống chỉ số phần tử các trong thuộc tính name của thẻ input
function loadIDLENTHE() {
    $(".trAppended").each(function () {
        //Lấy thuộc tính data-id của thẻ tr hiện
        var id = $(this).attr("data-id");
        var nameMaSanPham = "[" + id + "].SP_MA"; //Tạo ra mã sản phẩm
        var nameSoLuongNhap = "[" + id + "].SOLUONGNHAP"; //Tạo ra số lượng nhập
        var nameDonGiaNhap = "[" + id + "].DONGIANHAP";   //Tạo ra đơn giá nhập
        $(this).find(".ddlSanPham").attr("name", nameMaSanPham);//Gán name cho dropdownlist
        $(this).find(".txtDonGia").attr("name", nameDonGiaNhap);//Gán name đơn giá nhập
        $(this).find(".txtSoLuong").attr("name", nameSoLuongNhap);//Gán name số lượng nhập

    });
};


//Xử lý sự kiện xóa
$("body").delegate(".btnDelete", "click", function () {
    //Xóa phần tử cha phía ngoài
    var pos = parseInt($(this).closest("tr").attr("data-id"));
    $(this).closest("tr").remove();
    CapNhapID(pos);

});


function CapNhapID(pos) {
    var trdom = $(".trAppended");
    var trdomsize = trdom.length;
    for (i = pos; i < trdomsize; i++) {
        var id = i;
        $(trdom[i]).attr("data-id", id);
        //Cập nhật lại id khi xóa
        var nameMaSanPham = "[" + id + "].MaSP"; //Tạo ra mã sản phẩm
        var nameSoLuongNhap = "[" + id + "].SoLuongNhap"; //Tạo ra số lượng nhập
        var nameDonGiaNhap = "[" + id + "].DonGiaNhap";   //Tạo ra đơn giá nhập
        $(trdom[i]).find(".ddlSanPham").attr("name", nameMaSanPham);//Gán name cho dropdownlist
        $(trdom[i]).find(".txtDonGia").attr("name", nameDonGiaNhap);//Gán name đơn giá nhập
        $(trdom[i]).find(".txtSoLuong").attr("name", nameSoLuongNhap);//Gán name số lượng nhập
    }
};

//Kiểm tra số lượng
$("body").delegate(".txtSoLuong", "focusout",
        function () {
            var mydom = $(this);
            if (mydom.val().trim() == "" || parseInt(mydom.val()) < 1) {
                mydom.val('1');
            }
            var sl = mydom.val();
            if (sl > Math.floor(sl)) {
                mydom.val(Math.floor(sl));
            }
            return;
        });


//Kiểm tra đơn giá
$("body").delegate(".txtDonGia", "focusout",
    function () {
        var mydom = $(this);
        if (mydom.val().trim() == "" || parseInt(mydom.val()) < 0) {
            mydom.val('0');
        }
        return;
    });


//In đơn hàng

$("#btnInDonHang").click(function () {
    var content = $(".noiDung").html();
    InDonHang(content);
});

//Phương thức in
function InDonHang(content) {
    var printWindow = window.open('', '');
    printWindow.document.write('<html><head><title>Nội dung đơn hàng</title>');
    printWindow.document.write('</head><body>');
    printWindow.document.write(content);
    printWindow.document.write('</body></html>');
    printWindow.document.close();
    printWindow.print();
}



