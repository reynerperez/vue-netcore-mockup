<template>
    <v-data-table :items="invoices" :headers="headers">
        <template v-slot:item.file="{ item }">
            <v-btn-group v-if="item.file.length > 0" density="compact" color="success">
                <v-btn class="custom" size="small" variant="tonal" color="success" prepend-icon="mdi-paperclip"
                    :href="`${url}invoices/download?InvoiceId=${item.id}`">
                    {{ item.file }}
                </v-btn>
                <v-menu>
                    <template v-slot:activator="{ props }">
                        <v-btn size="small" icon color="success" variant="tonal" v-bind="props">
                            <v-icon icon="mdi-menu-down"></v-icon>
                        </v-btn>
                    </template>
                    <v-list density="compact">
                        <v-list-item @click="handleUpload(item.id)">
                            <template v-slot:prepend>
                                <v-icon icon="mdi-upload" size="small"></v-icon>
                            </template>
                            <v-list-item-title>Replace file</v-list-item-title>
                        </v-list-item>
                    </v-list>
                </v-menu>
            </v-btn-group>

            <v-btn v-else class="custom" density="comfortable" variant="tonal" prepend-icon="mdi-upload"
                @click="handleUpload(item.id)">
                Upload File
            </v-btn>

        </template>
        <template v-slot:item.actions="{ item }">
            <v-menu>
                <template v-slot:activator="{ props }">
                    <v-btn icon="mdi-dots-vertical" v-bind="props" density="compact" variant="plain"></v-btn>
                </template>
                <v-list>
                    <v-list-item @click="handleEdit(item)">
                        <template v-slot:prepend>
                            <v-icon icon="mdi-pencil"></v-icon>
                        </template>
                        <v-list-item-title>Edit</v-list-item-title>
                    </v-list-item>
                    <v-list-item @click="handleDelete(item.id)">
                        <template v-slot:prepend>
                            <v-icon icon="mdi-delete"></v-icon>
                        </template>
                        <v-list-item-title>Delete</v-list-item-title>
                    </v-list-item>
                </v-list>
            </v-menu>
        </template>
    </v-data-table>
    <ConfirmDialog v-model="showDelete" :id="selectedId" />
    <UploadFileDialog v-model="showUpload" :id="selectedId" />
</template>

<script setup lang="ts">
import { API_URL } from '../services/const.ts';
import { computed, ref } from 'vue'
import { useStore } from 'vuex'
import { toCurrency } from '@/helpers'
import { Invoice } from '@/models/Invoice';
import ConfirmDialog from '../components/ConfirmDialog.vue';
import UploadFileDialog from '../components/UploadFileDialog.vue';

const headers = [
    { title: 'Title', value: 'title' },
    { title: 'Date', key: 'date' },
    { title: 'Amount', key: 'amount', value: ({ amount }: { amount: string }) => toCurrency(+amount) },
    { title: 'PDF', key: 'file' },
    { title: 'Actions', key: 'actions' },
]

const store = useStore()

const url = API_URL

const invoices = computed<Invoice[]>(() => store.state.invoice.invoices)
const selectedId = ref<number>(0)

const showDelete = ref(false)
const showUpload = ref(false)

function handleEdit(invoice: Invoice) {
    store.dispatch('editInvoiceDialog', invoice)
}


function handleDelete(id: number) {
    selectedId.value = id
    showDelete.value = true;
}

function handleUpload(id: number) {
    selectedId.value = id
    showUpload.value = true;
}

</script>

<style>
.custom .v-btn__content {
    font-size: 12px !important;
    text-transform: capitalize !important;
    font-weight: normal !important;
}

.v-btn-group--density-compact.v-btn-group {
    height: 28px;
}
</style>