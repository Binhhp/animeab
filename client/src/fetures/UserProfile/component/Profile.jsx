
import "../css/profile.css";
import { Form, ErrorMessage, Field, Formik } from "formik";
import { validate } from "../../../hooks/useMessage";
import { userService } from "../../../reduxs/user/api/userService";
import { useDispatch } from "react-redux";
import { useSelector } from "react-redux";
import LoadingElipsis from "../../../shared/Loading/LoadingElipsis/Loading";
import ChooseAvatar from "./ChooseAvatar";
import { useEffect, useState } from "react";
import { useHistory } from "react-router";

export default function Profile({ user, userLoggedIn }) {

    const dispatch = useDispatch();

    const profileAction = useSelector(state => state.profile);

    const history = useHistory();
    const [avatar, setAvatar] = useState("");

    const enableEmail = (e) => {
        document.querySelector('input[name="email"]').disabled = false;
    };

    useEffect(() => {
        if(user?.photo_url !== undefined){
            const url = user?.photo_url === "" ? 'https://i.imgur.com/t6mJtNE.png' : user?.photo_url;
            setAvatar(url);
        }
    }, [user, setAvatar]);

    return (
        <div className="profile-box">
            <h2 className="heading"><i className="fas fa-user mr-3"></i>Trang cá nhân</h2>
            <div className="block-profile-content profile">
                <ChooseAvatar avatar={avatar} setAvatar={setAvatar} user={user}></ChooseAvatar>
                <Formik 
                    enableReinitialize={true}
                    initialValues={{
                        email: user?.email,
                        name: user?.name 
                    }}
                    validationSchema={validate.profile.message}
                    validator={() => ({})}
                    onSubmit={async values => {
                        values.photo_url = avatar;
                        const result = await dispatch(userService.updateProfile(values, userLoggedIn?.user.localId));
                        
                        if(result === "logout"){
                            history.push("/");
                            dispatch(userService.logout());
                            return;
                        }
                    }}>
                    {({ errors, touched, values }) => (
                        <div className="w-100">
                            <Form className="perform">
                                <div className="col-12">
                                    <div className="form-group">
                                        <label htmlFor="Email" className="prelabel">Email</label>
                                        <div className="d-flex">
                                            <Field 
                                                className={`form-control${errors.email && touched.email ? ' is-invalid' : ''}`} 
                                                placeholder="name@gmail.com" 
                                                name="email" 
                                                autoComplete="off"
                                                value={values?.email}
                                                disabled/>
                                            <div className="enable-edit" onClick={() => enableEmail()}><i className="fas fa-pen"></i></div>
                                        </div>
                                        <ErrorMessage className="error" component="div" name="email" />
                                        <div className="note-email">
                                            Bạn chỉ có thể sử dụng một địa chỉ email cho một tài khoản. Phiên đăng nhập sẽ hết hạn ngay sau đó.
                                        </div>
                                    </div>
                                    <div className="form-group">
                                        <label htmlFor="DisplayName" className="prelabel">Tên hiển thị</label>
                                        <Field 
                                            className={`form-control${errors.name && touched.name ? ' is-invalid' : ''}`} 
                                            placeholder="Tên hiển thị" 
                                            name="name" 
                                            autoComplete="off"
                                            value={values?.name}/>
                                        <ErrorMessage className="error" component="div" name="name" />
                                    </div>
                                    <button 
                                        type="submit" 
                                        className="btn-profile" 
                                        title="Cập nhật thông tin">Cập nhật thông tin</button>
                                </div>
                            </Form>
                            <div className="pr-loader">
                                {profileAction?.loading && <LoadingElipsis></LoadingElipsis>}
                            </div>
                        </div>
                    )}
                </Formik>
            </div>
        </div>
    )
}