import PieceDto from "./PieceDto";

export default interface UserData {
    currentGameId: number;
    inputText: string;
    displayLogin: string;
    playerId: number;
    loginExist: boolean;
    currentUserMove: boolean;
    currentUserPieceColor: string;
    currentPieces: PieceDto[];
}