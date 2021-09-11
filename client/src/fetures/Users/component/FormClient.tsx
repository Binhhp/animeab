import { Modal } from "react-bootstrap";
import "../css/login.css";
import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { useSelector } from "react-redux";
import FormRegister from "./FormRegister";
import FormLogin from "./FormLogin";
import FormForgotPassword from "./FormForgotPassword";
import ClientInfo from "./ClientInfo";

function FormClient() {
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => {
        setShow(true);
        setRegister(false);
        setForgotPassword(false);
    }

    const [isRegister, setRegister] = useState(false);
    const hanlderRegister = (e: any) => {
        e.preventDefault();
        setRegister(!isRegister);
    };
    
    const [isForgotPassword, setForgotPassword] = useState(false);
    const hanlderForgotPassowrd = (e: any) => {
        e.preventDefault();
        setForgotPassword(!isForgotPassword);
    };

    const userLogined = useSelector((state: any) => state.userLoggedIn);
    
    useEffect(() => {
        if(userLogined?.loggedIn){
            handleClose();
        }
    }, [userLogined])


    return(
        <div className="form-login">
            <ClientInfo handleShow={handleShow} show={show} userLogined={userLogined}></ClientInfo>
            <Modal 
                show={show} 
                className="modal-login"
                onHide={handleClose} 
                aria-labelledby="contained-modal-title-vcenter"
                centered>
                <div className="bg-modal">
                    <Modal.Header>
                        <span>
                            {isRegister ? 'Đăng kí tài khoản!' : 
                                (isForgotPassword ? 'Quên mật khẩu?' : 'Đăng nhập@')}
                        </span>
                    </Modal.Header>
                    <Modal.Body>
                        {(isRegister && <FormRegister></FormRegister>)
                            || (isForgotPassword && <FormForgotPassword></FormForgotPassword>)
                            || <FormLogin 
                                userLogined={userLogined}
                                hanlderForgotPassowrd={hanlderForgotPassowrd}></FormLogin>}
                    </Modal.Body>
                    <Modal.Footer>
                        <div className="link">
                            {
                                isRegister 
                                ? <span> Đã có tài khoản? <Link onClick={(e) => hanlderRegister(e)} className="link-register" to="/">Đăng nhập nào</Link></span>
                                : (isForgotPassword 
                                    ? <span className="back-to-login">
                                        <i className="fas fa-chevron-left"></i>&nbsp;&nbsp;
                                        <Link onClick={(e) => hanlderForgotPassowrd(e)} className="link-register" to="/">Quay lại đăng nhập</Link></span>
                                    : <span>Bạn chưa có tài khoản? <Link onClick={(e) => hanlderRegister(e)} className="link-register" to="/">Đăng kí ngay</Link></span>)
                            }
                        </div>
                    </Modal.Footer>
                    <div onClick={handleClose} className="close-btn"><i className="fas fa-times"></i></div>
                </div>
            </Modal>
        </div>
    )
}
export default React.memo(FormClient);