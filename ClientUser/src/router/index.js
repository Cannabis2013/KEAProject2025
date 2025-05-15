import About from '@/pages/about.vue'
import NotFound from '@/pages/NotFoundPage.vue'
import Home from '@/pages/home.vue'
import ArticlesPage from "@/pages/articlesPage.vue";
import UserPage from "@/pages/UserPage.vue";
import SignInPage from "@/pages/SignInPage.vue"
import NotAuthorized from '@/pages/NotAuthorized.vue'
import {signedIn} from '@/services/identity/auth'
import {createRouter, createWebHashHistory} from 'vue-router'
import ForumPage from "@/pages/ForumPage.vue";
import TopicPage from "@/pages/TopicPage.vue";

const routes = [
    {
        path: '/',
        component: Home
    },
    {
        path: '/articles',
        component: ArticlesPage
    },
    {
        path: '/forum',
        component: ForumPage,
        meta: {protected: true}
    },
    {
        path: '/topic/:id',
        component: TopicPage,
        meta: {protected: true}
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
        component: NotFound
    },
    {
        path: '/login',
        component: SignInPage
    },
    {
        path: '/unauthorized',
        component: NotAuthorized
    },
    {
        path: '/:pathMatch(.*)*', 
        name: 'NotFound', 
        component: NotFound}
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
/*
router.afterEach(async (to, from) => {
    const result = routes.find(r => r.path === to.fullPath)
    if (!result) await router.replace('/error')
})
 */
export default router
