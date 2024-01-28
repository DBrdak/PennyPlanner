import AppOverlay, {dashboardSections} from "../../components/appOverlay/AppOverlay";

export function GoalsPage() {
    const activeSectionIndex = dashboardSections.findIndex(s => s.title.toLowerCase() === 'goals')

    return (
        <AppOverlay activeSectionIndex={activeSectionIndex}>

        </AppOverlay>
    );
}