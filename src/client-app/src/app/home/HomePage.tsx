import AppOverlay, {dashboardSections} from "../../components/appOverlay/AppOverlay";
import {TilesLayout} from "../../components/tilesLayout/TilesLayout";
import {observer} from "mobx-react-lite";

export default observer(function HomePage() {

    return (
        <AppOverlay>
            <TilesLayout tiles={[

            ]} />
        </AppOverlay>
    );
})