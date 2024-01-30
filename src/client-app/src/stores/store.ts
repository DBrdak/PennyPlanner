import { createContext, useContext } from "react";
import ModalStore from "./modalStore";
import LayoutStore from "./layoutStore";
import AccountStore from "./accountStore";

interface Store {
  modalStore: ModalStore
  layoutStore: LayoutStore
  accountStore: AccountStore
}

export const store: Store = {
  modalStore: new ModalStore(),
  layoutStore: new LayoutStore(),
  accountStore: new AccountStore(),

}

export const StoreContext = createContext(store);

export function useStore() {
  return useContext(StoreContext)
}