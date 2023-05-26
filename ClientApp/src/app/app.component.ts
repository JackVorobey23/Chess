import { Component } from '@angular/core';
import { SignalRService } from './signalr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  constructor(
    public SignalRService: SignalRService
  ){}
  
  ngOnInit() {
    this.SignalRService.startConnection();
  };
  title = 'app';
}
