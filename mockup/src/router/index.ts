import { createRouter, createWebHistory } from "vue-router";
import Layout from "../pages/Layout.vue";
import Files from "../pages/Files.vue";
import Invoices from "../pages/Invoices.vue";

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
    ]
});