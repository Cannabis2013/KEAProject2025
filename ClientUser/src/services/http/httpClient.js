import {getAccessToken} from "@/services/identity/authStorage.js";

const baseDomain = "https://localhost:7226"

function header(token) {
    return token ? {
        "Content-Type": "application/json",
        'Authorization': `Bearer ${token}`
    } : {"Content-Type": "application/json"}
}

async function _get(route, token) {
    return await fetch(route, {
        method: "GET",
        headers: header(token)
    })
}

async function _post(route, data, token) {
    return await fetch(route, {
        method: "POST",
        headers: header(token),
        body: data ?? ""
    })
}

export default {
    async getRequest(route) {
        const url = `${baseDomain}${route}`
        const res = await _get(url)
        return res.ok ? await res.json() : null
    },
    async postRequest(route, data) {
        const url = `${baseDomain}${route}`
        const res = await _post(url, JSON.stringify(data))
        return res.ok ? await res.json() : null
    },
    async authGetRequest(route) {
        const url = `${baseDomain}${route}`
        const token = getAccessToken()
        const res = await _get(url, token)
        return res.ok ? await res.json() : null
    },
    async authPostRequest(route, data) {
        const url = `${baseDomain}${route}`
        let token = getAccessToken()
        let res = await _post(url, JSON.stringify(data), token)
        return res.ok ? await res.json() : null
    }
}