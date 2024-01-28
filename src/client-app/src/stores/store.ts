import { createContext, useContext } from "react";
import ModalStore from "./modalStore";
import LayoutStore from "./layoutStore";

interface Store {
  modalStore: ModalStore
  layoutStore: LayoutStore
}

export const store: Store = {
  modalStore: new ModalStore(),
  layoutStore: new LayoutStore()
}

export const StoreContext = createContext(store);

export function useStore() {
  return useContext(StoreContext)
}