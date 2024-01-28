import AppOverlay, {dashboardSections} from "../../components/appOverlay/AppOverlay";

export function BudgetPlansPage() {
    const activeSectionIndex = dashboardSections.findIndex(s => s.title.toLowerCase() === 'budget plans')

    return (
        <AppOverlay activeSectionIndex={activeSectionIndex}>

        </AppOverlay>
    );
}