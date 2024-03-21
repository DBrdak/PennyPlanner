import AppOverlay from "../../components/appOverlay/AppOverlay";
import {observer} from "mobx-react-lite";
import useTitle from "../../utils/hooks/useTitle";
import BudgetPlanContainer from "./components/BudgetPlanContainer";
import useAuthProvider from "../../utils/hooks/useAuthProvider";

export default observer (function BudgetPlansPage() {
    useAuthProvider()

    useTitle('Budget Plans')

    return (
        <AppOverlay>
            <BudgetPlanContainer />
        </AppOverlay>
    );
})