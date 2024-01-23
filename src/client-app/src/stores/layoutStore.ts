import {makeAutoObservable} from "mobx";

interface PageLayout {
    isAppOverlayDisplayed: boolean
    content: Element | null

}

export default class LayoutStore {
    layout: PageLayout = {
        isAppOverlayDisplayed: false,
        content: null
    }

    constructor() {
        makeAutoObservable(this);
    }

    openPageWithOverlay(content: Element) {
        this.layout.content = content
        this.layout.isAppOverlayDisplayed = true
    }

    openPageWithoutOverlay(content: Element) {
        this.layout.content = content
        this.layout.isAppOverlayDisplayed = false
    }
}