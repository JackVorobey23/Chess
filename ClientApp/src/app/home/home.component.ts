import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import UserData from 'src/Interfaces/GameData';
import { GameDataService } from '../game-data.service';
import { SessionStorageService } from '../session-storage.service';
import { SignalRService } from '../signalr.service';
import GameDto from 'src/Interfaces/GameDto';
import { Router } from "@angular/router";

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  userData: UserData = this.sessionStorageService.getUserData();
  
  myEventSubscription: any;
  constructor(
    public SignalRService: SignalRService,
    public gameDataService: GameDataService,
    public sessionStorageService: SessionStorageService,
    public router: Router
  ) { }
  


  ngOnInit() {
    this.userData = this.sessionStorageService.getUserData();
    console.log(this.userData);
    
    this.myEventSubscription = this.gameDataService.newGameData$.subscribe(data => {

      let gameDto: GameDto = JSON.parse(data);
      console.log(gameDto);

      if (gameDto.User1Id == this.userData?.playerId || gameDto.User2Id == this.userData?.playerId) {
        
        this.userData.currentGameId = gameDto.GameId;
        this.sessionStorageService.setUserData(this.userData);

        this.myEventSubscription.unsubscribe();
        this.router.navigate(['/game']);
      }
    })
  };
  title = 'app';
  sendData() {
    this.SignalRService.askServer(String(this.userData?.currentGameId));
  }

  generateLogin() {

    if (this.userData != undefined) {

      this.userData.playerId = Math.round(Math.random() * 1000000);
      this.userData.displayLogin = "Ghost" + this.userData.playerId;
      this.userData.loginExist = true;
      
      this.sessionStorageService.setUserData(this.userData);
    }
    else {
      console.log("gameData is undefined");

    }
  }

  waitForGame() {
    if (this.userData != undefined) {
      this.SignalRService.addWaiter(this.userData.displayLogin, this.userData.playerId);
    }
    else {
      console.log("gameData is undefined");

    }
  }
  ngOnDestroy(): void {
    this.myEventSubscription.unsubscribe();
  }

}
