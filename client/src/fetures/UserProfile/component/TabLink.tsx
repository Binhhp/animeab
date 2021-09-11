import { Nav } from "react-bootstrap";

export default function TabLink() {
    return (
        <Nav className="user-tabs">
            <Nav.Link eventKey="profile"><i className="fas fa-user mr-2"></i><span>Trang cá nhân</span></Nav.Link>
            <Nav.Link eventKey="password"><i className="fas fa-key mr-2"></i><span>Đổi mật khẩu</span></Nav.Link>
            <Nav.Link eventKey="love"><i className="fas fa-heart mr-2"></i><span>Anime yêu thích</span></Nav.Link>
        </Nav>
    )
}