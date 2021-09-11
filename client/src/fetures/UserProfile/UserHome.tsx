import { useSelector } from "react-redux";
import "./css/style.css";
import Layout from "../../layouts/Layout/Layout";
import { Tab } from "react-bootstrap";
import Profile from "./component/Profile";
import TabLink from "./component/TabLink";
import ChangePassword from "./component/ChangePassword";
import { useParams } from "react-router";
import AnimeLoves from "./component/AnimeLoves";

interface QuizParams {
    meta: string
}

export default function UserHome() {
    const userLoggedIn = useSelector((state: any) => state.userLoggedIn);
    const user = useSelector((state: any) => state.userCurrent);

    const { meta } = useParams<QuizParams>();

    return (
        userLoggedIn?.loggedIn &&
        <Layout title={`Nhà ${user?.name}`} descript="Xem phim anime online free trên AnimeAB">
            <div className="user-profile-home">
                <Tab.Container defaultActiveKey={meta}>
                    <div className="profile-header">
                        <div className="profile-bg" 
                        style={{backgroundImage: `url(${user?.photo_url === "" 
                        ? "https://i.imgur.com/q4Gd1Wi.jpg" : user?.photo_url })`}}></div>

                        <div className="container">
                            <div className="pr-title">{user?.name}</div>
                            <div className="pr-tabs">
                                <div className="bah-tab-min">
                                    <TabLink></TabLink>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="profile-content">
                        <Tab.Content className="block_area-content">
                            <Tab.Pane eventKey="profile">
                                <Profile user={user} userLoggedIn={userLoggedIn}></Profile>
                            </Tab.Pane>
                            <Tab.Pane eventKey="password">
                                <ChangePassword user={user} uid={userLoggedIn?.user.localId}></ChangePassword>
                            </Tab.Pane>
                            <Tab.Pane eventKey="love">
                                <AnimeLoves></AnimeLoves>
                            </Tab.Pane>
                        </Tab.Content>
                    </div>       
                </Tab.Container>
            </div>
        </Layout>
    )
}