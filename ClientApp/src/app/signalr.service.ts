import { ThisReceiver } from "@angular/compiler";
import { Injectable } from "@angular/core"; 
import * as signalR from "@aspnet/signalr";

@Injectable({providedIn: 'root'})
export class SignalRService {
    constructor () {}

    hubConnection?:signalR.HubConnection;

    startConnection = () => {
        this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl('https://localhost:7174/chess', {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets
        })
        .build();

        this.hubConnection
        .start()
        .then(() => {
            console.log('Hub connection started!');            
        })
        .catch(e => console.log(`damn!\n${e}`))
    }

    askServer() {
        this.hubConnection?.invoke("askServer", "hey")
            .catch(e => console.log(e));
    }

    askServerListener() {
        this.hubConnection?.on("askServerResponse", responseText => {
            console.log(responseText);
        })
    }
}