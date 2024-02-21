import {useEffect} from "react";

const useTitle = (staticTitle?: string, dynamicTitle?: string) => {
    useEffect(() => {
        if(dynamicTitle) {
            document.title = dynamicTitle && `Budgetify | ${dynamicTitle}`
        } else if(staticTitle) {
            document.title = `Budgetify | ${staticTitle}`
        } else {
            document.title = 'Budgetify'
        }
    }, [dynamicTitle, staticTitle])
}

export default useTitle
