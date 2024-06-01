import { ActionTree, GetterTree, MutationTree } from "vuex";
import { Invoice } from "../models/Invoice"
import { fetchInvoices, updateInvoice, createInvoice, deleteInvoice, uploadFile } from "@/services/invoiceService";



type State = {
    invoices: Invoice[],
    selected?: Invoice,
    dialog: false,
    loading: boolean,
}

const state: State = {
    invoices: [],
    selected: undefined,
    dialog: false,
    loading: true,
}

const getters = <GetterTree<State, any>>{
    invoices(state) {
        return state.invoices
    }
};


const mutations = <MutationTree<State>>{
    setInvoices(state, params) {
        state.invoices = params;
    },
    setLoading(state, params) {
        state.loading = params;
    },
    setDialog(state, params) {
        state.dialog = params;
    },
    setSelected(state, params) {
        state.selected = params;
    },
    updateInvoice(state, invoice) {
        const itemIndex = state.invoices.findIndex(x => x.id === invoice.id)
        state.invoices[itemIndex] = invoice;
    },
    updateInvoiceFile(state, { id, filename }) {
        const itemIndex = state.invoices.findIndex(x => x.id === id)
        state.invoices[itemIndex] = { ...state.invoices[itemIndex], file: filename };
    }
};

const actions = <ActionTree<State, any>>{
    async fetchInvoices({ commit }) {
        commit('setLoading', true)
        commit('setInvoices', await fetchInvoices())
        commit('setLoading', false)
    },
    setDialog({ commit }, params) {
        commit('setDialog', params)
    },
    addInvoiceDialog({ commit }) {
        commit('setSelected', null)
        commit('setDialog', true)
    },
    editInvoiceDialog({ commit }, params) {
        commit('setSelected', params)
        commit('setDialog', true)
    },
    async createInvoice({ commit }, invoice) {
        commit('setLoading', true)
        await createInvoice(invoice)
        commit('updateInvoice', invoice)
        commit('setLoading', false)
        commit('setDialog', false)
    },
    async updateInvoice({ commit }, invoice) {
        commit('setLoading', true)
        await updateInvoice(invoice)
        commit('updateInvoice', invoice)
        commit('setLoading', false)
        commit('setDialog', false)
    },
    async deleteInvoice({ commit, state }, id) {
        commit('setLoading', true)
        await deleteInvoice(id)
        commit('setInvoices', state.invoices.filter(i => i.id !== id))
        commit('setDialog', false)
        commit('setLoading', false)
    },
    async uploadFile({ commit, state }, { Id, File }) {
        commit('setLoading', true)
        const filename = await uploadFile(Id, File)
        commit('updateInvoiceFile', { id: Id, filename })
        commit('setLoading', false)
    }
};

export default {
    state, getters, mutations, actions
};