<template>
    <div class="pa-4 text-center">
        <v-dialog v-model="showForm" max-width="600" @close="setDialog(false)" :persistent="loading">
            <v-form @submit.prevent="handleSubmit()">
                <v-card :prepend-icon="selected ? 'mdi-pencil' : 'mdi-plus'"
                    :title="selected ? 'Edit Invoice' : 'Add Invoice'">
                    <v-card-text>

                        <v-text-field v-model="form.title" label="Title*" required variant="outlined"></v-text-field>
                        <v-text-field v-model="dateFormated" label="Date" variant="outlined">
                            <v-menu activator="parent">
                                <span slot="activator">
                                </span>
                                <v-date-picker color="primary" hideHeader hideWeekdays v-model="form.date">
                                </v-date-picker>
                            </v-menu>
                        </v-text-field>
                        <v-text-field v-model="form.amount" label="Amount*" required variant="outlined"></v-text-field>


                        <small class="text-caption text-medium-emphasis">*indicates required field</small>
                    </v-card-text>

                    <v-divider></v-divider>

                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn text="Close" variant="plain" @click="setDialog(false)" :disabled="loading"></v-btn>
                        <v-btn color="success" text="Save" variant="tonal" @click="handleSubmit"
                            :loading="loading"></v-btn>
                    </v-card-actions>
                </v-card>
            </v-form>

        </v-dialog>
    </div>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { useStore } from 'vuex'
import type { InvoiceDraft } from '../models/Invoice'
import moment from 'moment';

const store = useStore()

const selected = computed(() => store.state.invoice.selected)
const loading = computed(() => store.state.invoice.loading)
const setDialog = (value: boolean) => store.dispatch('setDialog', value)
const showForm = computed({
    get: () => store.state.invoice.dialog,
    set: (value) => { setDialog(value), resetForm() }
})

const formatDate = (value: string | Date) => {
    return moment(value).format('DD/MM/YYYY')
}

const resetForm = () => {
    form.value = { title: '', date: new Date(), amount: '', file: '' }
}

const dateFormated = computed(() => formatDate(form.value.date))

watch(selected, (invoice) => {
    console.log(invoice?.date)
    if (invoice)
        form.value = { title: invoice.title, date: moment(invoice.date, 'DD/MM/YYYY').toDate(), amount: invoice.amount, file: invoice.file }
    else
        form.value = { title: '', date: new Date(), amount: '', file: '' }
})

const form = ref<InvoiceDraft>({ title: '', date: new Date(), amount: '', file: '' })

const handleSubmit = async () => {
    if (selected.value)
        store.dispatch('updateInvoice', { id: selected.value.id, ...form.value, date: moment(form.value.date).format('DD/MM/YYYY') })
    else
        store.dispatch('createInvoice', { ...form.value, date: moment(form.value.date).format('DD/MM/YYYY') })

}

</script>