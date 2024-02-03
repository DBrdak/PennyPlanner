import AppOverlay, {dashboardSections} from "../../components/appOverlay/AppOverlay";
import {TilesLayout} from "../../components/tilesLayout/TilesLayout";
import {observer} from "mobx-react-lite";
import useTitle from "../../utils/hooks/useTitle";

export default observer(function SettingsPage() {

    useTitle('Settings')

    return (
        <AppOverlay>
            <TilesLayout tiles={[

            ]} />
        </AppOverlay>
    );
})