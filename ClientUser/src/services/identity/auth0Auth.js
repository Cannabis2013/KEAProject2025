import {clearStorage,} from "./authStorage"
import {useAuth0} from "@auth0/auth0-vue";

const auth0 = useAuth0()

export default {
    async signIn(email, password) {
        return null
    },
    async currentUser() {
        return null
    },
    async signedIn() {
        return null
    },
    async logOut() {
        clearStorage()
    }
}