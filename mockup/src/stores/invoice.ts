import { Invoice } from '@/models/Invoice'
import * as invoiceService from '@/services/invoiceService'
import { defineStore } from 'pinia'
import { ref, reactive } from 'vue'

export const useInvoiceStore = defineStore('invoices', () => {

    const invoices = ref<Invoice[]>([])
    const selected = ref<Invoice>()
    const dialog = ref(false)
    const loading = ref(false)
    const loadingTable = ref(false)
    const snackbar = reactive({ show: false, title: '', message: '' })


    const getInvoices = async (term: string = "") => {
        loadingTable.value = true;
        try {
            invoices.value = await invoiceService.fetchInvoices(term)
        } finally {
            loadingTable.value = false;
        }
    }

    const setDialog = (value: boolean) => {
        dialog.value = value;
    }

    const addInvoiceDialog = () => {
        selected.value = undefined;
        setDialog(true)
    }

    const editInvoiceDialog = (invoice: Invoice) => {
        selected.value = invoice;
        setDialog(true)
    }

    const createInvoice = async (invoice: Invoice) => {
        loading.value = true;
        try {
            const { invoiceId } = await invoiceService.createInvoice(invoice)
            invoice = { ...invoice, id: invoiceId }
            invoices.value.push(invoice)
            dialog.value = false;
        } finally {
            loading.value = false;
        }
    }

    const updateInvoice = async (invoice: Invoice) => {
        loading.value = true;
        try {
            await invoiceService.updateInvoice(invoice)
            const itemIndex = invoices.value.findIndex(x => x.id === invoice.id)
            invoices.value[itemIndex] = invoice
            dialog.value = false;
        } finally {
            loading.value = false;
        }
    }

    const deleteInvoice = async (id: Invoice['id']) => {
        loading.value = true;
        try {
            await invoiceService.deleteInvoice(id)
            const filtered = invoices.value.filter(i => i.id !== id)
            invoices.value = filtered
        } finally {
            loading.value = false;
        }

    }

    const uploadFile = async (id: Invoice['id'], file: File) => {

        loading.value = true;
        try {
            const fileName = await invoiceService.uploadFile(id, file)
            const itemIndex = invoices.value.findIndex(x => x.id === id)
            invoices.value[itemIndex].file = fileName;
        } finally {
            loading.value = false;
        }

    }

    return {
        invoices,
        selected,
        dialog,
        loading,
        loadingTable,
        snackbar,
        getInvoices,
        setDialog,
        addInvoiceDialog,
        editInvoiceDialog,
        createInvoice,
        updateInvoice,
        deleteInvoice,
        uploadFile,
    }

})