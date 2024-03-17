const unusualPaths: {path: RegExp, name: string}[] = [
    {path: RegExp('/accounts/new'), name: "New Account"},
    {path: RegExp('/accounts/[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}'), name: "Account"},
]

const sectionName = (pathname: string): string => {
    const unusualPath = unusualPaths.find(
        p => p.path.test(pathname)
    )?.name

    if(unusualPath) {
        return unusualPath
    }

    const sectionName = pathname
        .slice(pathname.lastIndexOf('/') + 1)
        .replaceAll('-', ' ')
        .split(' ')
        .map(word => word.slice(0, 1).toUpperCase().concat(word.slice(1)))
        .join(' ')

    return sectionName
}

export default sectionName