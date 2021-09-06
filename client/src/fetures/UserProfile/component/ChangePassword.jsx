
import { Form, ErrorMessage, Field, Formik } from "formik";
import { useSelector } from "react-redux";
import { useDispatch } from "react-redux";
import { validate } from "../../../hooks/useMessage";
import { userService } from "../../../reduxs/user/api/userService";
import LoadingElipsis from "../../../shared/Loading/LoadingElipsis/Loading";
export default function ChangePassword({ uid, user }) {

    const dispatch = useDispatch();
    const changePw = useSelector(state => state.changePassword);

    const isShowPw = (id, e) => {
        const elementInput = document.getElementById(id);
        if(elementInput.type === "password"){

            elementInput.type = 'text';
            e.target.classList.add("fa-eye-slash");
        }
        else{
            elementInput.type = 'password';
            e.target.classList.remove("fa-eye-slash");
        }
    };

    return (
        <div className="profile-box pw">
            <h2 className="heading"><i className="fas fa-key mr-3"></i>Đổi mật khẩu</h2>
            <div className="block-profile-content">
            <Formik 
                    enableReinitialize={true}
                    initialValues={validate.change_password.intialValue}
                    validationSchema={validate.change_password.message}
                    validator={() => ({})}
                    onSubmit={values => {
                        values['email'] = user?.email;
                        return dispatch(userService.changePassword(uid, values));
                    }}>
                    {({ errors, touched }) => (
                        <div className="change-pw">
                            <Form className="perform">
                                <div className="col-12">
                                    <div className="form-group">
                                        <label htmlFor="password" className="prelabel">Mật khẩu hiện tại</label>
                                        <div className="d-flex">
                                            <Field 
                                                id="password"
                                                as="input"
                                                type="password"
                                                className={`password form-control${errors.password && touched.password ? ' is-invalid' : ''}`} 
                                                placeholder="Mật khẩu hiện tại" 
                                                name="password" 
                                                autoComplete="off"/>
                                            <div onClick={(e) => isShowPw('password', e)} 
                                                className="show-hidden-pw enable-edit"><i className="fas fa-eye"></i>
                                            </div>
                                        </div>
                                        <ErrorMessage className="error" component="div" name="password" />
                                    </div>
                                    <div className="form-group">
                                        <label htmlFor="newPassword" className="prelabel">Mật khẩu mới</label>
                                        <div className="d-flex">
                                            <Field 
                                                id="newPassword"
                                                as="input"
                                                type="password"
                                                className={`password form-control${errors.newPassword && touched.newPassword ? ' is-invalid' : ''}`} 
                                                placeholder="Mật khẩu mới" 
                                                name="newPassword" 
                                                autoComplete="off"/>
                                            <div onClick={(e) => isShowPw('newPassword', e)} 
                                            className="show-hidden-pw enable-edit"><i className="fas fa-eye"></i></div>
                                        </div>
                                        <ErrorMessage className="error" component="div" name="newPassword" />
                                    </div>
                                    <div className="form-group">
                                        <label htmlFor="confirm_password" className="prelabel">Nhập lại mật khẩu</label>
                                        <div className="d-flex">
                                            <Field 
                                                id="confirm_password"
                                                as="input"
                                                type="password"
                                                className={`password form-control${errors.confirm_password && touched.confirm_password ? ' is-invalid' : ''}`} 
                                                placeholder="Nhập lại mật khẩu" 
                                                name="confirm_password" 
                                                autoComplete="off" />
                                            <div onClick={(e) => isShowPw('confirm_password', e)} 
                                             className="show-hidden-pw enable-edit"><i className="fas fa-eye"></i></div>
                                        </div>
                                        <ErrorMessage className="error" component="div" name="confirm_password" />
                                    </div>
                                    <button type="submit" className="btn-profile" title="Đổi mật khẩu">Đổi mật khẩu</button>
                                </div>
                            </Form>
                            <div className="pr-loader">
                                {changePw?.loading && <LoadingElipsis></LoadingElipsis>}
                            </div>
                        </div>
                    )}
                </Formik>
            </div>
        </div>
    )
}