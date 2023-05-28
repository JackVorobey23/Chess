import { Component } from '@angular/core';
import { SignalRService } from './signalr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  inputText: string | undefined;
  constructor(
    public SignalRService: SignalRService
  ){}
  
  ngOnInit() {
    this.SignalRService.startConnection();
    this.SignalRService.askServerListener();
  };
  title = 'app';
  sendData() {
    this.SignalRService.askServer(this.inputText!);
  }
}
