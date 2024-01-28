import AppOverlay, {dashboardSections} from "../../components/appOverlay/AppOverlay";

export function UserPage() {
    const activeSectionIndex = dashboardSections.findIndex(s => s.title.toLowerCase() === 'user')

    return (
        <AppOverlay activeSectionIndex={activeSectionIndex}>

        </AppOverlay>
    );
}