<template>
    <v-dialog v-model="model" width="auto" :persistent="loading">
        <v-card max-width="400" text="Are you sure you want to delete this invoice?">
            <v-divider></v-divider>
            <template v-slot:actions>
                <v-btn class="ms-auto" text="Delete" color="red" variant="tonal" @click="handleDelete()"
                    :loading="loading"></v-btn>
            </template>
        </v-card>
    </v-dialog>
</template>

<script setup lang="ts">
import { defineModel, defineProps, computed } from 'vue'
import { useInvoiceStore } from '@/stores/invoice'

const store = useInvoiceStore()
const model = defineModel()

const props = defineProps<{
    id?: number
}>()

const loading = computed(() => store.loading)


function handleDelete() {
    store.deleteInvoice(props.id)
    model.value = false
}
</script>