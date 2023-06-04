import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import UserData from 'src/Interfaces/UserData';
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

    this.myEventSubscription = this.gameDataService.newGameData$.subscribe(gameDto => {

      console.log(gameDto);

      if (gameDto.WPieceUserId == this.userData?.playerId || gameDto.BPieceUserId == this.userData?.playerId) {

        this.userData.currentGameId = gameDto.GameId;
        this.userData.currentUserMove = this.userData.playerId == gameDto.WPieceUserId;
        this.userData.currentPieces = gameDto.Pieces;
        this.userData.currentUserPieceColor = this.userData.currentUserMove ? "White" : "Black";
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
    
    const adjectives = [
    "Fierce", "Mystic", "Daring", "Wise", "Cunning", "Fearless", "Savage", "Noble", "Swift", "Fiery", "Grim", "Wicked", "Valiant", 
    "Brilliant", "Enigmatic", "Dreadful", "Crafty", "Ethereal", "Vengeful", "Radiant", "Heroic", "Sinister", "Sly", "Majestic", "Whispering", 
    "Arcane", "Shadow", "Infernal", "Golden", "Sorcerous", "Sneaky", "Luminous", "Demonic", "Stalwart", "Clever", "Wily", "Virtuous", 
    "Furious", "Swift", "Doomed", "Mighty", "Wise", "Thundering", "Venomous", "Silent", "Vigilant", "Dashing", "Elegant", "Brazen", 
    "Enchanted", "Radiant"
    ];
    
    const characters = [
    "Dragon", "Elf", "Griffin", "Sorcerer", "Phoenix", "Witch", "Centaur", "Warlock", "Wizard", "Vampire", "Goblin", "Sprite", "Fairy", 
    "Nymph", "Gorgon", "Basilisk", "Ogre", "Minotaur", "Chimera", "Satyr", "Harpy", "Mermaid", "Ninja", "Samurai", "Pegasus", "Troll", 
    "Werewolf", "Cyclops", "Shapeshifter", "Ghost", "Demon", "Giant", "Angel", "Siren", "Phantom", "Jinn", "Gargoyle", "Dwarf", "Troll", 
    "Valkyrie", "Kraken", "Unicorn", "Orc", "Salamander", "Wraith", "Leprechaun", "Banshee", "Warrior", "Paladin", "Knight", "Necromancer", 
    "Druid"
    ];
    if (this.userData != undefined) {

      this.userData.playerId = Math.round(Math.random() * 1000000);
      
      let tempLogin = 
        `${adjectives[Math.floor(Math.random() * adjectives.length)]
        }_${characters[Math.floor(Math.random() * characters.length)]}`
      
      this.userData.displayLogin = `${tempLogin}${this.userData.playerId}`;
      this.userData.loginExist = true;

      this.sessionStorageService.setUserData(this.userData);
    }
    else {
      console.log("gameData is undefined");

    }
  }

  waitForGame() {
    this.userData = this.sessionStorageService.getUserData();
    console.log(this.userData);

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
