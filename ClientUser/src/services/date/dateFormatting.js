export function toDate(dateAsString) {
    const date = new Date(dateAsString)
    const day = date.getDate() < 10 ? `0${date.getDate()}` : date.getDate()
    const month = (date.getMonth() + 1) < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1
    const year = date.getFullYear()
    return `${day}-${month}-${year}`
}

export function toTime(dateAsString){
    const date = new Date(dateAsString)
    const hour = date.getHours() < 10 ? '0' + date.getHours() : date.getHours()
    const minutes = date.getMinutes() < 10 ? '0' + date.getMinutes() : date.getMinutes()
    const seconds = date.getSeconds() < 10 ? '0' + date.getSeconds() : date.getSeconds()
    return `${hour}:${minutes}:${seconds}`
}

export function toDateTime(dateAsString) {
    const date = toDate(dateAsString)
    const time = toTime(dateAsString)
    return `${date} ${time}`
}