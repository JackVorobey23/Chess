import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import GameDto from 'src/Interfaces/GameDto';
import NewMoveDto from 'src/Interfaces/NewMoveDto';
import UserData from 'src/Interfaces/UserData';

@Injectable({
  providedIn: 'root'
})
export class GameDataService {
  
  newGameDataSubject  = new Subject<GameDto>();
  gameEndDataSubject  = new Subject<number>();
  newMoveDataSubject  = new Subject<NewMoveDto>();

  newGameData$: Observable<GameDto> = this.newGameDataSubject.asObservable();
  gameEndData$: Observable<number> = this.gameEndDataSubject.asObservable();
  newMoveData$: Observable<NewMoveDto> = this.newMoveDataSubject.asObservable();

  constructor() { }
}
