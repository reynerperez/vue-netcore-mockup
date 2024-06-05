<template>
    <v-snackbar v-model="snackbar.show" vertical color="red" variant="tonal">
        <div class="text-subtitle-1 pb-2">{{ snackbar.title }}</div>

        <p>{{ snackbar.message }}</p>

        <template v-slot:actions>
            <v-btn color="red" @click="snackbar.show = false">
                Close
            </v-btn>
        </template>
    </v-snackbar>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useInvoiceStore } from '@/stores/invoice'
const store = useInvoiceStore()
const snackbar = computed(() => store.snackbar)


</script>