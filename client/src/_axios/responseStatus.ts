export default function responseStatus(code: any, message: string | any, data: any, errors: string | any) {
    return {
        code: code,
        message: message,
        errors: errors,
        data: data
    }
}