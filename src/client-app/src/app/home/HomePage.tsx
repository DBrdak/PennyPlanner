import AppOverlay, {dashboardSections} from "../../components/appOverlay/AppOverlay";
import {TilesLayout} from "../../components/tilesLayout/TilesLayout";
import {observer} from "mobx-react-lite";
import useTitle from "../../utils/hooks/useTitle";

export default observer(function HomePage() {

    useTitle('Home')


    return (
        <AppOverlay>
            <TilesLayout tiles={[

            ]} />
        </AppOverlay>
    );
})