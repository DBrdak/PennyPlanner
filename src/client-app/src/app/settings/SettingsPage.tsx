import AppOverlay, {dashboardSections} from "../../components/appOverlay/AppOverlay";
import {TilesLayout} from "../../components/tilesLayout/TilesLayout";
import {TileContent} from "../../components/tilesLayout/TileContent";

export function SettingsPage() {
    const activeSectionIndex = dashboardSections.findIndex(s => s.title.toLowerCase() === 'settings')

    return (
        <AppOverlay activeSectionIndex={activeSectionIndex}>
            <TilesLayout tiles={[

            ]} />
        </AppOverlay>
    );
}