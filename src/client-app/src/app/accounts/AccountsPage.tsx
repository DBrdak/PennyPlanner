import AppOverlay, {dashboardSections} from "../../components/appOverlay/AppOverlay";
import {TilesLayout} from "../../components/tilesLayout/TilesLayout";
import {TileContent} from "../../components/tilesLayout/TileContent";

export function AccountsPage() {
    const activeSectionIndex = dashboardSections.findIndex(s => s.title.toLowerCase() === 'accounts')

    return (
        <AppOverlay activeSectionIndex={activeSectionIndex}>
            <TilesLayout tiles={[
                {cols: 8, height: '100%', content:[
                    <TileContent
                        text={'View Accounts'}
                        img={'/assets/view_account.jpg'}
                        navigateToPath={'browse'} />
                ]},
                {cols: 4, height: '100%', content:[
                    <TileContent
                        text={'Add Account'}
                         img={'/assets/view_account.jpg'} 
                        navigateToPath={'new'} />
                ]},
            ]} />
        </AppOverlay>
    );
}