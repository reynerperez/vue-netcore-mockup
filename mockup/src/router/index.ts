import { createRouter, createWebHistory } from "vue-router";
import Layout from "../views/Layout.vue";
import Files from "../views/Files.vue";
import Invoices from "../views/Invoices.vue";

export default createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: "/",
            redirect: { path: "/invoices" },
            component: Layout,
            children: [
                {
                    name: 'invoices',
                    path: '/invoices',
                    component: Invoices
                },
                {
                    name: 'files',
                    path: '/files',
                    component: Files
                },
            ]
        },



        // {
        //     path: "/",
        //     component: () => import("@/views/Home"),
        //     children: [
        //         {
        //             path: "",
        //             name: "home",
        //             component: () => import("@/views/HomeGlobal")
        //         },
        //         {
        //             path: "my-feed",
        //             name: "home-my-feed",
        //             component: () => import("@/views/HomeMyFeed")
        //         },
        //         {
        //             path: "tag/:tag",
        //             name: "home-tag",
        //             component: () => import("@/views/HomeTag")
        //         }
        //     ]
        // },
        // {
        //     name: "login",
        //     path: "/login",
        //     component: () => import("@/views/Login")
        // },
        // {
        //     name: "register",
        //     path: "/register",
        //     component: () => import("@/views/Register")
        // },
        // {
        //     name: "settings",
        //     path: "/settings",
        //     component: () => import("@/views/Settings")
        // },
        // // Handle child routes with a default, by giving the name to the
        // // child.
        // // SO: https://github.com/vuejs/vue-router/issues/777
        // {
        //     path: "/@:username",
        //     component: () => import("@/views/Profile"),
        //     children: [
        //         {
        //             path: "",
        //             name: "profile",
        //             component: () => import("@/views/ProfileArticles")
        //         },
        //         {
        //             name: "profile-favorites",
        //             path: "favorites",
        //             component: () => import("@/views/ProfileFavorited")
        //         }
        //     ]
        // },
        // {
        //     name: "article",
        //     path: "/articles/:slug",
        //     component: () => import("@/views/Article"),
        //     props: true
        // },
        // {
        //     name: "article-edit",
        //     path: "/editor/:slug?",
        //     props: true,
        //     component: () => import("@/views/ArticleEdit")
        // }
    ]
});