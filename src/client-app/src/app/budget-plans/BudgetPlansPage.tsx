import AppOverlay from "../../components/appOverlay/AppOverlay";
import {observer} from "mobx-react-lite";
import useTitle from "../../utils/hooks/useTitle";
import BudgetPlanContainer from "./components/BudgetPlanContainer";

export default observer (function BudgetPlansPage() {

    useTitle('Budget Plans')

    return (
        <AppOverlay>
            <BudgetPlanContainer />
        </AppOverlay>
    );
})