import {useEffect} from "react";

const useTitle = (staticTitle?: string, dynamicTitle?: string) => {
    useEffect(() => {
        if(dynamicTitle) {
            document.title = dynamicTitle && `Penny Planner | ${dynamicTitle}`
        } else if(staticTitle) {
            document.title = `Penny Planner | ${staticTitle}`
        } else {
            document.title = 'Penny Planner'
        }
    }, [dynamicTitle, staticTitle])
}

export default useTitle
