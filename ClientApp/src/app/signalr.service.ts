import { ThisReceiver } from "@angular/compiler";
import { Injectable } from "@angular/core"; 
import * as signalR from "@aspnet/signalr";
import { Observable, Subject } from "rxjs";

import {GameDataService} from "./game-data.service";

@Injectable({providedIn: 'root'})
export class SignalRService {
    constructor (
        public gameDataService: GameDataService
    ) {}
    
    
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

    askServer(text: string) {
        this.hubConnection?.invoke("AskServer", text)
            .catch(e => console.log(e));
    }

    askServerListener() {
        this.hubConnection?.on("askResponse", responseText => {
            console.log(responseText);
        })
    }

    gameStartListener() {
        this.hubConnection?.on("gameStartListener", responseText => {
            this.gameDataService.newGameDataSubject.next(responseText);
        })
    }

    addWaiter(playerName: string, playerId: number){
        this.hubConnection?.invoke("AddWaiter", playerName, playerId)
            .catch(e => console.log(e));
    }
}