import About from '@/pages/about.vue'
import ErrorRouting from '@/pages/ErrorRouting.vue'
import Home from '@/pages/home.vue'
import NewsPage from "@/pages/NewsPage.vue";
import UserPage from "@/pages/UserPage.vue";
import SignInPage from "@/pages/SignInPage.vue"
import NotAuthorized from '@/pages/NotAuthorized.vue'
import {signedIn} from '@/services/identity/auth'
import {createRouter, createWebHashHistory} from 'vue-router'

const routes = [
    {
        path: '/',
        component: Home
    },
    {
        path: '/news',
        component: NewsPage
    },
    {
        path: '/about',
        component: About
    },
    {
        path: '/user',
        component: UserPage,
        protected: true,
        meta: {protected: true}
    },
    {
        path: '/error',
        component: ErrorRouting
    },
    {
        path: '/login',
        component: SignInPage
    },
    {
        path: '/unauthorized',
        component: NotAuthorized
    }
]

const router = createRouter({
    history: createWebHashHistory(),
    routes
})

router.beforeEach(async (to, from) => {
    if (to.fullPath === '/') return
    const isAuthenticated = await signedIn()
    const protectedRoute = to.meta.protected ?? false
    if (!isAuthenticated && protectedRoute) return '/unauthorized'
})

router.afterEach(async (to, from) => {
    const result = routes.find(r => r.path === to.fullPath)
    if (!result) await router.replace('/error')
})

export default router
