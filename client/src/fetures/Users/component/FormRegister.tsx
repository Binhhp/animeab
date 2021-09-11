import { Form, Field, ErrorMessage, Formik } from "formik";
import { useDispatch, useSelector } from "react-redux";
import { userService } from "../../../reduxs/user/api/userService";
import { validate } from "../../../hooks/useMessage";
import Loading from "../../../shared/Loading/LoadingElipsis/Loading";

export default function FormRegister() {
    const dispatch = useDispatch();
    const register = useSelector((state: any) => state.userRegister);

    return(
        <Formik 
            initialValues={validate.register.intialValue}
            validationSchema={validate.register.message}
            onSubmit={(values, { resetForm }) => {
                dispatch(userService.register(values));
                setTimeout(() => {
                    resetForm()
                }, 2500);
            }}>
        {({ errors, touched }) => (
            <Form id="register-form" className="form-user">
                <div className="form-group">
                    <label className="label-login" htmlFor="email">Email</label>
                    <Field 
                        className={`form-control${errors.email && touched.email ? ' is-invalid' : ''}`} 
                        placeholder="name@gmail.com" 
                        name="email" autoComplete="off"/>
                    <ErrorMessage className="error" component="div" name="email" />
                </div>
                <div className="form-group">
                    <label className="label-login" htmlFor="name">Tên của bạn</label>
                    <Field 
                        className={`form-control${errors.name && touched.name ? ' is-invalid' : ''}`} 
                        placeholder="Tên của bạn" 
                        name="name" autoComplete="off"/>
                    <ErrorMessage className="error" component="div" name="name" />
                </div> 
                <div className="form-group">
                    <label className="label-login" htmlFor="password">Mật khẩu</label>
                    <Field 
                        as="input"
                        type="password"
                        className={`form-control${errors.password && touched.password ? ' is-invalid' : ''}`} 
                        placeholder="Mật khẩu" 
                        name="password" autoComplete="off" />
                    <ErrorMessage className="error" component="div" name="password" />   
                </div>
                <div className="form-group">
                    <label className="label-login" htmlFor="confirm_password">Nhập lại mật khẩu</label>
                    <Field 
                        as="input"
                        type="password"
                        className={`form-control${errors.confirm_password && touched.confirm_password ? ' is-invalid' : ''}`} 
                        placeholder="Nhập lại mật khẩu" 
                        name="confirm_password" autoComplete="off"/>
                    <ErrorMessage className="error" component="div" name="confirm_password" />   
                </div>
                <button title="Đăng kí tài khoản"
                    disabled={register?.registering}
                    type="submit" className="login-btn mt-3 form-group">
                    <span>Đăng kí luôn</span> 
                </button>
                {register?.registering && <Loading></Loading>}
            </Form>
        )}
        </Formik>
    )
}