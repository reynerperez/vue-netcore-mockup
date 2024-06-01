<template>
    <v-dialog v-model="model" width="auto" :persistent="loading">
        <v-form @submit.prevent="handleSubmit()">
            <v-card text="Upload PDF file" min-width="400">
                <v-card-text>
                    <v-file-input v-model="file" :rules="fileRules" label="File input"
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
import { useStore } from 'vuex'

const store = useStore()
const model = defineModel()

const props = defineProps<{
    id: number
}>()
const file = ref()
const fileRules = [
    (value: any) => {
        console.log(value)
        if (value.length) return true
        return 'You must enter a file.'
    },
]
const loading = computed(() => store.state.invoice.loading)


function handleSubmit() {
    store.dispatch('uploadFile', { Id: props.id, File: file.value })
    model.value = false;
}
</script>