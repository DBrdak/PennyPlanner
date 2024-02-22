import theme from "../../app/theme";

export const calcColorForCategory = (type: string) => type.toLowerCase() === 'income' ? theme.palette.success.light : theme.palette.error.main
