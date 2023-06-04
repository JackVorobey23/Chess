import { ThisReceiver } from "@angular/compiler";
import { Injectable } from "@angular/core";
import * as signalR from "@aspnet/signalr";
import { Observable, Subject } from "rxjs";

import { GameDataService } from "./game-data.service";

@Injectable({ providedIn: 'root' })
export class SignalRService {
    constructor(
        public gameDataService: GameDataService
    ) { }


    hubConnection?: signalR.HubConnection;

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

    newDataListener() {
        this.hubConnection?.on("gameStart", responseText => {
            this.gameDataService.newGameDataSubject.next(JSON.parse(responseText));
        })
        this.hubConnection?.on("newMove", responseText => {
            console.log(responseText);
            
            this.gameDataService.newMoveDataSubject.next(JSON.parse(responseText));
        })
        this.hubConnection?.on("gameEnd", responseText => {
            this.gameDataService.gameEndDataSubject.next(JSON.parse(responseText));
        })
    }
    addWaiter(playerName: string, playerId: number) {
        this.hubConnection?.invoke("AddWaiter", playerName, playerId)
            .catch(e => console.log(e));
    }

    endGame(gameId: number, winnerId: number) {
        this.hubConnection?.invoke("EndGame", gameId, winnerId)
            .catch(e => console.log(e))
    }

    makeMove(gameId: number, move: string) {
        this.hubConnection?.invoke("MakeMove", gameId, move)
    }
}