<template>
    <v-app>
        <v-app-bar>
            <v-app-bar-nav-icon @click="drawer = !drawer">
            </v-app-bar-nav-icon>
            <v-app-bar-title>Mockup Application</v-app-bar-title>
            <v-spacer></v-spacer>
            <v-btn v-if="authenticated" class="me-10" color="red" variant="tonal" @click="handleLogin">Logout</v-btn>
            <v-btn v-else class="me-10" color="success" variant="tonal" @click="handleLogin">Login</v-btn>
        </v-app-bar>
        <v-navigation-drawer v-model="drawer">
            <MenuList />
        </v-navigation-drawer>
        <v-main>
            <RouterView></RouterView>
            <AppMessages></AppMessages>
        </v-main>
    </v-app>
</template>

<script setup>
import { ref } from 'vue'
import MenuList from '@/components/MenuList.vue';
import AppMessages from '@/components/AppMessages.vue';
import axiosInstance from '@/services/httpFetcher';

const drawer = ref(null)
const authenticated = ref(false)

const handleLogin = async () => {
    if (authenticated.value) {
        delete axiosInstance.defaults.headers.common["Authorization"]
        authenticated.value = false
    } else {
        const { data: token } = await axiosInstance.get('auth')
        console.log(token)
        axiosInstance.defaults.headers.common['Authorization'] = `Bearer ${token}`
        authenticated.value = true
    }

}

</script>