import AppOverlay, {dashboardSections} from "../../components/appOverlay/AppOverlay";
import {TilesLayout} from "../../components/tilesLayout/TilesLayout";
import {TileContent} from "../../components/tilesLayout/TileContent";

export function GoalsPage() {
    const activeSectionIndex = dashboardSections.findIndex(s => s.title.toLowerCase() === 'goals')

    return (
        <AppOverlay activeSectionIndex={activeSectionIndex}>
            <TilesLayout tiles={[

            ]} />
        </AppOverlay>
    );
}