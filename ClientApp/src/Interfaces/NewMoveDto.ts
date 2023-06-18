import PieceDto from "./PieceDto";

export default interface NewMoveDto {
    GameId: number;
    Pieces: PieceDto[];
}