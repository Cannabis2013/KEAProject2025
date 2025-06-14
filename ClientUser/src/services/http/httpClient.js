import {getAccessToken} from "@/services/identity/authStorage.js";

const apiDomain = "https://localhost:7226"

async function _request(route, method,token,data) {
    return await fetch(route, {
        method: method ?? "GET",
        headers: token ? {
            "Content-Type": "application/json",
            'Authorization': `Bearer ${token}`
        } : {"Content-Type": "application/json"},
        body: data ?? undefined
    })
}

export default {
    async getRequest(route) {
        const url = `${apiDomain}${route}`
        const res = await _request(url)
        return res.ok ? await res.json() : null
    },
    async postRequest(route, data) {
        const url = `${apiDomain}${route}`
        const res = await _request(url,"POST", null,JSON.stringify(data))
        return res.ok ? await res.json() : null
    },
    async authGetRequest(route) {
        const url = `${apiDomain}${route}`
        const token = getAccessToken()
        const res = await _request(url,"GET",token)
        return res.ok ? await res.json() : null
    },
    async authPostRequest(route, data) {
        const url = `${apiDomain}${route}`
        const token = getAccessToken()
        const res = await _request(url,"POST", token, JSON.stringify(data))
        return res.ok ? await res.json() : null
    },
    async authPatchRequest(route, data) {
        const url = `${apiDomain}${route}`
        const token = getAccessToken()
        const res = await _request(url,"PATCH", token, JSON.stringify(data))
        return res.ok ? await res.json() : null
    },
    async authDeleteRequest(route) {
        const url = `${apiDomain}${route}`
        const token = getAccessToken()
        const res = await _request(url,"DELETE", token)
        return res.ok ? await res.json() : null
    }
}