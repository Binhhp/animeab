import React from "react";
import { Nav, Tab } from "react-bootstrap";
import "../css/area.css";
import Item from "./Item";

export default function AreaLeft({ ...props }){

    return(
        <div id="block-area-left">
            <Tab.Container defaultActiveKey="today">
                <div className="slideout-heading">  
                    <div className="d-inline-block">
                        <h5 className="cat-heading">{props.title}</h5>
                        <div className="bah-tab-min">
                            <Nav variant="pills" className="anw-tabs nav-fill">
                                <Nav.Item>
                                    <Nav.Link eventKey="today">Ngày</Nav.Link>
                                </Nav.Item>
                                <Nav.Item>
                                    <Nav.Link eventKey="week">Tuần</Nav.Link>
                                </Nav.Item>
                                <Nav.Item>
                                    <Nav.Link eventKey="month">Tháng</Nav.Link>
                                </Nav.Item>
                            </Nav>
                        </div>
                    </div>	
                </div>
                <Tab.Content className="block-area">
                    <Tab.Pane eventKey="today" className="cbox-content">
                        <Item keyword="day" animes={props.animeDays}></Item>
                    </Tab.Pane>
                    <Tab.Pane eventKey="week" className="cbox-content">
                        <Item keyword="week" animes={props.animeWeeks}></Item>
                    </Tab.Pane>
                    <Tab.Pane eventKey="month" className="cbox-content">
                        <Item keyword="month" animes={props.animeMonths}></Item>
                    </Tab.Pane>
                </Tab.Content>
            </Tab.Container>
        </div>  
    )
}