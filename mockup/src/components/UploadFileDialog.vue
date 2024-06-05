<template>
    <v-dialog v-model="model" width="auto" :persistent="loading">
        <v-form @submit.prevent="handleSubmit()" ref="formRef">
            <v-card text="Upload PDF file" min-width="400">
                <v-card-text>
                    <v-file-input v-model="file" :rules="rules.file" label="File input"
                        variant="outlined"></v-file-input>
                </v-card-text>
                <v-divider></v-divider>
                <template v-slot:actions>
                    <v-btn class="ms-auto" type="submit" text="Upload" prepend-icon="mdi-upload" color="success"
                        variant="tonal" :loading="loading"></v-btn>
                </template>
            </v-card>
        </v-form>
    </v-dialog>
</template>

<script setup lang="ts">
import { defineModel, defineProps, computed, ref } from 'vue'
import { useInvoiceStore } from '@/stores/invoice'
import { rules } from '@/helpers/validation'

const props = defineProps<{
    id: number
}>()

const store = useInvoiceStore()
const model = defineModel()
const file = ref()
const formRef = ref()
const loading = computed(() => store.loading)


async function handleSubmit() {
    if (!formRef.value.isValid) return
    await store.uploadFile(props.id, file.value)
    file.value = null;
    model.value = false;
}
</script>