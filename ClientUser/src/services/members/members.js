import {currentUser} from "@/services/identity/auth.js";
import httpClient from "@/services/http/httpClient.js";

export async function signedInMember() {
    try {
        const user = await currentUser();
        const userId = user.id ?? null;
        if (!userId) return null
        const route = `/members/user/${userId}`
        return await httpClient.authGetRequest(route)
    } catch (error) {
        return null
    }
}