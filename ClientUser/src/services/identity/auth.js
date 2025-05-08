import {
    clearStorage,
    getAccessToken,
    getEmail,
    getRefreshToken,
    getUserId,
    saveAccessToken,
    saveEmail,
    saveRefreshToken,
    saveUserId
} from "@/services/identity/authStorage.js";
import httpClient from "@/services/http/httpClient.js";

export async function signIn(email, password) {
    try {
        const data = await httpClient.postRequest("/auth/login", {email, password})
        if (!data) return null
        updateTokens(data)
        updateUserInformation(data)
        return data
    } catch (e) {
        console.error(e)
        return null
    }
}

export async function currentUser() {
    return {
        email: getEmail(),
        id: getUserId()
    }
}

function updateUserInformation(data) {
    saveEmail(data.email)
    saveUserId(data.id)
}

function updateTokens(data) {
    saveAccessToken(data.accessToken)
    saveRefreshToken(data.refreshToken)
}

async function refresh() {
    const data = await httpClient.postRequest("/auth/refresh", {
        "email": getEmail(),
        "refreshToken": getRefreshToken()
    })
    if (!data) return false
    updateTokens(data)
    return true
}

export async function signedIn() {
    try {
        if (!getAccessToken()) return false
        const data = await httpClient.authGetRequest("/auth/check")
        return data != null ? data : await refresh()
    } catch (e) {
        return false
    }
}

export function logOut() {
    return clearStorage()
}