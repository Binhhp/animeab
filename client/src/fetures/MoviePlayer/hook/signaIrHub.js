import * as signaIR from "@microsoft/signalr";

const hubConnection = new signaIR.HubConnectionBuilder()
                        .withUrl(process.env.REACT_APP_SIGNAIR)
                        .build();

export { hubConnection }