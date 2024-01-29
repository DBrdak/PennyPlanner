import AppOverlay, {dashboardSections} from "../../components/appOverlay/AppOverlay";
import {TilesLayout} from "../../components/tilesLayout/TilesLayout";
import {TileContent} from "../../components/tilesLayout/TileContent";

export function BudgetPlansPage() {
    const activeSectionIndex = dashboardSections.findIndex(s => s.title.toLowerCase() === 'budget plans')

    return (
        <AppOverlay activeSectionIndex={activeSectionIndex}>
            <TilesLayout tiles={[

            ]} />
        </AppOverlay>
    );
}