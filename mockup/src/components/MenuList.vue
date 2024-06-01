<template>
    <v-list density="compact" nav>
        <v-list-item v-for="item in items" ::key="item.title" :prepend-icon="item.icon" :to="{ name: item.routeName }"
            :title="item.title"></v-list-item>
    </v-list>
</template>

<script setup>
import { ref } from 'vue'

const items = ref([
    { icon: 'mdi-home', routeName: 'invoices', title: 'Invoices' },
    { icon: 'mdi-folder', routeName: 'files', title: 'Files' }
])

</script>