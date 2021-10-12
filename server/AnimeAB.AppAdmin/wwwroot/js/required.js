
var rules = {
    displayname: {
        required: true,
        maxlength: 30
    },
    email: {
        required: true,
        email: true,
        maxlength: 50
    },
    password: {
        required: true,
        minlength: 3,
        maxlength: 30
    },
    password_confirm: {
        equalTo: "#password"
    },
    season: {
        required: true,
        maxlength: 500
    },
    date: {
        required: true
    },
    key: {
        required: true,
        maxlength: 100
    },
    series: {
        required: true,
        maxlength: 20
    },
    session: {
        required: true,
        maxlength: 50
    },
    year: {
        required: true,
        number: true,
        maxlength: 4,
        minlength: 4,
        range: [2000, 2050]
    },
    ordinal: {
        required: true,
    },
    title: {
        required: true,
        maxlength: 150
    },
    titlevie: {
        required: true,
        maxlength: 1050
    },
    description: {
        required: true,
        maxlength: 2500
    },
    link: {
        required: true,
        maxlength: 5550
    },
    url: {
        url: true,
        maxlength: 250
    },
    urlAnime: {
        url: true,
        required: true
    },
    episode: {
        required: true,
        number: true,
        range: [0, 1000]
    },
    duration: {
        required: true,
        number: true,
        range: [20, 240]
    },
    trainer: {
        required: true,
        maxlength: 2050
    }

};

var messages = {
    email: {
        required: "Bạn cần nhập email",
        email: "Email không hợp lệ",
        maxlength: "Email không quá 50 ký tự"
    },
    password: {
        required: "Bạn cần nhập mật khẩu",
        minlength: "Mật khẩu tối thiểu 3 ký tự",
        maxlength: "Mật khẩu tối đa 30 ký tự"
    },
    displayname: {
        required: "Bạn cần nhập tên của bạn",
        maxlength: "Tên giới hạn 30 ký tự"
    },
    password_confirm: {
        equalTo: "Bạn cần nhập lại mật khẩu đã nhập"
    },
    key: {
        required: "Bạn cần nhập key",
        maxlength: "Key không quá 100 ký tự"
    },
    series: {
        required: "Bạn cần nhập series anime",
        maxlength: "Series nhập không quá 20 ký tự"
    },
    session: {
        required: "Bạn cần nhập session của anime",
        maxlength: "Session nhập không quá 50 ký tự"
    },
    season: {
        required: "Bạn cần nhập season của anime",
        maxlength: "Session nhập không quá 500 ký tự"
    },
    date: {
        required: "Bạn cần nhập năm phát hành của anime"
    },
    year: {
        required: "Bạn cần nhập năm trình chiếu",
        number: "Năm trình chiếu phải là số",
        minlength: "Năm tối thiểu 4 ký tự",
        maxlength: "Năm tối thiểu 4 ký tự",
        range: "Năm nhập từ 2000 đến 2050"
    },
    ordinal: {
        required: "Bạn cần nhập số thứ tự anime"
    },
    title: {
        required: "Bạn cần nhập tiêu đề",
        maxlength: "Tiêu đề không quá 150 ký tự"
    },
    titlevie: {
        required: "Bạn cần nhập tiêu đề bổ sung",
        maxlength: "Tiếu đề bổ sung không quá 1050 ký tự"
    },
    description: {
        required: "Bạn cần nhập mô tả",
        maxlength: "Mô tả không quá 2500 ký tự"
    },
    episode: {
        required: "Bạn cần nhập số tập.",
        number: "Số tập là kiểu số",
        range: "Số tập từ 0 đến 1000"
    },
    duration: {
        required: "Bạn cần nhập thời lượng anime.",
        number: "Thời lượng anime là kiểu số",
        range: "Thời lượng anime từ 20 phút đến 240 phút"
    },
    trainer: {
        required: "Bạn cần nhập link trainer",
        maxlength: "Trainer không quá 2050 ký tự"
    },
    link: {
        required: "Bạn cần nhập link",
        maxlength: "Link không quá 5550 ký tự"
    },
    url: {
        url: "Không phải URL",
        maxlength: "Url không quá 250 ký tự"
    },
    urlAnime: {
        url: "Không phải URL",
        required: "Nhập url để get anime"
    }
};

var options = {
    errorElement: 'span',
    errorClass: 'help-block text-danger',
    validClass: "success",
    highlight: function (element) {
        $(element).addClass('is-invalid');
    },
    unhighlight: function (element) {
        $(element).removeClass('is-invalid');
    },
    errorPlacement: function (error, element) {
        if (element.parent('.input-group').length) {
            error.insertAfter(element.parent());
        } else {
            error.insertAfter(element);
        }
    },
};

var optionsDatabase = {
    paging: true,
    searching: true,
    pageLength: 10,
    processing: true,
    serverSide: false,
    "bSort": false,
    "responsive": true
}