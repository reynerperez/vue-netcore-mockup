<template>
    <v-container>
        <v-row>
            <v-col cols="12">
                <v-card class="py-4" rounded="lg" variant="tonal">
                    <template #title>
                        <h2 class="text-h4 font-weight-light pb-4">Invoices</h2>
                    </template>

                    <div class="mx-4 d-flex flex-row-reverse">
                        <v-btn class="rounded-xl px-4" @click="openDialog()" prepend-icon="mdi-plus" color="success"
                            variant="tonal">
                            Add Invoice
                        </v-btn>
                    </div>

                    <div class="ma-4">
                        <InvoiceTable class="rounded-lg" />
                    </div>
                </v-card>
            </v-col>
        </v-row>
        <InvoiceForm></InvoiceForm>
    </v-container>

</template>

<script setup lang="ts">
import { onMounted } from 'vue'
import { useStore } from 'vuex'

import InvoiceForm from '../components/InvoiceForm.vue';
import InvoiceTable from '../components/InvoiceTable.vue';

const store = useStore()
const openDialog = () => store.dispatch('addInvoiceDialog')


onMounted(() => {
    store.dispatch('fetchInvoices')
})

</script>