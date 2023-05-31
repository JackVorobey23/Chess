import { Injectable } from '@angular/core';
import { User } from 'oidc-client';
import UserData from 'src/Interfaces/GameData';

@Injectable({
  providedIn: 'root'
})
export class SessionStorageService {

  constructor() {}

  setUserData(value: UserData): void {
    sessionStorage.setItem('userData', JSON.stringify(value));
  }

  getUserData(): UserData {
    
    let foundItem = sessionStorage.getItem('userData');
    
    let userData: UserData = {

      currentGameId: -1,
      inputText: "",
      displayLogin: "",
      playerId: -1,
      loginExist: false
    };
    
    if(foundItem != null){
      userData = JSON.parse(foundItem);
    }
    return userData;
  }

  clear(): void {
    sessionStorage.clear();
  }
}
