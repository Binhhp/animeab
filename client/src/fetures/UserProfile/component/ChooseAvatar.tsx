import { useState } from "react";
import { Modal } from "react-bootstrap";
import { listAvt } from "../data/list_avt";
import "../css/avatar.css";

export default function ChooseAvatar({ user, avatar, setAvatar }: any) {
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    
    return (
        <div className="pr-img">
            <div className="pr-avt" onClick={handleShow}>
                <div className="edit-avt"><i className="fas fa-pen"></i></div>
                <img src={avatar} alt={user?.name} />
            </div>
            <Modal className="modal-avt" show={show} onHide={handleClose}>
                <div className="avt-title">
                    <h2><i className="fas fa-camera-retro mr-2"></i>Thay đổi ảnh đại diện</h2>
                </div>
                <div className="avt-list" data-simplebar>
                    {listAvt.map((item) => (
                        <div onClick={() => setAvatar(item.img)}
                         className={`avt-item${item.img === avatar ? ' avt-selected' : ''}`} key={item.id}>
                            <div className="checked"><i className="fas fa-check"></i></div>
                            <div className="avt-profile">
                                <img src={item.img} alt="Avatar" />
                            </div>
                        </div>
                    ))}
                </div>
                <button onClick={handleClose} className="choose-avt">Đặt làm ảnh đại diện</button>
                <button onClick={handleClose} className="pr-close" title="Thoát nào!">
                    <i className="fas fa-times"></i>
                </button>
            </Modal>
        </div>
    )
}