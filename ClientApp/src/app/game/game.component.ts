import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-game',
  standalone: false,
  template: `
    <p>
      game works!
    </p>
  `,
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
