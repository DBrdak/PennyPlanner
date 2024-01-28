import {makeAutoObservable} from "mobx";

export default class LayoutStore {
    isDrawerOpen: boolean = false

    constructor() {
        makeAutoObservable(this);
    }

    setDrawerState() {
        this.isDrawerOpen = !this.isDrawerOpen
    }
}