const months = [
    'januar',
    'februar',
    'marts',
    'april',
    'maj',
    'juni',
    'juli',
    'august',
    'september',
    'oktober',
    'november',
    'december',
]

export function toLetterDate (dateAsString) {
    const date = new Date(dateAsString)
    const day = date.getDate()
    const month = months[date.getMonth()]
    const year = date.getFullYear()
    return `${day}. ${month} ${year}`
}

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
    return `${hour}:${minutes}`
}

export function toDateTime(dateAsString) {
    const date = toDate(dateAsString)
    const time = toTime(dateAsString)
    return `${date} ${time}`
}