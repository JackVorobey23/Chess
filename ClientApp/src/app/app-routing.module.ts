import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from "./home/home.component";
import { GameComponent } from "./game/game.component";

const routes: Routes = [
  { path: 'home-component', component: HomeComponent },
  { path: 'game-component', component: GameComponent },
  { path: '', redirectTo: '/home-component', pathMatch: 'full' }, // Default route
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }