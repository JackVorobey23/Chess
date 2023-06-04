import { Component } from '@angular/core';
import { SignalRService } from './signalr.service';
import { GameDataService } from './game-data.service';
import GameDto from 'src/Interfaces/GameDto';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  myEventSubscription: any;
  constructor(
    public SignalRService: SignalRService,
    public gameDataService: GameDataService
  ) { }


  ngOnInit() {
    
    this.SignalRService.startConnection();

    this.SignalRService.askServerListener();

    this.SignalRService.newDataListener();

  };
  title = 'app';
}
