import AppOverlay, {dashboardSections} from "../../components/appOverlay/AppOverlay";

export function StatisticsPage() {
    const activeSectionIndex = dashboardSections.findIndex(s => s.title.toLowerCase() === 'statistics')

    return (
        <AppOverlay activeSectionIndex={activeSectionIndex}>

        </AppOverlay>
    );
}