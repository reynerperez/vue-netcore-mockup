<template>
    <v-container>
        <v-row>
            <v-col cols="12">
                <v-card class="py-4" rounded="lg" variant="tonal">
                    <template #title>
                        <h2 class="text-h4 font-weight-light pb-4">Invoices</h2>
                    </template>

                    <div class="mx-4 d-flex flex-row">
                        <v-text-field class="pe-4" prepend-inner-icon="mdi-magnify" rounded clearable
                            placeholder="Search Invoice..." v-model="searchTerm" @keyup.enter="search()"
                            :on-click:clear="search" density="compact" variant="solo-filled"></v-text-field>
                        <v-btn class="rounded-xl px-4" @click="openDialog()" prepend-icon="mdi-plus" color="success"
                            variant="tonal">
                            Add Invoice
                        </v-btn>
                    </div>

                    <div class="ma-4">
                        <InvoiceTable />
                    </div>
                </v-card>
            </v-col>
        </v-row>
        <InvoiceForm></InvoiceForm>
    </v-container>

</template>

<script setup lang="ts">
import { onMounted, ref, watch } from 'vue'
import { useDebounceFn } from '@vueuse/core'
import { useInvoiceStore } from '@/stores/invoice'

import InvoiceForm from '../components/InvoiceForm.vue';
import InvoiceTable from '../components/InvoiceTable.vue';

const store = useInvoiceStore()
const searchTerm = ref("")
const openDialog = () => store.addInvoiceDialog()
//const debSearch = useDebounceFn(() => await store.getInvoices(searchTerm.value ?? ""), 1000)
const search = useDebounceFn(async () => await store.getInvoices(searchTerm.value ?? ""), 1000)


watch(searchTerm, () => search())


onMounted(() => {
    store.getInvoices()
})

</script>