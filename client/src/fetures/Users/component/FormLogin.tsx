import React from "react";
import { Form, ErrorMessage, Field, Formik } from "formik";
import { validate } from "../../../hooks/useMessage";
import { useDispatch } from "react-redux";
import { userService } from "../../../reduxs/user/api/userService";
import Loading from "../../../shared/Loading/LoadingElipsis/Loading";

function FormLogin({hanlderForgotPassowrd, userLogined}: any){
    const dispatch = useDispatch();

    return(
        <Formik 
            initialValues={validate.login.intialValue}
            validationSchema={validate.login.message}
            validator={() => ({})}
            onSubmit={(values: any) => {
                let email = values.email;
                let password = values.password;
                dispatch(userService.login(email, password));
            }}>
        {({ errors, touched }) => (
            <Form id="login-form" className="form-user">
                <div className="form-group">
                    <label className="label-login" htmlFor="email">Email</label>
                    <Field 
                        className={`form-control${errors.email && touched.email ? ' is-invalid' : ''}`} 
                        placeholder="name@gmail.com" 
                        name="email" autoComplete="off"/>
                    <ErrorMessage className="error" component="div" name="email" />
                </div>
                <div className="form-group">
                    <label className="label-login" htmlFor="password">Mật khẩu</label>
                    <Field 
                        as="input"
                        type="password"
                        className={`form-control${errors.password && touched.password ? ' is-invalid' : ''}`} 
                        placeholder="Mật khẩu" 
                        name="password" autoComplete="off"/>
                    <ErrorMessage className="error" component="div" name="password" />   
                </div>
                <div className="form-check">
                    <span onClick={e => hanlderForgotPassowrd(e)} 
                        className="forgot-password">Quên mật khẩu?</span>
                </div>
                <button title="Đăng nhập"
                    disabled={userLogined?.loggingIn}
                    type="submit" className="login-btn form-group">
                    <span>Đăng nhập</span>
                </button>
                {userLogined?.loggingIn && <Loading></Loading>}
            </Form>
        )}
        </Formik>
        
    )
}
export default FormLogin;