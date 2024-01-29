import AppOverlay, {dashboardSections} from "../../../components/appOverlay/AppOverlay";
import {TilesLayout} from "../../../components/tilesLayout/TilesLayout";


export function AddAccount() {
    const activeSectionIndex = dashboardSections.findIndex(s => s.title.toLowerCase() === 'accounts')

    return (
        <AppOverlay activeSectionIndex={activeSectionIndex}>
        </AppOverlay>
    );
}