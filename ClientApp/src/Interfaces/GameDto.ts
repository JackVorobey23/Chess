import PieceDto from "./PieceDto";

export default interface GameDto {
    WPieceUserId: number;
    BPieceUserId: number;
    GameId: number;
    Pieces: PieceDto[];
}