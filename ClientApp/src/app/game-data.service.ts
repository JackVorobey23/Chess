import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import UserData from 'src/Interfaces/GameData';

@Injectable({
  providedIn: 'root'
})
export class GameDataService {
  
  newGameDataSubject  = new Subject<any>();

  newGameData$: Observable<any> = this.newGameDataSubject.asObservable();

  constructor() { }
}
