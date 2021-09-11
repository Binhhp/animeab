import { Form, Field, ErrorMessage, Formik } from "formik";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useActions } from "../../../reduxs/user/action/UserAction";
import { userService } from "../../../reduxs/user/api/userService";
import { validate } from "../../../hooks/useMessage";
import Loading from "../../../shared/Loading/LoadingElipsis/Loading";

export default function FormForgotPassword() {
    const dispatch = useDispatch();
    const password = useSelector((state: any) => state.userPassword);

    useEffect(() => {
        dispatch(useActions.passwordClear());
    }, [dispatch])

    return(
        (password?.success && 
        <span className="forgor-pw-success">
            <p className="text-center">Kiểm tra email của bạn! 🌱 Chúng tôi đã gửi 1 liên kết để đặt lại mật khẩu!</p>
        </span>)
        
        || <Formik 
                initialValues={validate.password.intialValue}
                validationSchema={validate.password.message}
                onSubmit={values => {
                dispatch(userService.password(values.email));
            }}>
        {({ errors, touched }) => (
            <Form id="forgot-password-form" className="form-user">
                <div className="form-group">
                    <label className="label-login" htmlFor="email">Email</label>
                    <Field 
                        className={`form-control${errors.email && touched.email ? ' is-invalid' : ''}`} 
                        placeholder="name@gmail.com" 
                        name="email" autoComplete="off"/>
                    <ErrorMessage className="error" component="div" name="email" />
                </div>
                <button 
                    title='Đặt lại mật khẩu'
                    disabled={password?.loading}
                    type="submit" className="login-btn form-group">
                        Đặt lại mật khẩu
                </button>
                {password?.loading && <Loading></Loading>}
            </Form>
        )}
        </Formik>
    )
}