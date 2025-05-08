export function saveAccessToken(token) {
    localStorage.setItem("jwtAccess", token)
}

export function getAccessToken() {
    return localStorage.getItem("jwtAccess") ?? ""
}

export function saveRefreshToken(token) {
    localStorage.setItem("jwtRefresh", token)
}

export function getRefreshToken() {
    return localStorage.getItem("jwtRefresh") ?? ""
}

export function saveUserId(id) {
    localStorage.setItem("userId", id)
}

export function getUserId() {
    return localStorage.getItem("userId") ?? "00000000-0000-0000-0000-000000000000"
}

export function clearStorage() {
    localStorage.clear()
}

export function saveEmail(email) {
    return localStorage.setItem("email", email)
}

export function getEmail() {
    return localStorage.getItem("email") ?? ""
}

