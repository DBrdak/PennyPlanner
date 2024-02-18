import { createContext, useContext } from "react";
import ModalStore from "./modalStore";
import LayoutStore from "./layoutStore";
import AccountStore from "./accountStore";
import TransactionEntityStore from "./transactionEntityStore";
import TransactionStore from "./transactionStore";
import CategoryStore from "./categoryStore";
import TransactionSubcategoryStore from "./transactionSubcategoryStore";

interface Store {
  modalStore: ModalStore
  layoutStore: LayoutStore
  accountStore: AccountStore
  transactionEntityStore: TransactionEntityStore
  transactionStore: TransactionStore
  categoryStore: CategoryStore
  subcategoryStore: TransactionSubcategoryStore
}

export const store: Store = {
  modalStore: new ModalStore(),
  layoutStore: new LayoutStore(),
  accountStore: new AccountStore(),
  transactionEntityStore: new TransactionEntityStore(),
  transactionStore: new TransactionStore(),
  categoryStore: new CategoryStore(),
  subcategoryStore: new TransactionSubcategoryStore(),
}

export const StoreContext = createContext(store);

export function useStore() {
  return useContext(StoreContext)
}