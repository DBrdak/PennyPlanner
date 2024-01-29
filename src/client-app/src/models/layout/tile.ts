import {TileContentProps} from "../../components/tilesLayout/TileContent";

export default interface Tile {
    cols: number;
    height: string
    content: React.ReactElement<TileContentProps>[]
}