
import * as Yup from 'yup';

const messageValidator = {
    email: Yup.string()
        .email("Email không hợp lệ.")
        .min(6, "Độ dài email từ 6 đến 100 ký tự.")
        .max(100, "Độ dài email từ 6 đến 100 ký tự.")
        .required("Bạn cần nhập email."),

    name: Yup.string()
        .min(3, "Độ dài tên từ 3 đến 100 ký tự.")
        .max(100, "Độ dài tên từ 3 đến 100 ký tự.")
        .required("Bạn cần nhập tên của bạn."),

    password: Yup.string()
        .min(6, "Độ dài mật khẩu tối thiểu 6 ký tự.")
        .required("Bạn cần nhập mật khẩu của bạn."),

    confirm_password: Yup.string()
        .oneOf([Yup.ref('password'), null], "Nhập lại mật khẩu không đúng"),
        
    confirm_password_new: Yup.string()
        .oneOf([Yup.ref('newPassword'), null], "Nhập lại mật khẩu mới không đúng")
};

const validate = {
    login: {
        intialValue: {
            email: '',
            password: ''
        },
        message: Yup.object().shape({
            email: messageValidator.email,
            password: messageValidator.password
        })
    },
    register: {
       intialValue: {
            email: '',
            password: '',
            confirm_password: '',
            name: ''
       },
       message: Yup.object().shape({

        email: messageValidator.email,

        name: messageValidator.name,
    
        password: messageValidator.password,

        confirm_password: messageValidator.confirm_password
        })
    },
    password: {
        intialValue: {
            email: ''
        },
        message: Yup.object().shape({

            email: messageValidator.email
        })
    },
    profile: {
        
        message: Yup.object().shape({

            email: messageValidator.email,
            name: messageValidator.name
        })
    },
    change_password: {
        intialValue: {
            password: '',
            newPassword: '',
            confirm_password: '',
        },
        message: Yup.object().shape({
            password: messageValidator.password,
            newPassword: messageValidator.password,
            confirm_password: messageValidator.confirm_password_new
        })
    }
};



export { validate };