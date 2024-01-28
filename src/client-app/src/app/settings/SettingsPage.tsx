import AppOverlay, {dashboardSections} from "../../components/appOverlay/AppOverlay";

export function SettingsPage() {
    const activeSectionIndex = dashboardSections.findIndex(s => s.title.toLowerCase() === 'settings')

    return (
        <AppOverlay activeSectionIndex={activeSectionIndex}>

        </AppOverlay>
    );
}