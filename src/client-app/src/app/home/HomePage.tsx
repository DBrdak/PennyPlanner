import AppOverlay, {dashboardSections} from "../../components/appOverlay/AppOverlay";

export function HomePage() {
    const activeSectionIndex = dashboardSections.findIndex(s => s.title.toLowerCase() === 'home')

    return (
        <AppOverlay activeSectionIndex={activeSectionIndex}>

        </AppOverlay>
    );
}