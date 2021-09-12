import * as signaIR from "@microsoft/signalr";

const signaIRUrl = process.env.REACT_APP_SIGNAIR as string;
const hubConnection = new signaIR.HubConnectionBuilder()
                        .withUrl(signaIRUrl)
                        .build();

export { hubConnection }