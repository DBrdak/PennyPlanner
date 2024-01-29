import AppOverlay, {dashboardSections} from "../../../components/appOverlay/AppOverlay";

export function BrowseAccounts() {
    const activeSectionIndex = dashboardSections.findIndex(s => s.title.toLowerCase() === 'accounts')

    return (
        <AppOverlay activeSectionIndex={activeSectionIndex}>
            Hello
        </AppOverlay>
    );
}