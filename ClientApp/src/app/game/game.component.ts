import { Component, OnDestroy, OnInit } from '@angular/core';
import { SessionStorageService } from '../session-storage.service';
import UserData from 'src/Interfaces/UserData';
import { Router } from '@angular/router';
import { SignalRService } from "../signalr.service";
import { GameDataService } from '../game-data.service';
import PieceDto from 'src/Interfaces/PieceDto';

@Component({
  selector: 'app-game',
  standalone: false,
  templateUrl: "./game.component.html",
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit, OnDestroy {

  userData: UserData = this.sessionStorageService.getUserData();
  currentGameExist: boolean = false;
  currentUserMove: boolean = false;
  myGameDataSubscription: any;
  myMoveDataSubscription: any;
  reload: boolean = true;

  cycleForBoard: [number, number][] = [];

  constructor(
    public sessionStorageService: SessionStorageService,
    public SignalRService: SignalRService,
    public gameDataService: GameDataService,
    public router: Router
  ) { }

  ngOnInit(): void {

    for (let i = 1; i <= 8; i++) {
      for (let j = 1; j <= 8; j++) {
        this.cycleForBoard.push([i, j]);
      }
    }

    console.log(this.cycleForBoard);

    this.userData = this.sessionStorageService.getUserData();

    if (this.userData.currentGameId != -1) {
      this.currentGameExist = true;
    }

    if(this.userData.currentUserPieceColor == "White"){
      this.currentUserMove = true;
    }

    this.myMoveDataSubscription = this.gameDataService.newMoveData$.subscribe(data => {
      
      console.log(data);
      
      if(data.GameId == this.userData.currentGameId){
        
        if(this.currentUserMove) {

          this.currentUserMove = false;
        }
        else {
          this.currentUserMove = true;
        }

        this.visualizeMove(data.Move);
      }
    })

    this.myGameDataSubscription = this.gameDataService.gameEndData$.subscribe(data => {

      console.log(data);

      if (Number(data) == this.userData?.currentGameId) {

        this.userData.currentGameId = -1;
        this.userData.currentUserMove = false;


        this.sessionStorageService.setUserData(this.userData);

        this.router.navigate(['/home']);
      }
    })
  }

  endGame() {
    this.SignalRService.endGame(this.userData.currentGameId, this.userData.playerId);
    this.userData.currentGameId = -1;
    this.sessionStorageService.setUserData(this.userData);
    this.router.navigate(['/home']);
  }

  ngOnDestroy(): void {
    this.myGameDataSubscription.unsubscribe();
    this.myMoveDataSubscription.unsubscribe();
  }

  visualizeMove(move: string) {
    let parsedMove: string[] = move.split('-');
    console.log(parsedMove);

    let tempPiece = this.sessionStorageService
      .getUserData().currentPieces
      .find(p => p.PiecePosition == parsedMove[0]);

      if (tempPiece == undefined) {
        console.log("move is not possible!");
        return;
      }
      else {
        this.userData.currentPieces = this.userData.currentPieces.filter(p => p.PiecePosition != parsedMove[0])
  
        let movedPiece: PieceDto = {
          PieceColor: tempPiece.PieceColor,
          PieceName: tempPiece.PieceName,
          PiecePosition: parsedMove[1]
        }
  
        this.userData.currentPieces.push(movedPiece)
  
        this.sessionStorageService.setUserData(this.userData);
        let reload = this.cycleForBoard;
        this.cycleForBoard = [];
        setTimeout(() => this.cycleForBoard = reload);
      }
  }

  makeMove(newMove: string) {

    this.SignalRService.makeMove(this.userData.currentGameId, newMove);
  }
  
  ComeBack() {
    this.router.navigate(['/home']);
  }
}
