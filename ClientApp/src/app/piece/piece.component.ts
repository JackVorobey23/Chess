import { Component, Input, OnInit } from '@angular/core';
import { SessionStorageService } from '../session-storage.service';
import PieceDto from '../../Interfaces/PieceDto'

@Component({
  selector: 'app-piece',
  templateUrl: './piece.component.html',
  styleUrls: ['./piece.component.css']
})

export class PieceComponent implements OnInit {

  @Input() item: number[] | undefined;

  constructor(
    public sessionStorageService: SessionStorageService
  ) { }
  ngOnInit(): void {
  }

  getStyle(i: number, j: number) {
    let toRetern = '';

    if (i % 2 == 0) {
      toRetern = j % 2 == 1 ? 'rgb(183, 114, 58)' : 'rgb(247, 201, 163)';
    }
    else {
      toRetern = j % 2 == 1 ? 'rgb(247, 201, 163)' : 'rgb(183, 114, 58)';
    }
    return toRetern;
  }

  getPieceUrl(i: number, j: number) {

    let chars = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h']

    let numCoord = this.sessionStorageService.getUserData().currentUserPieceColor == "White" ? 9 - i : i;
    
    let charCoord = this.sessionStorageService.getUserData().currentUserPieceColor == "White" ? chars[j - 1] : chars[8 - j];

    let piecePos = charCoord + String(numCoord);

    let piece = this.sessionStorageService
      .getUserData().currentPieces
      .find(p => p.PiecePosition == piecePos);
    
    if(piece == undefined){
      return ''
    }
    let path = '../../assets/images/pieces/'

    path += `${piece.PieceName}_${piece.PieceColor}.png`

    return path;
  }
}
