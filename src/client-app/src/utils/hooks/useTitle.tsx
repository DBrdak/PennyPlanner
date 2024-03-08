import {useEffect} from "react";

const useTitle = (staticTitle?: string, dynamicTitle?: string) => {
    useEffect(() => {
        if(dynamicTitle) {
            document.title = dynamicTitle && `PennyPlanner | ${dynamicTitle}`
        } else if(staticTitle) {
            document.title = `PennyPlanner | ${staticTitle}`
        } else {
            document.title = 'PennyPlanner'
        }
    }, [dynamicTitle, staticTitle])
}

export default useTitle
