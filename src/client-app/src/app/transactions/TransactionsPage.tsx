import AppOverlay from "../../components/appOverlay/AppOverlay";
import useTitle from "../../utils/hooks/useTitle";
import {observer} from "mobx-react-lite";

export default observer (function TransactionsPage() {
    useTitle('Transactions')

    return (
        <AppOverlay>

        </AppOverlay>
    );
})