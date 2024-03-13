import {makeAutoObservable} from "mobx";

export default class LayoutStore {
    isDrawerOpen: boolean = false
    activeSectionIndex: number = 0

    constructor() {
        makeAutoObservable(this);

        const initialPathname = window.location.pathname;
        this.setActiveSectionIndexByPath(initialPathname);
    }

    setActiveSectionIndexByPath(path: string) {
        const sectionPaths = [
            { path: '/home', index: 0 },
            { path: '/budget-plans', index: 1 },
            { path: '/accounts', index: 2 },
            { path: '/goals', index: 3 },
            { path: '/statistics', index: 4 },
            { path: '/settings', index: 5 },
            { path: '/user', index: 6 },
            {path: '/transactions', index: 7}
        ];

        const matchingSection = sectionPaths.find((section) =>
            path.startsWith(section.path)
        );

        if (matchingSection) {
            this.setActiveSectionIndex(matchingSection.index);
        } else {
            this.setActiveSectionIndex(0);
        }
    }

    setDrawerState(state?: boolean) {
        if(state !== undefined){
            this.isDrawerOpen = state
        } else {
            this.isDrawerOpen = !this.isDrawerOpen
        }
    }

    setActiveSectionIndex(index: number){
        this.activeSectionIndex = index
    }
}