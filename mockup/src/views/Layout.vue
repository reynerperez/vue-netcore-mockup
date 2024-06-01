<template>
    <v-app>
        <v-app-bar>
            <v-app-bar-nav-icon @click="drawer = !drawer">
            </v-app-bar-nav-icon>
            <v-app-bar-title>Mockup Application</v-app-bar-title>
        </v-app-bar>
        <v-navigation-drawer v-model="drawer">
            <MenuList />
        </v-navigation-drawer>
        <v-main>
            <RouterView></RouterView>
        </v-main>
    </v-app>
</template>

<script setup>
import { ref } from 'vue'
import MenuList from '@/components/MenuList.vue';

const drawer = ref(null)

</script>